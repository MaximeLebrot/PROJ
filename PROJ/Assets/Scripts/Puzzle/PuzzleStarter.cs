using UnityEngine;

public class PuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    public bool Active { get; set; }

    private void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }   
    private void OnTriggerEnter(Collider other)
    {
        if(Active == false)
        {
            //Debug.Log("Start Puzzle");
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzleID, GetComponentInParent<Puzzle>().transform)));
            puzzle.SetPlayer(other.transform);
            Active = true;
        }
        

        //StartPuzzleEvent skickas även när pusslet är igång, fix plz.

    }

    

}