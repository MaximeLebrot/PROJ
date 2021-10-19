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

    void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    protected void Translate(List<PuzzleObject> objects) { solution = translator.CalculateSolution(puzzleObjects); }

    private void Update()
    {
        
        if (inputMaster.PuzzleDEBUGGER.calculatesolution.triggered)
        {
            solution = "";
            Translate(puzzleObjects);
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

}



