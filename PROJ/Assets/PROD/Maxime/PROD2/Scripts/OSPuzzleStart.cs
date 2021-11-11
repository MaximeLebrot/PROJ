using UnityEngine;

public class OSPuzzleStart : MonoBehaviour
{
    [SerializeField] private OSPuzzle osPuzzle;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private MetaPlayerController player;

    private void Start()
    {
        if (osPuzzle == null)
            osPuzzle = GetComponent<OSPuzzle>();
        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MetaPlayerController>();

    }

    private void OnEnable()
    {
        EventHandler<ExitPuzzleEvent>.RegisterListener(osPuzzle.ExitOSPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(osPuzzle.StartOSPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(player.ChangeStateToOSPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(player.ChangeStateToOSWalk);
    }
    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(osPuzzle.ExitOSPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(osPuzzle.StartOSPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(player.ChangeStateToOSPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(player.ChangeStateToOSWalk);
    }

    private void OnEventCalled(StartPuzzleEvent eve)
    {
        osPuzzle.StartOSPuzzle(eve);
        player.ChangeStateToOSPuzzle(eve);
    }

    private void OnEventCalled(ExitPuzzleEvent eve)
    {
        osPuzzle.ExitOSPuzzle(eve);
        player.ChangeStateToOSWalk(eve);
    }
}

