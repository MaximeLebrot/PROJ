using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class MetaPlayerController : MonoBehaviour, IPersist
{
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public PlayerController playerController3D { get; private set; }
    public PuzzlePlayerController puzzleController { get; private set; }
    public Animator animator { get; private set; }

    //StateMachine
    private StateMachine stateMachine;
    [SerializeField] private PlayerState[] states;
    [SerializeField] public List<ControllerValues> controllerValues = new List<ControllerValues>();
    
    public ControllerInputReference inputReference;
    public PlayerInput playerInput;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        physics = GetComponent<PlayerPhysicsSplit>();
        playerController3D = GetComponent<PlayerController>();
        puzzleController = GetComponent<PuzzlePlayerController>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine(this, states);
    }
    private void OnEnable()
    {
        inputReference.Initialize();
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
        inputReference.InputMaster.Interact.performed += OnHub;
    }
    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
        inputReference.InputMaster.Interact.performed -= OnHub;
    }

    //TEMPORARY
    private void OnHub(InputAction.CallbackContext obj)
    {
        transform.position = new Vector3(871.52002f, 13.1800003f, 608.859985f);
    }

    private void StartPuzzle(StartPuzzleEvent spe)
    {
        puzzleController.CurrentPuzzleID = spe.info.ID;
        puzzleController.PuzzleTransform = spe.info.puzzlePos;
        stateMachine.ChangeState<PuzzleState>();
    }


    public void ChangeStateToOSPuzzle(StartPuzzleEvent eve) => stateMachine.ChangeState<OSPuzzleState>();

    public void ChangeStateToOSWalk(ExitPuzzleEvent eve) => stateMachine.ChangeState<OSWalkState>();

    private void Update()
    {
        
        stateMachine.RunUpdate();
    }


    public void Save(string gameName)
    {
        
    }

    public void Load(string gameName)
    {
        
    }
}