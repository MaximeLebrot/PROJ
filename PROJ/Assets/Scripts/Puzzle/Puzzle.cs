using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Puzzle : MonoBehaviour
{
    [SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT

    [SerializeField] public List<PuzzleObject> puzzleObjects = new List<PuzzleObject>();
    [SerializeField] public List<PuzzleObject> instatiatedSymbols = new List<PuzzleObject>();
    [SerializeField] protected string solution;
    [SerializeField] Transform symbolPos;
    [SerializeField] int symbolOffset;
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
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitPuzzle);
    }

    private void OnDisable()
    {
        inputMaster.Disable();
        EventHandler<EvaluateSolutionEvent>.UnregisterListener(EvaluateSolution);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitPuzzle);
    }



    protected void Translate(List<PuzzleObject> objects)
    {
        

        solution = translator.CalculateSolution(puzzleObjects);
    }

    private void PlaceSymbols()
    {
        foreach (PuzzleObject obj in instatiatedSymbols)
        {
            Destroy(obj.gameObject);
        }

        instatiatedSymbols.Clear();

        foreach(PuzzleObject obj in puzzleObjects)
        {
            instatiatedSymbols.Add(Instantiate(obj));
        }

        if (instatiatedSymbols.Count % 2 == 0)
            EvenPlaceSymbols();
        else
            UnevenPlaceSymbols();
    }

    private void UnevenPlaceSymbols()
    {
        
        int mid = instatiatedSymbols.Count / 2;
        instatiatedSymbols[mid].transform.position = symbolPos.position;
        instatiatedSymbols[mid].transform.rotation = symbolPos.rotation;

        for(int i = 1; i <= mid; i++)
        {
            Vector3 tempPos = symbolPos.position;
            tempPos.x -= i * symbolOffset;
            instatiatedSymbols[mid - i].transform.position = tempPos;
            instatiatedSymbols[mid - i].transform.rotation = symbolPos.rotation;

            
            tempPos = symbolPos.position;
            tempPos.x += i * symbolOffset;
            instatiatedSymbols[mid + i].transform.position = tempPos;
            instatiatedSymbols[mid + i].transform.rotation = symbolPos.rotation;
        }
    }

    private void EvenPlaceSymbols()
    {
       

        int midRight = instatiatedSymbols.Count / 2;
        int midLeft = midRight - 1;

        Vector3 midLeftPos = symbolPos.position - (Vector3.right * (symbolOffset / 2));
        Vector3 midRightPos = symbolPos.position + (Vector3.right * (symbolOffset / 2));

        instatiatedSymbols[midLeft].transform.position = midLeftPos;
        instatiatedSymbols[midLeft].transform.rotation = symbolPos.rotation;

        instatiatedSymbols[midRight].transform.position = midRightPos;
        instatiatedSymbols[midRight].transform.rotation = symbolPos.rotation;

        for (int i = 1; i <= midLeft; i++)
        {
            Vector3 tempPos = midLeftPos;
            tempPos.x -= i * symbolOffset;
            instatiatedSymbols[midLeft - i].transform.position = tempPos;
            instatiatedSymbols[midLeft - i].transform.rotation = symbolPos.rotation;

            Debug.Log("TEMPpos:  " + tempPos + "MIDLEFTpos:  " + midLeftPos);
            tempPos = midRightPos;
            tempPos.x += i * symbolOffset;
            instatiatedSymbols[midRight + i].transform.position = tempPos;
            instatiatedSymbols[midRight + i].transform.rotation = symbolPos.rotation;
        }
    }

    private void Update()
    {
        //SHOULD BE IN PLAYER FOR WHEN THEY WANT TO EVALUATE PUZZLE
        if (inputMaster.PuzzleDEBUGGER.calculatesolution.triggered)
        {
            EventHandler<EvaluateSolutionEvent>.FireEvent(new EvaluateSolutionEvent(new PuzzleInfo(puzzleID)));
        }

        if(inputMaster.Player.Interact.triggered)
            PlaceSymbols();

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
        //Should be in OnEnable but is here for Development and debugging
        solution = "";
        Translate(puzzleObjects);

        Debug.Log("Solution: " + solution + " INPUT : " + grid.GetSolution());
        if (solution.Equals(grid.GetSolution()))
        {
            
            EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(puzzleID), true));
            grid.CompleteGrid();
        }
        else
        {
            
            EventHandler<ResetPuzzleEvent>.FireEvent(new ResetPuzzleEvent(new PuzzleInfo(puzzleID)));
            grid.ResetGrid();
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        PuzzleInfo info = new PuzzleInfo(puzzleID);
        EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(info, false));
    }

    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        if(eve.success != true)
        {
            if (eve.info.ID == puzzleID)
            {
                grid.ResetGrid();
            }
        }
        
    }

    public int GetPuzzleID() { return puzzleID; }

}



