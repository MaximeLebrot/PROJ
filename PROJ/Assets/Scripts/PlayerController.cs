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

    [HideInInspector] public Vector3 force;

    //Component references
    public PlayerPhysicsSplit physics;
   // public PhysicsComponent physics { get; private set; }
    public Animator animator { get; private set; }
    private AudioSource audioSource;
    private Vector3 input;
    private bool jump;
    private Transform cameraTransform;
    private RaycastHit groundHitInfo;
    private bool wasGrounded;

    private float xMove, zMove;
    private bool surf = false;
    void Awake()
    {
        cameraTransform = Camera.main.transform;
        //physics = GetComponent<PhysicsComponent>();
        physics = GetComponent<PlayerPhysicsSplit>();
        groundCheckBox = GetComponentInChildren<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        Vector3 input =
        Vector3.right * xMove + 
        Vector3.forward * zMove;
        //InputGrounded(input);
        if(surf)
            InputSurfGrounded(input);
        else
            InputGrounded(input);

        //For testing only
        if (Input.GetKeyUp(KeyCode.G))
            physics.ResetPosition();
        if (Input.GetKeyUp(KeyCode.T))
            TransitionSurf();
    }
    private void FixedUpdate()
    {
        Jump();
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
    public void InputAirborne(Vector3 inp, bool airborne)
    {
        input = inp.normalized * airControl;
        //PlayerDirection();
        RotateInDirectionOfMovement();
        AccelerateAirborne();
    }
    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force = input * acceleration;
        /*
        om vi accelerar i en annan riktning vill vi egentligen bromsa f�rst
        skal�rprodukten anv�nds i multiplikation f�r att avg�ra hur mycket av decelerationen som ska
        appliceras, d� detta b�r bero p� vinkeln i vilken man byter riktning/velocitet/momentum
        */

        force -= (((dot - 1) * turnRate *  -physics.GetXZMovement().normalized) / 2);
        //addera * turnSpeed av kraften vi precis tog bort, till v�r nya riktning.
        //g�r i princip att man sv�nger snabbare
        force += (((dot - 1) * turnRate * retainedSpeedWhenTurning * -inputXZ.normalized) / 2);
        //Debug.DrawLine(transform.position, transform.position + -((dot - 1) * turnRate * -physics.velocity.normalized) / 2, Color.red);
    }
    private void AccelerateAirborne()
    {
        force = input * acceleration;
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
        input = input.magnitude * Vector3.ProjectOnPlane(input, physics.groundHitInfo.normal).normalized;
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

        transform.Rotate(0, xMove * turnSpeed, 0);
    }
    private void Jump()
    {
        if (jump)
        {
            force.y += jumpHeight / Time.fixedDeltaTime; jump = false;
        }
    }

    #endregion


    private void TransitionSurf()
    {
        surf = !surf;
    }
    /// <summary>
    /// Boxcast to get a little thickness to the groundcheck so as to not get stuck in crevasses or similar geometry. 
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        //wasGrounded used to prohibit consecutive dashes without touching ground

        if (Physics.BoxCast(transform.position + Vector3.up, groundCheckBox.size, Vector3.down, out groundHitInfo, transform.rotation, groundCheckDistance, groundCheckMask))
            wasGrounded = true;

        //Old groundcheck, kept in case the boxcast misbehaves. 
        //Physics.Raycast(transform.position, Vector3.down, out groundHitInfo, groundCheckDistance, groundCheckMask);       

        return groundHitInfo.collider;
    }

    //Ability System


    //Gets & Sets
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public void SetJump()
    {
        jump = true;
    }
}
