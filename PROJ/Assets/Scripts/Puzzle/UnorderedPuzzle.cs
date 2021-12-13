using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnorderedPuzzle : Puzzle
{
    [SerializeField]private List<TranslationAndObject> translationsSorted = new List<TranslationAndObject>();

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

        foreach (TranslationAndObject pair in translationsSorted)
        {
            if (currentSolutionCopy.Contains(pair.translation))
            {
                //make symbol glow
                //if(settings.showClearedSymbols)
                pair.pObj.Activate(true);
                //remove it from comparison string
                currentSolutionCopy = currentSolutionCopy.Remove(currentSolutionCopy.IndexOf(pair.translation), pair.translation.Length);
                //Debug.Log(currentSolutionCopy[currentSolutionCopy.IndexOf(translation)]+ ", length : " + translation.Length);
            }
            else
            {
                //if(settings.showClearedSymbols)
                pair.pObj.Activate(false);
            }
            
        }

        

        if (currentSolution.Length == solution.Length && currentSolutionCopy.Length == 0)
        {
          
            //Debug.Log("should return true to evaluate solution");
            return true;
        }
        return false;

    }



    //called when we initiate a puzzle, probably
    private void SortTranslations(List<TranslationAndObject> listToSort)
    {
        listToSort.Sort((a, b) => b.translation.Length.CompareTo(a.translation.Length));
        translationsSorted = listToSort;
    }

}
