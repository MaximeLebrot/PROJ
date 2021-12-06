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
    protected List<TranslationAndObject> translations;

    public PuzzleGrid grid;

    private SymbolPlacer symbolPlacer;
 
    //should NOT be public, but ModularHintSystem currently relies on this List
    public List<PuzzleObject> placedSymbols = new List<PuzzleObject>();
    [SerializeField] private List<TranslationAndObject> translationsSorted = new List<TranslationAndObject>();

    //track progress
    private PuzzleCounter puzzleCounter;
    private int numOfPuzzles;
    private int currentPuzzleNum = 0;

    private Transform player;
    private PuzzleParticles particles;

    public float NextPuzzleTimer { get; } = 2.5f;
    public void SetPlayer(Transform t) { player = t; grid.Player = player; }

    private FMOD.Studio.EventInstance PuzzleSolved;

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
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnExitPuzzle);
        EventHandler<ResetPuzzleEvent>.RegisterListener(OnResetPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
    }
    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnExitPuzzle);
        EventHandler<ResetPuzzleEvent>.UnregisterListener(OnResetPuzzle);
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
        PuzzleSolved = FMODUnity.RuntimeManager.CreateInstance("event:/Game/PuzzleSolved");
        PuzzleSolved.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PuzzleSolved.start();
        PuzzleSolved.release();
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

    }

    
    protected List<bool> clearedSymbols = new List<bool>();

    public virtual void CheckIfClearedSymbol(string currentSolution) //currentSolution = what the player has drawn
    {
        Debug.Log("CHECK");

        int solutionOffset = 0;

        //Checks for empty solution
        if(currentSolution.Length > 0 == false)
        {
            foreach(var pair in translations)
            {
                pair.pObj.Activate(false);
            }
        }

        //goes through each index of strings in translations
        for (int i = 0; i < translations.Count; i++)
        {

            if (IsEqualRange(solutionOffset, translations[i].translation.Length, currentSolution, i))
            {
                solutionOffset += translations[i].translation.Length;

                translations[i].pObj.Activate(true);
            }
            else
            {
                
                translations[i].pObj.Activate(false);
            }

        }


        //ApplyClearedSymbols();

    }


    private bool IsEqualRange(int offset, int length, string currentSolution, int translationIndex)
    {
        //Debug.Log(length);

        if (offset + length > currentSolution.Length)
        {
            //Debug.Log("LENGTH IS WRONG");
            return false;
        }

        Debug.Log("input: " + currentSolution.Substring(offset, length) + " translation: " + translations[translationIndex].translation);
        //Debug.Log("equal = " + currentSolution.Substring(offset, length).Equals(translations[translationIndex]));

        return currentSolution.Substring(offset, length).Equals(translations[translationIndex].translation);
        
    }

    public void OnExitPuzzle(ExitPuzzleEvent eve)
    {
        if (eve.success != true)
        {
            if (eve.info.ID == currentPuzzleInstance.GetPuzzleID())
            {
                if(eve.success == false)
                {
                    ResetPuzzle();
                }
                    
            }
        }
    }


    //To manage the number of times ResetPuzzle is subscribed to its event, quick fix dont judge pls
    private bool registered = true;
    private void OnResetPuzzle(ResetPuzzleEvent eve)
    {

        if (eve.info.ID == currentPuzzleInstance.GetPuzzleID())
        {
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        EventHandler<ResetPuzzleEvent>.UnregisterListener(OnResetPuzzle);
        registered = false;
        Debug.Log("Reset puzzle called");

        symbolPlacer.UnloadSymbols();
        grid.ResetGrid();
    }

    public void RegisterToResetPuzzleEvent()
    {
        if (registered)
            return;

        EventHandler<ResetPuzzleEvent>.RegisterListener(OnResetPuzzle);
        registered = true;
    }
    
    private void PlaceSymbols()
    {
        placedSymbols = symbolPlacer.PlaceSymbols(currentPuzzleInstance, symbolPos);
        
    }

    protected void SortTranslations(List<TranslationAndObject> listToSort)
    {
        listToSort.Sort((a, b) => b.translation.Length.CompareTo(a.translation.Length));
        translationsSorted = listToSort;
    }

}

public class TranslationAndObject
{
    public string translation;
    public PuzzleObject pObj;
}



