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


    private void Awake()
    {
        physics = GetComponent<PlayerPhysicsSplit>();
        playerController3D = GetComponent<PlayerController>();
        puzzleController = GetComponent<PuzzlePlayerController>();
        stateMachine = new StateMachine(this, states);
    }
    private void OnEnable()
    {
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
        EventHandler<CompletePuzzleEvent>.RegisterListener(CompletePuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(EndPuzzle);
    }
    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
        EventHandler<CompletePuzzleEvent>.UnregisterListener(CompletePuzzle);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(EndPuzzle);
    }
    private void StartPuzzle(StartPuzzleEvent spe)
    {
        puzzleController.CurrentPuzzleID = spe.info.ID;
       
        stateMachine.ChangeState<PuzzleState>();
    }
    private void EndPuzzle(ExitPuzzleEvent spe)
    {
        stateMachine.ChangeState<WalkState>();
    }

    public void CompletePuzzle(CompletePuzzleEvent eve)
    {
        stateMachine.ChangeState<WalkState>();
    }

    private void Update()
    {
        stateMachine.RunUpdate();
    }
}