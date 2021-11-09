using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    #endregion

    [HideInInspector] public Vector3 force;
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public Animator animator { get; private set; }
    private Vector3 input;
    private float xMove, zMove;

    //Input
    private InputAction quitPuzzle;


    public int CurrentPuzzleID { get; set; }
    public Transform PuzzleTransform { get; set; }

    public MetaPlayerController metaPlayerController;
    private void OnEnable()
    {
        quitPuzzle = metaPlayerController.inputReference.InputMaster.ExitPuzzle;
        //Does this screw up the build? >>>>> NEW CODE FOR INPUT SYSTEM <<<<
        quitPuzzle.Enable();

        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed += OnQuitPuzzle;
    }
    private void OnDisable()
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed -= OnQuitPuzzle;
        quitPuzzle.Disable();
    }
    void Start()
    {
        physics = GetComponent<PlayerPhysicsSplit>();

    }
    private void Update()
    {
        xMove = metaPlayerController.inputReference.InputMaster.Movement.ReadValue<Vector2>().x;
        zMove = metaPlayerController.inputReference.InputMaster.Movement.ReadValue<Vector2>().y;

        Vector3 input = 
        PuzzleTransform.right * xMove +
        PuzzleTransform.forward * zMove;
        
        HandleInput(input);
    }
    private void OnQuitPuzzle(InputAction.CallbackContext obj)
    {
        EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(PuzzleTransform.GetComponent<Puzzle>().GetPuzzleID()), false));
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
        RotateCharacterInsidePuzzle();
        if (input.magnitude < float.Epsilon)
        {
            Decelerate();
            return;
        }
        else 
        {
            if (input.magnitude > 1f)
                input.Normalize();
        }
        Accelerate();            
    }
    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force = input * acceleration;
        force -= (((dot - 1) * turnRate * -physics.GetXZMovement().normalized) / 2);
    }

    private void Decelerate()
    {
        force = -deceleration * physics.GetXZMovement().normalized;
    }

    private void RotateCharacterInsidePuzzle()
    {
        transform.forward = Vector3.Lerp(transform.forward, input.normalized, turnSpeed * Time.deltaTime);
    }

    #endregion

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
