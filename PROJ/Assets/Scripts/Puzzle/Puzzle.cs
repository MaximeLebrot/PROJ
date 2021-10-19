using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT

    [SerializeField] public List<PuzzleObject> puzzleObjects = new List<PuzzleObject>();
    [SerializeField] protected string solution;
    [SerializeField] private string playerInput = "";

    protected PuzzleTranslator translator = new PuzzleTranslator(); 
    
    private InputMaster inputMaster;
    private PuzzleGrid grid;

    void Awake()
    {
        grid = GetComponentInChildren<PuzzleGrid>();
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
        EventHandler<EvaluateSolutionEvent>.RegisterListener(EvaluateSolution);
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }



    protected void Translate(List<PuzzleObject> objects) { solution = translator.CalculateSolution(puzzleObjects); }

    private void Update()
    {
        //SHOULD BE IN PLAYER FOR WHEN THEY WANT TO EVALUATE PUZZLE
        if (inputMaster.PuzzleDEBUGGER.calculatesolution.triggered)
        {
            EventHandler<EvaluateSolutionEvent>.FireEvent(new EvaluateSolutionEvent());
        }
        
    }

    public void AddInput(char c)
    {
        playerInput += c;
    }
    public void RemoveInput()
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < playerInput.Length - 1; i++)
        {
            sb.Append(playerInput[i]);
        }
    }

    public void EvaluateSolution(EvaluateSolutionEvent eve)
    {
        //Should be in OnEnable
        solution = "";
        Translate(puzzleObjects);

        if (solution.Equals(grid.GetSolution()))
        {
            Debug.Log("WIN");//Fire EndPuzzleEvent
        }
        else
            Debug.Log("LOSER");//Fire ResetPuzzleEvent
    }

}



