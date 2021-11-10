using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSPuzzleState")]
public class OSPuzzleState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void EnterState()
    {
        Debug.Log("Entered Puzzle State");
        player.physics.velocity = Vector3.zero;
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
        base.EnterState();
    }
    public override void RunUpdate()
    {
        //When do we leave puzzle state, only controlled by EndPuzzleEvent? 

    }
    public override void ExitState()
    {
        player.playerController3D.enabled = true;
        player.puzzleController.enabled = false;
        base.ExitState();
    }
}
