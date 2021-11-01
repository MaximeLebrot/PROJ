using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInstance : MonoBehaviour
{
    [SerializeField] private int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] public List<SymbolModPair> puzzleObjects = new List<SymbolModPair>();
    
    private Puzzle masterPuzzle;
    private bool currentState;

    public bool IsSolved() => currentState;

    private void Awake()
    {
        PuzzleDictionary.AddPuzzle(puzzleID);
    }

    private void OnEnable()
    {
        masterPuzzle = GetComponentInParent<Puzzle>();
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
            masterPuzzle.EvaluateSolution();
    }

    public int GetPuzzleID() { return puzzleID;  }

    public void Solve() 
    { 
        currentState = true; 
        PuzzleDictionary.SetState(puzzleID, currentState); 
    }
    
    public void Load()
    {
        currentState = PuzzleDictionary.GetState(puzzleID);
        Debug.Log(puzzleID + " load ");
    }
}

[System.Serializable]
public class SymbolModPair
{
    public PuzzleObject symbol;
    public ModifierVariant modifier;
}
