using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Parameters exposed in the inspector
    [Header("Player Control")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float airControl = 0.2f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float turnRate = 4f;
    [SerializeField] private float turnSpeed; 
    [SerializeField] private float retainedSpeedWhenTurning = 0.33f;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundCheckMask;
    [SerializeField] private float groundCheckDistance = 0.05f;
    private BoxCollider groundCheckBox;

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

    private InputMaster inputMaster;

    
    void Awake()
    {
        inputMaster = new InputMaster();

        cameraTransform = Camera.main.transform;
        physics = GetComponent<PlayerPhysicsSplit>();
        groundCheckBox = GetComponentInChildren<BoxCollider>();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }


    private void Update()
    {

        xMove = inputMaster.Player.Movement.ReadValue<Vector2>().x;
        zMove = inputMaster.Player.Movement.ReadValue<Vector2>().y;


        Vector3 input =
        Vector3.right * xMove +
        Vector3.forward * zMove;

        if (surfCamera)
            InputSurfGrounded(input);
        else
            InputGrounded(input);

     

    }
    private void FixedUpdate()
    {
        IsGrounded();
        physics.AddForce(force);
        force = Vector3.zero;
    }

    #region Movement
    public void InputSurfGrounded(Vector3 inp)
    {
        input = inp;
        if (input.magnitude > 1f)
        {
            input.Normalize();
        }
        RotateInDirectionOfMovement();

        if (input.magnitude < float.Epsilon)
        {
            Decelerate();
        }
        else
            Accelerate();
    }
    public void InputGrounded(Vector3 inp)
    {
        input = inp;
        if (input.magnitude > 1f)
        {
            input.Normalize();
        }
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
    private void RotateInDirectionOfMovement()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion rotation = Quaternion.Euler(temp);
        input = rotation * input;
        input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized;

        transform.Rotate(0, xMove * turnSpeed, 0);
    }


    #endregion


    private void TransitionSurf()
    {
        surfCamera = !surfCamera;
    }
    /// <summary>
    /// Boxcast to get a little thickness to the groundcheck so as to not get stuck in crevasses or similar geometry. 
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        if(Physics.BoxCast(transform.position, Vector3.one * groundCheckBoxSize, Vector3.down, out groundHitInfo, transform.rotation, groundCheckDistance, groundCheckMask))
            Debug.Log("Grounded!");
        return groundHitInfo.collider;
    }

    //Ability System


    //Gets & Sets
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

}
