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
        EventHandler<ResetPuzzleEvent>.RegisterListener(ResetStarter);
    }

    private void OnDisable()
    {
        EventHandler<ResetPuzzleEvent>.UnregisterListener(ResetStarter);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PuzzleStarter Trigger entered");
        
        if(Active == false)
        {
            Debug.Log("Start Puzzle");
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzle.GetPuzzleID(), GetComponentInParent<Puzzle>().transform)));
            puzzle.SetPlayer(other.transform);
            Active = true;
        }
        

        //StartPuzzleEvent skickas även när pusslet är igång, fix plz.

    }

    public void ResetStarter(ResetPuzzleEvent eve)
    {
        Debug.Log("ResetStarter called with reset event");
        //if (eve.info.ID == puzzle.GetPuzzleID())
            Active = false;
    }

    

}