using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSPuzzleState")]
public class OSPuzzleState : PlayerState
{
    private bool realignPlayer;

    public override void Initialize() => base.Initialize();

    public override void EnterState() => HandleStateEntry();

    public override void RunUpdate()
    {
        if (realignPlayer)
        {
            //player.transform.position = Vector3.Lerp(player.transform.position, 
        }

        //When do we leave puzzle state, only controlled by EndPuzzleEvent? 

    }

    public override void ExitState() => HandleExitState();

    private void HandleExitState()
    {
        base.ExitState();
        player.playerController3D.enabled = true;
        player.puzzleController.enabled = false;
    }

    private void HandleStateEntry()
    {
        base.EnterState();
        player.physics.velocity = Vector3.zero;
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
