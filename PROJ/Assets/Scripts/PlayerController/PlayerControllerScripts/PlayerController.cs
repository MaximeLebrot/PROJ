using System;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    #region Parameters exposed in the inspector

    [Header("Player Control")]  
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 2f;   
    [SerializeField] private float turnRate = 4f;
    [SerializeField] private float modelTurnSpeed;
    [SerializeField] private float objectTurnSpeed;
    [SerializeField] private float retainedSpeedWhenTurning = 0.33f;
    [SerializeField] private float airControl = 0.2f;
    
    [Header("Slopes")]
    [SerializeField] private float slopeMaxAngle = 140;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundCheckMask;
    [SerializeField] private float groundCheckDistance = 0.05f;
    private RaycastHit groundHitInfo;
    private float groundCheckBoxSize = 0.1f;

    #endregion
    
    //Input
    private float inputThreshold = 0.1f;
    private Vector3 input;
    private Vector3 force;

    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public Transform cameraTransform { get; private set; }
    public ControllerInputReference inputReference;
    [SerializeField] public GameObject characterModel;

    //Properties
    private float groundHitAngle;

    void Awake()
    {
        Application.targetFrameRate = 250;
        cameraTransform = Camera.main.transform;
        physics = GetComponent<PlayerPhysicsSplit>();
        SecureDelegates();
    }
    private void OnEnable()
    {
        EventHandler<SaveSettingsEvent>.RegisterListener(OnSaveSettings);
        force = Vector3.zero;
    }
    private void OnDisable()
    {
        EventHandler<SaveSettingsEvent>.UnregisterListener(OnSaveSettings);
    }

    //This could instead load a delegate with a preffered input chain, but as of now that would require more code than the current solution. 
    //to be considered in the future, though
    private delegate void RotationDelegate();
    private RotationDelegate rotationDelegate;

    private delegate void InputDelegate(Vector3 input);
    private InputDelegate inputDelegate;
    private void OnSaveSettings(SaveSettingsEvent eve)
    {
        if (eve.settingsData.oneHandMode)
        {
            transform.forward = cameraTransform.forward;
            characterModel.transform.forward = cameraTransform.forward;
            rotationDelegate = RotateInVelocityDirection;
            inputDelegate = InputVelocityRotation;
        }
        else
        {
            rotationDelegate = RotateInCameraDirection;
            inputDelegate = InputCameraRotation;
        }
    }
    private void SecureDelegates()
    {
        if (rotationDelegate == null || inputDelegate == null)
        {
            rotationDelegate = RotateInCameraDirection;
            inputDelegate = InputCameraRotation;
        }
    }
    private void FixedUpdate()
    {
        physics.AddForce(force);
    }

    /// <summary>
    /// If FPS dips below 50 (fixed update tick), resetting the value locally - as in, inside FixedUpdate - 
    /// will result in input values that are actually captured by the state machine being discarded. Input must be allowed to accumulate in that case,
    /// and to accomplish this, force needs to be reset when a frame from Update is actually called inside the physics script, to ensure that we use the input
    /// values before resetting the vector.
    /// </summary>
    public void ResetForceVector()
    {
        force = Vector3.zero;
    }
    #region Movement

    public void InputWalk(Vector3 inp)
    {
        inputDelegate(inp);      

        //to stop character rotation when input is 0
        if (input.magnitude < inputThreshold)
            Decelerate();
        else
        {
            if (input.magnitude > 1f)
            {
                input.Normalize();
            }
            PlayerDirection(inp);
            Accelerate();
        }
    }
    public void InputAirborne(Vector3 inp)
    {
        inputDelegate(inp);

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        PlayerDirection(inp);
        input *= airControl;
        //Cannot decelerate when airborne
        Accelerate();
    }

    private void InputCameraRotation(Vector3 inp)
    {
        input = inp.x * Vector3.right +
                inp.y * Vector3.forward;
    }
    private void InputVelocityRotation(Vector3 inp)
    {
        input = inp.x * transform.right +
                inp.y * transform.forward;
    }

    private void Decelerate()
    {
        Vector3 projectedDeceleration = Vector3.ProjectOnPlane(-physics.GetXZMovement(), groundHitInfo.normal) * deceleration;
        force += projectedDeceleration;
    }
    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force = input * acceleration;
        force -= ((1 - dot) * 0.5f) 
                 * turnRate 
                 * physics.GetXZMovement().normalized;
        
        //Add "retainedSpeedWhenTurning" amount of previously existing momentum to our new direction
        //Makes turning less punishing
        force += ((1 - dot) * 0.5f)
                 * retainedSpeedWhenTurning 
                 * inputXZ.normalized;
    }
    private void ProjectMovement()
    {
        groundHitAngle = groundHitInfo.collider == null ? 90 : Vector3.Angle(input, groundHitInfo.normal);
        if (groundHitAngle < slopeMaxAngle)
            input = Vector3.ProjectOnPlane(input, groundHitInfo.normal);        

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
    #region Rotation
    private void PlayerDirection(Vector3 rawInput)
    {
        rotationDelegate();
        RotateCharacterModel();
        ProjectMovement();
    }
    private void RotateInCameraDirection()
    {
        Vector3 temp = cameraTransform.rotation.eulerAngles;
        temp.x = 0;
        Quaternion camRotation = Quaternion.Euler(temp);

        input = camRotation * input;
        input.y = 0;
    }
    private void RotateInVelocityDirection()
    {
        Vector3 charVelocity = physics.GetXZMovement();
        if (charVelocity.magnitude < inputThreshold)
            return;
        transform.forward = Vector3.Lerp(transform.forward, physics.GetXZMovement().normalized, objectTurnSpeed * Time.deltaTime);
    }
    private void RotateCharacterModel()
    {
        Vector3 charVelocity = physics.GetXZMovement();
        if (charVelocity.magnitude < inputThreshold)
            return;
        characterModel.transform.forward = Vector3.Lerp(characterModel.transform.forward, charVelocity.normalized, modelTurnSpeed * Time.deltaTime);
    }
    #endregion

    /// <summary>
    /// Boxcast to get a little thickness to the groundcheck so as to not get stuck in crevasses or similar geometry. 
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        bool grounded = Physics.BoxCast(transform.position, Vector3.one * groundCheckBoxSize, Vector3.down, out groundHitInfo, transform.rotation, groundCheckDistance, groundCheckMask);

        return grounded; 
    }

    //Gets & Sets
    public void SetDeceleration(float val) => deceleration = val;
    public float GetDeceleration() => deceleration;
}
