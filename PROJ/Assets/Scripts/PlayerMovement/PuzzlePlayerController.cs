using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlayerController : MonoBehaviour
{
    #region Parameters exposed in the inspector
    [Header("Puzzle Player Control")]
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float maxSpeed = 5f;
    [Tooltip("The amount of slow-down applied when turning")]
    [SerializeField] private float turnRate = 4f;
    [Tooltip("The speed at which the character model rotates when changing direction")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float retainedSpeedWhenTurning = 0.33f;


    #endregion

    //Component references
    public PlayerPhysicsSplit physics;
    public Animator animator { get; private set; }
    private Vector3 input;
    private bool jump;
    private Transform cameraTransform;
    private RaycastHit groundHitInfo;

    [HideInInspector] public Vector3 force;
    private float xMove, zMove;
    private bool surf = false;
    private float groundCheckBoxSize = 0.25f;
    void Awake()
    {
        physics = GetComponent<PlayerPhysicsSplit>();
    }
    private void OnEnable()
    {
       
    }
    private void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        Vector3 input =
        Vector3.right * xMove +
        Vector3.forward * zMove;
  
        HandleInput(input);
        physics.PuzzleControllerInput();

        //For testing only
        if (Input.GetKeyUp(KeyCode.G))
            physics.ResetPosition();

    }
    private void FixedUpdate()
    {
        physics.AddForce(force);
        force = Vector3.zero;
    }

    #region Movement
    public void HandleInput(Vector3 inp)
    {
        input = inp;
        if (input.magnitude > 1f)
        {
            input.Normalize();
        }
        RotateCharacterInsidePuzzle();

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
        force -= (((dot - 1) * turnRate * -physics.GetXZMovement().normalized) / 2);
        //force += (((dot - 1) * turnRate * retainedSpeedWhenTurning * -inputXZ.normalized) / 2);
    }

    private void Decelerate()
    {
        force = -deceleration * physics.GetXZMovement().normalized;
    }

    private void RotateCharacterInsidePuzzle()
    {
        /*Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion rotation = Quaternion.Euler(temp);
        input = rotation * input;
        input = input.magnitude * Vector3.ProjectOnPlane(input, groundHitInfo.normal).normalized;

        transform.Rotate(0, xMove * turnSpeed, 0);*/
        transform.forward = Vector3.Lerp(transform.forward, input.normalized, turnSpeed * Time.deltaTime);

    }

    #endregion

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
