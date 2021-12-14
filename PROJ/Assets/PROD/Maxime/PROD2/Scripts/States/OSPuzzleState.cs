using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSPuzzleState")]
public class OSPuzzleState : PlayerState
{
    private bool realignPlayer;
    private Transform puzzleTransform;

    public override void Initialize() => base.Initialize();

    public override void EnterState() => HandleStateEntry();

    public override void RunUpdate()
    {
        if (realignPlayer)
            player.transform.position = Vector3.Lerp(player.transform.position, puzzleTransform.position, Time.deltaTime * 5);
        if (Vector3.Distance(player.transform.position, puzzleTransform.position) < 0.1f && realignPlayer == true)
            realignPlayer = false;

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
        realignPlayer = true;
        player.physics.velocity = Vector3.zero;
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
    }

    private void GetPuzzleInfo(StartPuzzleEvent eve)
    {
        puzzleTransform = eve.info.puzzlePos;
    }

    private void OnEnable()
    {
        EventHandler<StartPuzzleEvent>.RegisterListener(GetPuzzleInfo);
    }

    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(GetPuzzleInfo);
    }
}
