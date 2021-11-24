using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //[SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] private int masterPuzzleID; 
    
    [SerializeField] private List<PuzzleInstance> puzzleInstances = new List<PuzzleInstance>();
    [SerializeField] private string playerInput = "";
    [SerializeField] protected string solution;
    [SerializeField] Transform symbolPos;
    
    protected PuzzleInstance currentPuzzleInstance;
    protected PuzzleTranslator translator = new PuzzleTranslator();
    public PuzzleGrid grid;

    private SymbolPlacer symbolPlacer;
 
    //should NOT be public, but ModularHintSystem currently relies on this List
    public List<PuzzleObject> placedSymbols = new List<PuzzleObject>();

    //track progress
    private PuzzleCounter puzzleCounter;
    private int numOfPuzzles;
    private int currentPuzzleNum = 0;

    private Transform player;
    private PuzzleParticles particles;
    private List<string> translations;

    public float NextPuzzleTimer { get; } = 2.5f;
    public void SetPlayer(Transform t) { player = t; grid.Player = player; }

    void Awake()
    {
        symbolPlacer = GetComponent<SymbolPlacer>();
        puzzleCounter = GetComponentInChildren<PuzzleCounter>();
        particles = GetComponentInChildren<PuzzleParticles>();
        if (puzzleInstances.Count > 0)
        {
            SetupPuzzleInstances();
            currentPuzzleInstance = puzzleInstances[0];
            numOfPuzzles = puzzleInstances.Count;
            puzzleCounter.GenerateMarkers(numOfPuzzles);
            grid = GetComponentInChildren<PuzzleGrid>();
            grid.StartGrid();
            
            InitiatePuzzle();
            //solution = Translate();
            
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
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitPuzzle);
        EventHandler<ResetPuzzleEvent>.RegisterListener(ResetPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
    }
    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitPuzzle);
        EventHandler<ResetPuzzleEvent>.UnregisterListener(ResetPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
    }
    private void SetupPuzzleInstances()
    {
        foreach (PuzzleInstance pi in puzzleInstances)
        {
            pi.SetupPuzzleInstance(this, masterPuzzleID);
        }

    }

    public virtual void InitiatePuzzle()
    {       
        EventHandler<LoadPuzzleEvent>.FireEvent(new LoadPuzzleEvent(new PuzzleInfo(GetPuzzleID())));
        
        GetComponentInChildren<PuzzleStarter>().ResetStarter();
        //grid.ResetGrid();

        currentPuzzleInstance.SetUpHazards();

        if (currentPuzzleInstance.HasRestrictions())
            grid.SetRestrictions(currentPuzzleInstance.GetRestrictions());

        PlaceSymbols();
        solution = Translate();
        translations = translator.GetTranslations();
    }
    protected virtual void NextPuzzle()
    {
        symbolPlacer.UnloadSymbols();
        currentPuzzleInstance.DestroyHazards();
        ResetClearanceVariables();


        if (particles != null)
            particles.Play();

        puzzleCounter.MarkedAsSolved(currentPuzzleNum);
        currentPuzzleNum++;     

        if(currentPuzzleNum >= puzzleInstances.Count)
        {
            symbolPlacer.UnloadSymbols();
            EventHandler<ActivatorEvent>.FireEvent(new ActivatorEvent(new PuzzleInfo(masterPuzzleID)));
            puzzleCounter.DeleteMarkers();
            CompletePuzzle();
            return;
        }

        grid.ResetGrid();
        currentPuzzleInstance = puzzleInstances[currentPuzzleNum];
    }

    private void CompletePuzzle()
    {
        Invoke("CompleteGrid", 2);
        EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(masterPuzzleID), true));
        GetComponent<Collider>().enabled = false;
    }

    private void CompleteGrid()
    {
        grid.CompleteGrid();
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
        if (placedSymbols.Count > 0)
            return translator.CalculateSolution(placedSymbols);
        else
        {
            Debug.LogWarning("SOLUTION EMPTY, NO INSTANTIATED SYMBOLS");
            return null;
        }         
    }
    public virtual bool EvaluateSolution()
    {

        if (solution.Equals(grid.GetSolution()))
        {
            //uppdaterar curr puzzle
            currentPuzzleInstance.Solve();
            EventHandler<SaveEvent>.FireEvent(new SaveEvent());
            EventHandler<ClearPuzzleEvent>.FireEvent(new ClearPuzzleEvent(new PuzzleInfo(GetPuzzleID())));

            NextPuzzle();
            return true;
        }

        return false;
    }


    private void OnTriggerExit(Collider other)
    {
        PuzzleInfo info = new PuzzleInfo(currentPuzzleInstance.GetPuzzleID());
        EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(info, false));
        GetComponentInChildren<PuzzleStarter>().ResetStarter();

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
    public int GetPuzzleID() { return currentPuzzleInstance.GetPuzzleID(); }


    private void ResetClearanceVariables()
    {
        solutionOffset = 0;
        translationIndex = 0;
        clearedSymbols.Clear();
    }

    private int solutionOffset = 0;
    private int translationIndex = 0;
    List<bool> clearedSymbols = new List<bool>();

    public void CheckIfClearedSymbol(string currentSolution) //currentSolution = what the player has drawn
    {


        //goes through each index of strings in translations
        for (int i = translationIndex; i < translations.Count; i++)
        {
            if(IsEqualRange(solutionOffset, translations[i].Length, currentSolution))
            {
                translationIndex++;
                solutionOffset += translations[i].Length;
                clearedSymbols.Add(true);
            }
            else
            {
                break;
            }

        }

        //Debug.Log(clearedSymbols.Count + " BOOLS");

        for (int i = 0; i < clearedSymbols.Count; i++)
        {
            if(placedSymbols[i].Active == false)
                placedSymbols[i].Activate();
        }

     }

    private bool IsEqualRange(int offset, int length, string currentSolution)
    {
        //Debug.Log(length);

        if (offset + length > currentSolution.Length)
        {
            //Debug.Log("LENGTH IS WRONG");
            return false;
        }

        Debug.Log("input: " + currentSolution.Substring(offset, length) + " translation: " + translations[translationIndex]);
        //Debug.Log("equal = " + currentSolution.Substring(offset, length).Equals(translations[translationIndex]));

        return currentSolution.Substring(offset, length).Equals(translations[translationIndex]);
        
    }

    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        if (eve.success != true)
        {
            if (eve.info.ID == currentPuzzleInstance.GetPuzzleID())
            {
                if(eve.success == false)
                {
                    symbolPlacer.UnloadSymbols();
                    grid.ResetGrid();
                }
                    
            }
        }

    }

    private void ResetPuzzle(ResetPuzzleEvent eve)
    {
        if (eve.info.ID == currentPuzzleInstance.GetPuzzleID())
        {
            symbolPlacer.UnloadSymbols();
            grid.ResetGrid();

        }
    }

    
    private void PlaceSymbols()
    {
        placedSymbols = symbolPlacer.PlaceSymbols(currentPuzzleInstance, symbolPos);
        /*if (instantiatedSymbols.Count > 0)
        {
            UnloadSymbols();
        }


        //Is this the way we want to fetch the list??
        foreach (SymbolModPair pair in currentPuzzleInstance.puzzleObjects)
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
            UnevenPlaceSymbols();*/
    }

     
}



