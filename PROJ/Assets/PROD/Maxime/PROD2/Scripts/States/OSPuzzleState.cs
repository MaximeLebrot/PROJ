using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSPuzzleState")]
public class OSPuzzleState : PlayerState
{
    private bool realignPlayer;
    private Transform puzzleTransform;
    private Vector3 offset = new Vector3(0, 1.1f, 0);

    private Vector2 playerXZ = new Vector2();
    private Vector2 puzzleXZ = new Vector2();
    public override void Initialize() => base.Initialize();

    public override void EnterState() => HandleStateEntry();

    public override void ExitState() => HandleExitState();

    public override void RunUpdate()
    {
        HandleStartingAlignment();

        //When do we leave puzzle state, only controlled by EndPuzzleEvent? 
    }

    private void HandleStartingAlignment()
    {
        UpdateVector2s();
        if (Vector2.Distance(playerXZ, puzzleXZ) < 0.1f && realignPlayer == true)
        {
            Debug.Log("realigned");
            realignPlayer = false;
            player.physics.velocity = Vector3.zero;
        }
        if (realignPlayer)
            player.transform.position = Vector3.Lerp(player.transform.position, puzzleTransform.position + offset, Time.deltaTime * 5);
    }

    private void UpdateVector2s()
    {
        playerXZ.x = player.transform.position.x;
        playerXZ.y = player.transform.position.z;
    }

    private void HandleExitState()
    {
        base.ExitState();
        Debug.Log("PuzzleStateExit");
        player.playerController3D.enabled = true;
        player.puzzleController.enabled = false;
    }

    private void HandleStateEntry()
    {
        base.EnterState();
        Debug.Log("Puzzle state");
        realignPlayer = true;
        player.physics.velocity = Vector3.zero;
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
    }

    private void GetPuzzleInfo(StartPuzzleEvent eve)
    {
        puzzleTransform = eve.info.puzzlePos;
        puzzleXZ.x = puzzleTransform.position.x;
        puzzleXZ.y = puzzleTransform.position.z;
    }
    private void OnEnable() => EventHandler<StartPuzzleEvent>.RegisterListener(GetPuzzleInfo);

    private void OnDisable() => EventHandler<StartPuzzleEvent>.UnregisterListener(GetPuzzleInfo);
}
