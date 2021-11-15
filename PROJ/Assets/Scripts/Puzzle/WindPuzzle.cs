using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WindPuzzle : Puzzle
{

    [SerializeField] private VisualEffect wind;
    [SerializeField] private Animator windBurst;
    [SerializeField] List<GameObject> windMarkers;

    public override void InitiatePuzzle()
    {
        windMarkers[currentPuzzleInstance.GetComponent<WindPuzzleInstance>().GetWindRotations()].GetComponentInChildren<Animator>().SetTrigger("on");
        //Start Wind
        wind.Play();
        wind.SetVector3("Direction", currentPuzzleInstance.GetComponent<WindPuzzleInstance>().GetWindDirection());
        windBurst.SetTrigger("burst");
        base.InitiatePuzzle();

        //adjust solution for wind
        string newString = solution;
        //Debug.Log(currentPuzzleInstance.GetComponent<WindPuzzleInstance>().GetWindRotations());
        for (int j = 0; j < currentPuzzleInstance.GetComponent<WindPuzzleInstance>().GetWindRotations(); j++)
        {
            newString = PuzzleHelper.RotateSymbolsOneStep(newString);
        }

        solution = newString;
        Debug.Log("THE SOLUTION IS : " + solution);
    }

    public override bool EvaluateSolution()
    {
        

        //Debug.Log(solution);
        if (solution.Equals(grid.GetSolution()))
        {

            windMarkers[currentPuzzleInstance.GetComponent<WindPuzzleInstance>().GetWindRotations()].GetComponentInChildren<Animator>().SetTrigger("off");
            
            //uppdaterar curr puzzle
            currentPuzzleInstance.Solve();
            EventHandler<SaveEvent>.FireEvent(new SaveEvent());
            EventHandler<ClearPuzzleEvent>.FireEvent(new ClearPuzzleEvent(new PuzzleInfo(GetPuzzleID())));

            NextPuzzle();
            return true;
        }

        return false;
    }




    protected override void NextPuzzle()
    {
        wind.Stop();
        base.NextPuzzle();
    }
}
