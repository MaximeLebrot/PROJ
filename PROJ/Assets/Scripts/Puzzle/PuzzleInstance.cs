using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInstance : MonoBehaviour
{
    [SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] public List<PuzzleObject> puzzleObjects = new List<PuzzleObject>();
    [SerializeField] protected string solution;

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
        puzzleObject.EvaluateSolution(puzzleObjects);
    }

}
