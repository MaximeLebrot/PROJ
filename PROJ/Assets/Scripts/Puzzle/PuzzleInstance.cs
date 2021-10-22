using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInstance : MonoBehaviour
{
    [SerializeField] private int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] public List<PuzzleObject> puzzleObjects = new List<PuzzleObject>();
    
    //How do we fetch the master puzzle object? 
    [SerializeField]private Puzzle puzzleObject;

    private void OnEnable()
    {
        EventHandler<EvaluateSolutionEvent>.RegisterListener(EvaluateSolution);
    }

    private void OnDisable()
    {
        EventHandler<EvaluateSolutionEvent>.UnregisterListener(EvaluateSolution);
    }

    private void EvaluateSolution(EvaluateSolutionEvent evaluationEvent)
    {
        Debug.Log("Instance recieved eval event");
        if(evaluationEvent.info.ID == puzzleID)
            puzzleObject.EvaluateSolution(puzzleObjects);
    }

    public int GetPuzzleID() { return puzzleID;  }
}
