using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Puzzle : MonoBehaviour
{
    //[SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] private int masterPuzzleID; 
    [SerializeField] private int nextPuzzleTimer; 
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
    private PuzzleParticles particles;
    public void SetPlayer(Transform t) { player = t; grid.Player = player; }

    void Awake()
    {
        particles = GetComponentInChildren<PuzzleParticles>();
        if (puzzleInstances.Count > 0)
        {
            SetupPuzzleInstances();
            currentPuzzleInstance = puzzleInstances[0];
            numOfPuzzles = puzzleInstances.Count;
            grid = GetComponentInChildren<PuzzleGrid>();
            grid.StartGrid();
            
            InitiatePuzzle();
            solution = Translate();
            
        }
        else
            Debug.LogWarning("NO PUZZLE INSTANCES IN PUZZLE");        
    }
    private void Start()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
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
        
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
    }
    private void OnDisable()
    {
        inputMaster.Disable();
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
    }
    private void SetupPuzzleInstances()
    {
        foreach (PuzzleInstance pi in puzzleInstances)
        {
            pi.SetupPuzzleInstance(this, masterPuzzleID);
        }

    }
    private void InitiatePuzzle()
    {
        //Debug.Log("Initiate puzzle");
        EventHandler<LoadPuzzleEvent>.FireEvent(new LoadPuzzleEvent(new PuzzleInfo(GetPuzzleID())));
        EventHandler<ResetPuzzleEvent>.FireEvent(new ResetPuzzleEvent(new PuzzleInfo(currentPuzzleInstance.GetPuzzleID(masterPuzzleID))));
        GetComponentInChildren<PuzzleStarter>().ResetStarter();
        grid.ResetGrid();

        if (currentPuzzleInstance.HasRestrictions())
            grid.SetRestrictions(currentPuzzleInstance.GetRestrictions());

        PlaceSymbols();

    }
    private void NextPuzzle()
    {
        UnloadSymbols();
        if(particles != null)
            particles.Play();

        currentPuzzleNum++;     

        //Debug.Log("Next puzzle, #" + currentPuzzleNum);
        if(currentPuzzleNum >= puzzleInstances.Count)
        {
            //no more puzzle instances here
            //N�TT SKA H�NDA H�R? n�n effekt och feedback p� att man klarat det h�r pusslet. Inte spara griden utan st�nga av griden typ
            //Exit puzzle
            //Debug.Log("Last puzzle instance completed");


            EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(currentPuzzleInstance.GetPuzzleID(masterPuzzleID)), true));
            grid.CompleteGrid();
            GetComponent<Collider>().enabled = false;
            return;
        }
        grid.ResetGrid();
        currentPuzzleInstance = puzzleInstances[currentPuzzleNum];
        //OnComplete instance       
        Invoke("InitiatePuzzle", nextPuzzleTimer);
    }

    #region Place Symbols
    private void PlaceSymbols()
    {
        if(instantiatedSymbols.Count > 0)
        {
            UnloadSymbols();
        }

        
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

        Vector3 midLeftPos = (Vector3.left * (symbolOffset / 2));
        Vector3 midRightPos = (Vector3.right * (symbolOffset / 2));

        instantiatedSymbols[midLeft].transform.localPosition = midLeftPos;
        instantiatedSymbols[midLeft].transform.localRotation = new Quaternion(0,0,0,0);

        instantiatedSymbols[midRight].transform.localPosition = midRightPos;
        instantiatedSymbols[midRight].transform.rotation = new Quaternion(0, 0, 0, 0);

        for (int i = 1; i <= midLeft; i++)
        {
            Vector3 tempPos = midLeftPos;
            tempPos -= i * (symbolOffset * Vector3.right);
            instantiatedSymbols[midLeft - i].transform.localPosition = tempPos;
            instantiatedSymbols[midLeft - i].transform.rotation = symbolPos.rotation;

            tempPos = midRightPos;
            tempPos += i * (symbolOffset * Vector3.right);
            instantiatedSymbols[midRight + i].transform.localPosition = tempPos;
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
            EventHandler<ClearPuzzleEvent>.FireEvent(new ClearPuzzleEvent(new PuzzleInfo(GetPuzzleID())));

            NextPuzzle();
        }
        else
        {
            InitiatePuzzle();
        }
        
        

    }

    private void UnloadSymbols()
    {
        foreach(PuzzleObject po in instantiatedSymbols)
        {
            po.Unload();
        }
        instantiatedSymbols.Clear();
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
            if (eve.info.ID == currentPuzzleInstance.GetPuzzleID(masterPuzzleID))
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
    public int GetPuzzleID() { return currentPuzzleInstance.GetPuzzleID(masterPuzzleID);}


}



