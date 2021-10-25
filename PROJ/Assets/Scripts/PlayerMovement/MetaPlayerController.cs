using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaPlayerController : MonoBehaviour
{
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public PlayerController playerController3D { get; private set; }
    public PuzzlePlayerController puzzleController { get; private set; }



    //StateMachine
    private StateMachine stateMachine;
    [SerializeField] private PlayerState[] states;
    [SerializeField] public List<ControllerValues> controllerValues = new List<ControllerValues>();
    
    public InputMaster inputMaster;


    private void Awake()
    {
        inputMaster = new InputMaster();
        physics = GetComponent<PlayerPhysicsSplit>();
        playerController3D = GetComponent<PlayerController>();
        puzzleController = GetComponent<PuzzlePlayerController>();
        stateMachine = new StateMachine(this, states);
    }
    private void OnEnable()
    {
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitPuzzle);
    }
    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitPuzzle);
    }
    private void StartPuzzle(StartPuzzleEvent spe)
    {
        puzzleController.CurrentPuzzleID = spe.info.ID;
       
        stateMachine.ChangeState<PuzzleState>();
    }

    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        stateMachine.ChangeState<WalkState>();
    }

    private void Update()
    {
        stateMachine.RunUpdate();
    }
}