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

    private void OnTriggerEnter(Collider other)
    {

        if (Active == false)
        {
            puzzle.GetComponent<SphereCollider>().enabled = true;
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzle.GetPuzzleID(), GetComponentInParent<Puzzle>())));
            puzzle.SetPlayer(other.transform);
            Active = true;
        }


    }


    public void ResetStarter()
    {
        //Debug.Log("RESET STARTER");
            Active = false;
    }


}