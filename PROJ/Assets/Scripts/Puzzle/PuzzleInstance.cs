using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInstance : MonoBehaviour
{
    [SerializeField] private int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] public List<SymbolModPair> puzzleObjects = new List<SymbolModPair>();

    [SerializeField] private List<Vector2Int> activeNodes;
    [SerializeField] private List<Hazard> hazards;

    private Puzzle masterPuzzle;
    private bool currentState;
    private List<Hazard> instantiatedHazards = new List<Hazard>();

    public bool IsSolved() => currentState;

    private void Awake()
    {
        PuzzleDictionary.AddPuzzle(puzzleID);
    }

    

    public void SetupPuzzleInstance(Puzzle puzzle, int masterPuzzleID)
    {
        masterPuzzle = puzzle;
        puzzleID = int.Parse(masterPuzzleID.ToString() + puzzleID.ToString());
    }
    public int GetPuzzleID()
    {
        return puzzleID;  
    }

    public void Solve() 
    { 
        //Send Event to do something?
        currentState = true; 
        PuzzleDictionary.SetState(puzzleID, currentState); 
    }
    
    public void Load()
    {
        currentState = PuzzleDictionary.GetState(puzzleID);
        Debug.Log("Load puzzle " + puzzleID + "    STATE:: " + currentState);
    }


    public void SetUpHazards()
    {
        if(hazards.Count > 0)
        {
            foreach (Hazard h in hazards)
            {
                Hazard instance = Instantiate(h, transform).GetComponent<Hazard>();
                instantiatedHazards.Add(instance);
                instance.StartHazard(GetPuzzleID());
            }
        }
    }

    public void DestroyHazards()
    {
        if (hazards.Count > 0)
        {
            foreach (Hazard h in instantiatedHazards)
            {
                h.DeleteHazardObjects();
                Destroy(h.gameObject);
            }
        }
    }
  

    public bool HasRestrictions()
    {
        return activeNodes.Count > 0;
    }
    public List<Vector2Int> GetRestrictions()
    {
        return activeNodes;
    }
}

[System.Serializable]
public class SymbolModPair
{
    public PuzzleObject symbol;
    public ModifierVariant modifier;
    
}
