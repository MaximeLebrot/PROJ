using UnityEngine;

public class PuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    public bool Active;

    private void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }

    private void OnEnable()
    {
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnExit);
        EventHandler<ResetPuzzleEvent>.RegisterListener(ResetStarter);
    }

    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnExit);
        EventHandler<ResetPuzzleEvent>.UnregisterListener(ResetStarter);
    }

    
    private void OnExit(ExitPuzzleEvent eve)
    {
        ResetStarter();
    }

    public void ResetStarter(ResetPuzzleEvent eve)
    {
        ResetStarter();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (Active == false)
        {
            //Debug.Log("PuzzleStarter Trigger entered");
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzle.GetPuzzleID(), GetComponentInParent<Puzzle>().transform)));
            puzzle.SetPlayer(other.transform);
            Active = true;
        }


    }


    public void ResetStarter()
    {
            Active = false;
    }


}