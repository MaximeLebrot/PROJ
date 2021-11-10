using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaPlayerController : MonoBehaviour, IPersist
{
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public PlayerController playerController3D { get; private set; }
    public PuzzlePlayerController puzzleController { get; private set; }



    //StateMachine
    private StateMachine stateMachine;
    [SerializeField] private PlayerState[] states;
    [SerializeField] public List<ControllerValues> controllerValues = new List<ControllerValues>();
    
    public ControllerInputReference inputReference;


    private void Start()
    {
        
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
        puzzleController.PuzzleTransform = spe.info.puzzlePos;
        stateMachine.ChangeState<PuzzleState>();
    }

    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        stateMachine.ChangeState<WalkState>();
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