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
    public bool surfCamera = false;
    private float groundCheckBoxSize = 0.25f;
    
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

    public void InputGrounded(Vector3 inp)
    {
        input = inp.x * Vector3.right + 
                inp.y * Vector3.forward;

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

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
        AccelerateDecelerate();
    }
    private void CalcDirection(Vector3 inp)
    {
        if (surfCamera)
            RotateInDirectionOfMovement(inp);
        else
            PlayerDirection();
    }
    private void AccelerateDecelerate() 
    {
        //Decelerate
        if (input.magnitude < float.Epsilon)
        {
            force = -deceleration * physics.GetXZMovement().normalized;
        }
        //Accelerate
        else
        {
            Vector3 inputXZ = new Vector3(input.x, 0, input.z);
            float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

            force = input * acceleration;
            force -= (((dot - 1) * turnRate * -physics.GetXZMovement().normalized));          
            //Add "retainedSpeedWhenTurning" amount of previously existing momentum to our new direction
            //Makes turning less punishing
            force += (((dot - 1) * turnRate * retainedSpeedWhenTurning * -inputXZ.normalized));
        }
    }

    //Rotation when using walk
    private void PlayerDirection()
    {
        Vector3 temp = cameraTransform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion camRotation = Quaternion.Euler(temp);

        input = camRotation * input;
        input.y = 0;
        //input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized;
        ProjectMovement();
        RotateTowardsCameraDirection();
    }
    private void RotateTowardsCameraDirection()
    {
        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x,
        cameraTransform.transform.localEulerAngles.y,
        transform.localEulerAngles.z);
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
    public float slopeMaxAngle = 140;
    public float decelerationSlopeAngle = 110f;
    private void ProjectMovement()
    {
        float angle = groundHitInfo.collider == null ? 90 : Vector3.Angle(input, groundHitInfo.normal);
        float slopeDecelerationFactor = 0f;
        if (angle > decelerationSlopeAngle)
        {
            slopeDecelerationFactor = deceleration * ((angle - decelerationSlopeAngle) / (slopeMaxAngle - decelerationSlopeAngle));
           // input += //And the other part shouldnt? Or should we simply project some of it, and ignore the remainder
        }
        if (angle < slopeMaxAngle)
            input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized * slopeDecelerationFactor;        
        else
        {
            //Slide state? 
            //Some disruption to movement, possibly another PlayerState, or timed value tweaks
            input = Vector3.zero;
        }
        Debug.Log("Angle is : " + angle);
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
        
        float groundFriction = 0f;        
        if (grounded)
            groundFriction = groundHitInfo.collider.material.dynamicFriction;
        
        return grounded; 
    }

    //Gets & Sets
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

}
