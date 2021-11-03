using UnityEngine;

public class PuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    public bool Active;

    //[SerializeField] private AudioSource source;
    //[SerializeField] private GameObject enderText;
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
        //Debug.Log("ResetStarter called with reset event");
        //if (eve.info.ID == puzzle.GetPuzzleID())
        ResetStarter();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (Active == false)
        {
            Debug.Log("PuzzleStarter Trigger entered");
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzle.GetPuzzleID(), GetComponentInParent<Puzzle>().transform)));
            puzzle.SetPlayer(other.transform);
            Active = true;
            /*
            source.Play(); //S
            if (enderText != null)
                enderText.SetActive(true); //S
            */
        }


        //StartPuzzleEvent skickas även när pusslet är igång, fix plz.

    }


    public void ResetStarter()
    {
        
            Active = false;
        /*
            if (enderText != null)
                enderText.SetActive(false);
        */

    }


}