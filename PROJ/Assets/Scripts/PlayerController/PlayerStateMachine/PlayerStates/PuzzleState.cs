using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/PuzzleState")]
public class PuzzleState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void EnterState()
    {
        Debug.Log("Enter Puzzle State");
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitPuzzle);
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
        base.EnterState();
    }
    public override void RunUpdate()
    {
        SetInput();
    }
    public override void ExitState()
    {
        Debug.Log("Puzzle state exit state");
        player.playerController3D.enabled = true;
        player.puzzleController.enabled = false;
        base.ExitState();
    }
    private void SetInput()
    {
        player.puzzleController.SetInput(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        Debug.Log("Exit Puzzle");
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitPuzzle);
        stateMachine.ChangeState<WalkState>();
    }
}
