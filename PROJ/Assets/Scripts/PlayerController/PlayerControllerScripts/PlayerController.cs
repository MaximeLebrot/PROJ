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
    private float inputThreshold = 0.1f;
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
        input = inp.x * turnSpeed * Vector3.right + 
                inp.y * Vector3.forward;   

        //to stop character rotation when input is 0
        if (input.magnitude < inputThreshold)
            Decelerate();
        else
        {
            if (input.magnitude > 1f)
            {
                input.Normalize();
            }
            CalcDirection(inp);
            Accelerate();
        }
    }
    public void InputAirborne(Vector3 inp)
    {
        input = inp.x * cameraTransform.right +
                inp.y * cameraTransform.forward;

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
    private void Decelerate()
    {
        force += -deceleration * physics.GetXZMovement().normalized;
    }
    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force += input * acceleration;
        force -= ((1 - dot) * 0.5f) 
                 * turnRate 
                 * physics.GetXZMovement().normalized;
        //Add "retainedSpeedWhenTurning" amount of previously existing momentum to our new direction
        //Makes turning less punishing
        force += ((1 - dot) * 0.5f)
                 * turnRate 
                 * retainedSpeedWhenTurning 
                 * inputXZ.normalized;
    }
    
    private void PlayerDirection(Vector3 rawInput)
    {
        Vector3 temp = cameraTransform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion camRotation = Quaternion.Euler(temp);

        input = camRotation * input;
        input.y = 0;
        RotateInVelocityDirection();
        ProjectMovement();
    }
    private void RotateInVelocityDirection()
    {
        transform.rotation = Quaternion.LookRotation(physics.GetXZMovement().normalized, Vector3.up);
    }
    //Obsolete
    private void RotateTowardsCameraDirection(Vector3 rawInput)
    {
        /*transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x,
        cameraTransform.localEulerAngles.y,
        transform.localEulerAngles.z);*/

        //rotation from input
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion rotation = Quaternion.Euler(temp);

        //Add rotation to input
        //input += rotation * input;
        /*transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                                 input.x,
                                                 transform.localEulerAngles.z);*/
        transform.rotation = Quaternion.LookRotation(physics.GetXZMovement().normalized, Vector3.up);
        //transform.Rotate(0, rawInput.x, 0);
        //transform.forward = Vector3.Lerp(transform.forward, new Vector3(transform.forward.x, input.y, transform.forward.z), turnSpeed * Time.deltaTime);

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
        if (groundHitAngle > decelerationSlopeAngle)
        {
            //force = slopeDecelerationFactor * -physics.velocity * slopeDecelerationMultiplier;
            force = slopeDecelerationFactor * slopeDecelerationMultiplier * -physics.velocity.normalized;
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
