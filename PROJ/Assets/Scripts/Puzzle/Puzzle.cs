using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT

    [SerializeField] public List<PuzzleObject> puzzleObjects = new List<PuzzleObject>();
    [SerializeField] protected string solution;

    protected PuzzleTranslator translator = new PuzzleTranslator();
    protected void Translate(List<PuzzleObject> objects) { solution = translator.CalculateSolution(puzzleObjects); }

}



