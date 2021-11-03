using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Puzzle : MonoBehaviour
{
    //[SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] private List<PuzzleInstance> puzzleInstances = new List<PuzzleInstance>();
    [SerializeField] private string playerInput = "";
    [SerializeField] private string solution;
    private PuzzleInstance currentPuzzleInstance;

    protected PuzzleTranslator translator = new PuzzleTranslator();

    private InputMaster inputMaster;
    private PuzzleGrid grid;

    //Draw symbols
    [SerializeField] public List<PuzzleObject> instantiatedSymbols = new List<PuzzleObject>();
    [SerializeField] Transform symbolPos;
    [SerializeField] int symbolOffset;


    private int numOfPuzzles;
    private int currentPuzzleNum = 0;
    private int numOfFinishedPuzzles = 0;

    private Transform player;

    public void SetPlayer(Transform t) { player = t; grid.Player = player; }

    void Awake()
    {
        
        if (puzzleInstances.Count > 0)
        {
            currentPuzzleInstance = puzzleInstances[0];
            numOfPuzzles = puzzleInstances.Count;
            grid = GetComponentInChildren<PuzzleGrid>();
            inputMaster = new InputMaster();
            PlaceSymbols();
            solution = Translate();
            
        }
        else
            Debug.LogWarning("NO PUZZLE INSTANCES IN PUZZLE");

        
    }

    public void Load()
    {
        currentPuzzleNum = 0;
        currentPuzzleInstance = puzzleInstances[currentPuzzleNum];
        grid.ResetGrid();
        PlaceSymbols();
        CheckSolvedPuzzles();
    }

    private void CheckSolvedPuzzles()
    {
        while(currentPuzzleInstance.IsSolved() && 
            currentPuzzleNum + 1 <= puzzleInstances.Count)
        {
            Debug.Log("LOAD NEXT PUZZLE");
            NextPuzzle();
        }

    }

    private void OnEnable()
    {
        inputMaster.Enable();
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
    }
    private void OnDisable()
    {
        inputMaster.Disable();
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
    }

   private void InitiatePuzzle()
    {
        //Debug.Log("Initiate puzzle");

        grid.ResetGrid();
        if(currentPuzzleInstance.HasRestrictions())
            grid.SetRestrictions();
        PlaceSymbols();
    }
    private void NextPuzzle()
    {
        

        currentPuzzleNum++;     

        //Debug.Log("Next puzzle, #" + currentPuzzleNum);
        if(currentPuzzleNum >= puzzleInstances.Count)
        {
            //no more puzzle instances here
            //NÅTT SKA HÄNDA HÄR? nån effekt och feedback på att man klarat det här pusslet. Inte spara griden utan stänga av griden typ
            //Exit puzzle
            //Debug.Log("Last puzzle instance completed");


            EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(currentPuzzleInstance.GetPuzzleID()), true));
            grid.CompleteGrid();
            GetComponent<Collider>().enabled = false;
            return;
        }

        currentPuzzleInstance = puzzleInstances[currentPuzzleNum];
        //OnComplete instance       
        InitiatePuzzle();
    }

    #region Place Symbols
    private void PlaceSymbols()
    {
        for(int i = 0; i <instantiatedSymbols.Count; i++)
        {
            Destroy(instantiatedSymbols[i].gameObject);
        }

        instantiatedSymbols.Clear();
        //Is this the way we want to fetch the list??
        foreach(SymbolModPair pair in currentPuzzleInstance.puzzleObjects)
        {
            GameObject instance = Instantiate(pair.symbol).gameObject;
            
            PuzzleObject objectInstance = instance.GetComponent<PuzzleObject>();
            instantiatedSymbols.Add(objectInstance);
            objectInstance.transform.parent = symbolPos;
            objectInstance.SetModifier(pair.modifier);

        }

        if (instantiatedSymbols.Count % 2 == 0)
            EvenPlaceSymbols();
        else
            UnevenPlaceSymbols();
    }

    //DONT EVEN LOOK IN HERE
    private void UnevenPlaceSymbols()
    {
        
        int mid = instantiatedSymbols.Count / 2;
        instantiatedSymbols[mid].transform.position = symbolPos.position;
        instantiatedSymbols[mid].transform.rotation = symbolPos.rotation;

        for(int i = 1; i <= mid; i++)
        {
            Vector3 tempPos = symbolPos.position;
            tempPos -= i * (symbolOffset * Vector3.right);
            instantiatedSymbols[mid - i].transform.position = tempPos;
            instantiatedSymbols[mid - i].transform.rotation = symbolPos.rotation;
            instantiatedSymbols[mid - i].transform.localPosition = 
                new Vector3(instantiatedSymbols[mid - i].transform.localPosition.x, 0, 0);



            tempPos = symbolPos.position;
            tempPos += i * (symbolOffset * Vector3.right);
            instantiatedSymbols[mid + i].transform.position = tempPos;
            instantiatedSymbols[mid + i].transform.rotation = symbolPos.rotation;
            instantiatedSymbols[mid + i].transform.localPosition = 
                new Vector3(instantiatedSymbols[mid + i].transform.localPosition.x, 0, 0);
        }
    }

    private void EvenPlaceSymbols()
    {
       

        int midRight = instantiatedSymbols.Count / 2;
        int midLeft = midRight - 1;

        Vector3 midLeftPos = symbolPos.position - (Vector3.right * (symbolOffset / 2));
        Vector3 midRightPos = symbolPos.position + (Vector3.right * (symbolOffset / 2));

        instantiatedSymbols[midLeft].transform.position = midLeftPos;
        instantiatedSymbols[midLeft].transform.rotation = symbolPos.rotation;

        instantiatedSymbols[midRight].transform.position = midRightPos;
        instantiatedSymbols[midRight].transform.rotation = symbolPos.rotation;

        for (int i = 1; i <= midLeft; i++)
        {
            Vector3 tempPos = midLeftPos;
            tempPos -= i * (symbolOffset * Vector3.right);
            instantiatedSymbols[midLeft - i].transform.position = tempPos;
            instantiatedSymbols[midLeft - i].transform.rotation = symbolPos.rotation;

            tempPos = midRightPos;
            tempPos += i * (symbolOffset * Vector3.right);
            instantiatedSymbols[midRight + i].transform.position = tempPos;
            instantiatedSymbols[midRight + i].transform.rotation = symbolPos.rotation;
        }
    }
    #endregion    

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

    private string Translate()
    {
        if (instantiatedSymbols.Count > 0)
            return translator.CalculateSolution(instantiatedSymbols);
        else
        {
            Debug.LogWarning("SOLUTION EMPTY, NO INSTANTIATED SYMBOLS");
            return null;
        }
            
    }
    public void EvaluateSolution()
    {
        //Debug.Log("EvaluateSolution Called");
        //Should be in OnEnable but is here for Development and debugging
        solution = Translate();

        //Debug.Log("Solution: " + solution + " INPUT : " + grid.GetSolution());
        //  was the solution correct? 
        if (solution.Equals(grid.GetSolution()))
        {
            //uppdaterar curr puzzle
            currentPuzzleInstance.Solve();
            EventHandler<SaveEvent>.FireEvent(new SaveEvent());
            NextPuzzle();
        }
        else
        {
            grid.ResetGrid();
        }
        
        EventHandler<ResetPuzzleEvent>.FireEvent(new ResetPuzzleEvent(new PuzzleInfo(currentPuzzleInstance.GetPuzzleID())));
        GetComponentInChildren<PuzzleStarter>().ResetStarter();

    }

    private void OnTriggerExit(Collider other)
    {

        //PuzzleInfo info = new PuzzleInfo(currentPuzzleInstance.GetPuzzleID());
        //EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(info, false));
        grid.ResetGrid();
        GetComponentInChildren<PuzzleStarter>().ResetStarter();

    }

    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        if(eve.success != true)
        {
            if (eve.info.ID == currentPuzzleInstance.GetPuzzleID())
            {
                grid.ResetGrid();
            }
        }
        
    }
    public void StartPuzzle(StartPuzzleEvent eve)
    {
        //Maybe this is dumb, ID comes from PuzzleInstance, but should technically be able to identify itself like this
        //Debug.Log("Start puzzle event, id is :" + GetPuzzleID());
        if (eve.info.ID == GetPuzzleID())
        {
            //Debug.Log("id match" + GetPuzzleID());
            grid.StartPuzzle();
        }
    }

    public string GetSolution()
    {
        return solution;
    }

    //Maybe return ID from current PuzzleInstance instead
    public int GetPuzzleID() { return currentPuzzleInstance.GetPuzzleID();}


}



