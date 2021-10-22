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
    //[SerializeField] private float airControl = 0.2f;
    //[SerializeField] private float jumpHeight = 5f;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundCheckMask;
    [SerializeField] private float groundCheckDistance = 0.05f;


    #endregion
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public Animator animator { get; private set; }
    private Transform cameraTransform;

    private RaycastHit groundHitInfo;  
    [HideInInspector] public Vector3 force;
    private Vector3 input;
    private float xMove, zMove;
    private bool surfCamera = false;
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
        
        if (surfCamera)
            RotateInDirectionOfMovement(inp);
        else
            PlayerDirection();

        if (input.magnitude < float.Epsilon)
        {
            Decelerate();
        }
        else
            Accelerate();
    }

    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force = input * acceleration;
        force -= (((dot - 1) * turnRate *  -physics.GetXZMovement().normalized));
        //addera * turnSpeed av kraften vi precis tog bort, till v�r nya riktning.
        //g�r i princip att man sv�nger snabbare
        force += (((dot - 1) * turnRate * retainedSpeedWhenTurning * -inputXZ.normalized));
    }
    private void Decelerate()
    {
        force = -deceleration * physics.GetXZMovement().normalized;
    }

    //Rotation when using walk
    private void PlayerDirection()
    {
        Vector3 temp = cameraTransform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion camRotation = Quaternion.Euler(temp);

        input = camRotation * input;
        input.y = 0;
        input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized;
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
        input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized;

        transform.Rotate(0, rawInput.x * turnSpeed, 0);

    }


    #endregion


    public void TransitionSurf()
    {
        surfCamera = !surfCamera;
    }
    /// <summary>
    /// Boxcast to get a little thickness to the groundcheck so as to not get stuck in crevasses or similar geometry. 
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, Vector3.one * groundCheckBoxSize, Vector3.down, out groundHitInfo, transform.rotation, groundCheckDistance, groundCheckMask);
    }

    //Gets & Sets
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

}
