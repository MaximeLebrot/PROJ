using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Parameters exposed in the inspector
    [Header("Player Control")]  
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 2f;
    
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float turnRate = 4f;
    [SerializeField] private float turnSpeed; 
    [SerializeField] private float retainedSpeedWhenTurning = 0.33f;
    [SerializeField] private float airControl = 0.2f;
    //[SerializeField] private float jumpHeight = 5f;
    
    [Header("Slopes")]
    [SerializeField] private float slopeMaxAngle = 140;
    [SerializeField] private float decelerationSlopeAngle = 110f;
    [SerializeField] private float slopeDecelerationMultiplier = 2f;
    [SerializeField] private float glideMinAngle = 80f;

[Header("GroundCheck")]
    [SerializeField] private LayerMask groundCheckMask;
    [SerializeField] private float groundCheckDistance = 0.05f;


    #endregion

    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public Animator animator { get; private set; }
    private Transform cameraTransform;


    [HideInInspector] public Vector3 force;
    private RaycastHit groundHitInfo;  
    private Vector3 input;
    private bool surfCamera = false;
    private float groundCheckBoxSize = 0.25f;
    public float groundHitAngle { get; private set; }
    public float GlideMinAngle => glideMinAngle;

    //Camera Test/Debug
    public bool dualCameraBehaviour = true;

    void Awake()
    {
        cameraTransform = Camera.main.transform;
        physics = GetComponent<PlayerPhysicsSplit>();
    }
    
    private void FixedUpdate()
    {
        physics.AddForce(force);
        force = Vector3.zero;
    }

    #region Movement

    public void InputGlide(Vector3 inp)
    {
        SlopeDeceleration();
        InputWalk(inp);
    }
    public void InputWalk(Vector3 inp)
    {
        input = inp.x * Vector3.right + 
                inp.y * Vector3.forward;

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        //Maybe this to stop character rotation when input is 0
        //if(input.magnitude < inputThreshold)
        //  Decelerate();
        //else
        CalcDirection(inp);
        AccelerateDecelerate();
    }
    public void InputAirborne(Vector3 inp)
    {
        input = inp.x * Vector3.right +
                inp.y * Vector3.forward;

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        CalcDirection(inp);
        input *= airControl;
        //Cannot decelerate when airborne
        Accelerate();
    }
    private void CalcDirection(Vector3 inp)
    {
        if (dualCameraBehaviour)
        {
            if (surfCamera)
                RotateInDirectionOfMovement(inp);
            else
                PlayerDirection(inp);
        }
        else
            PlayerDirection(inp);


    }
    private void AccelerateDecelerate() 
    {
        //Decelerate
        if (input.magnitude < float.Epsilon)
        {
            force += -deceleration * physics.GetXZMovement().normalized;
        }
        //Accelerate
        else
        {
            Accelerate();           
        }
    }
    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force += input * acceleration;
        force -= (((dot - 1) * turnRate * -physics.GetXZMovement().normalized));
        //Add "retainedSpeedWhenTurning" amount of previously existing momentum to our new direction
        //Makes turning less punishing
        force += (((dot - 1) * turnRate * retainedSpeedWhenTurning * -inputXZ.normalized));
    }
    
    //Do we not want the camera to rotate when the character is standing still? Exit the rotate method if input is below a certain treshold.
    //Rotation when using walk
    private void PlayerDirection(Vector3 rawInput)
    {
        Vector3 temp = cameraTransform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion camRotation = Quaternion.Euler(temp);

        input = camRotation * input;
        //input.y = 0;
        ProjectMovement();
        RotateTowardsCameraDirection(rawInput);
    }
    private void RotateTowardsCameraDirection(Vector3 rawInput)
    {
        /*transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x,
        transform.transform.localEulerAngles.y,
        transform.localEulerAngles.z);*/

        //rotation from input
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion rotation = Quaternion.Euler(temp);

        //Add rotation to input
        input += rotation * input;
        transform.Rotate(0, rawInput.x, 0);

        /*
         * vad är y-värdet vi vill åt. Ska spelarens rotation röra sig mot kamerans?
         * 
         */

    }

    //Rotation when using Glide
    private void RotateInDirectionOfMovement(Vector3 rawInput)
    {
        //Create rotation
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion rotation = Quaternion.Euler(temp);

        //Add rotation to input
        input = rotation * input;

        //steepClimbMaxAngle
        //Do we want a percentage of the input to be projected on plane? 
        //Flat ground would have the same movement, but inclines would not
        //Would probably result in running away from the ground on declines though, 
        //unless the gravity keeps it in check, but it wont be certain to do so.
        //the alternative then would be a full projection on declines, but this requires another split of the logic
        ProjectMovement();
        transform.Rotate(0, rawInput.x * turnSpeed, 0);

    }
    private void SlopeDeceleration()
    {
        float slopeDecelerationFactor = ((groundHitAngle - decelerationSlopeAngle) / (slopeMaxAngle - decelerationSlopeAngle));
        Debug.Log("Ground hit angle is : " + groundHitAngle);
        if (groundHitAngle > decelerationSlopeAngle)
        {
            //Not high enough to properly stop the glide, but feels like absolute garbage when walking
            //Which means, this kind of deceleration needs to be done either only in glidestate, or based
            //off of some momentum factor, if we add Mass as a variable

            //force = slopeDecelerationFactor * -physics.velocity * slopeDecelerationMultiplier;
            force = slopeDecelerationFactor * slopeDecelerationMultiplier * -physics.velocity.normalized;
            Debug.Log("slope decel factor: " + slopeDecelerationFactor + "angle is : " + groundHitAngle);
        }
    }
    private void ProjectMovement()
    {
        groundHitAngle = groundHitInfo.collider == null ? 90 : Vector3.Angle(input, groundHitInfo.normal);
        if (groundHitAngle < slopeMaxAngle)
            input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized;        
        else
        {
            //Slide state? 
            //Some disruption to movement, possibly another PlayerState, or timed value tweaks
            input = Vector3.zero;
        }
        //Debug.Log("Angle is : " + angle);
    }
    #endregion


    public void TransitionSurf(bool val)
    {
        surfCamera = val;
    }
    /// <summary>
    /// Boxcast to get a little thickness to the groundcheck so as to not get stuck in crevasses or similar geometry. 
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        bool grounded = Physics.BoxCast(transform.position, Vector3.one * groundCheckBoxSize, Vector3.down, out groundHitInfo, transform.rotation, groundCheckDistance + physics.GlideHeight, groundCheckMask);
        
        return grounded; 
    }

    //Gets & Sets
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

}
