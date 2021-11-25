using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnorderedPuzzle : Puzzle
{
    [SerializeField]private List<string> translationsSorted = new List<string>();

    public override bool EvaluateSolution()
    {
        if (CheckWhichSymbolsAreCleared(grid.GetSolution()))
        {
            currentPuzzleInstance.Solve();
            EventHandler<SaveEvent>.FireEvent(new SaveEvent());
            EventHandler<ClearPuzzleEvent>.FireEvent(new ClearPuzzleEvent(new PuzzleInfo(GetPuzzleID())));

            NextPuzzle();
            return true;
        }
        return false;
    }
    public override void InitiatePuzzle()
    {
        base.InitiatePuzzle();
        SortTranslations(translations);
    }
    private int playerSolutionOffset = 0;
    public override void CheckIfClearedSymbol(string currentSolution)
    {
        //nada
    }
    private bool CheckWhichSymbolsAreCleared(string currentSolution)
    {
        string currentSolutionCopy = currentSolution;

        foreach (string translation in translationsSorted)
        {
            if (currentSolutionCopy.Contains(translation))
            {
                //make symbol glow
                //remove it from comparison string
                currentSolutionCopy = currentSolutionCopy.Remove(currentSolutionCopy.IndexOf(translation), translation.Length);
                //Debug.Log(currentSolutionCopy[currentSolutionCopy.IndexOf(translation)]+ ", length : " + translation.Length);
            }
        }
        Debug.Log("currentSolutionCopy length is:" + currentSolutionCopy.Length);
        Debug.Log("currentSolution length is:" + currentSolution.Length);
        Debug.Log("solution length is:" + solution.Length);
        if (currentSolution.Length == solution.Length && currentSolutionCopy.Length == 0)
        {
          
            Debug.Log("should return true to evaluate solution");
            return true;
        }
        return false;

    }
    //called when we initiate a puzzle, probably
    private void SortTranslations(List<string> listToSort)
    {
        listToSort.Sort((a, b) => b.Length.CompareTo(a.Length));
        translationsSorted = listToSort;
    }

}
