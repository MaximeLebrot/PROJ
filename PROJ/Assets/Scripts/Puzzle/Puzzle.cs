using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Puzzle : MonoBehaviour
{
    //[SerializeField] int puzzleID; //should be compared to solution on a EvaluatePuzzleEvent and fire a SUCCESS EVENT or FAIL EVENT
    [SerializeField] private int masterPuzzleID; 
    
    [SerializeField] private List<PuzzleInstance> puzzleInstances = new List<PuzzleInstance>();
    [SerializeField] private string playerInput = "";
    [SerializeField] protected string solution;
    protected PuzzleInstance currentPuzzleInstance;

    protected PuzzleTranslator translator = new PuzzleTranslator();
    [SerializeField] private SymbolPlacer symbolPlacer;

    //private InputMaster inputMaster;
    private PuzzleCounter puzzleCounter;
    protected PuzzleGrid grid;

    [SerializeField] Transform symbolPos;
    //Draw symbols
    //should NOT be public, but ModularHintSystem currently relies on this List
    public List<PuzzleObject> placedSymbols = new List<PuzzleObject>();


    //FKIN TECH LEEEEEED
    private int numOfPuzzles;
    private int currentPuzzleNum = 0;
    private int numOfFinishedPuzzles = 0;

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
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
    }
    private void OnDisable()
    {
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

    public virtual void InitiatePuzzle()
    {       
        EventHandler<LoadPuzzleEvent>.FireEvent(new LoadPuzzleEvent(new PuzzleInfo(GetPuzzleID())));
        
        GetComponentInChildren<PuzzleStarter>().ResetStarter();
        //grid.ResetGrid();

        if (currentPuzzleInstance.HasRestrictions())
            grid.SetRestrictions(currentPuzzleInstance.GetRestrictions());

        PlaceSymbols();
        solution = Translate();
        translations = translator.GetTranslations();
    }
    protected virtual void NextPuzzle()
    {
        symbolPlacer.UnloadSymbols();
        
        if(particles != null)
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

        //PuzzleInfo info = new PuzzleInfo(currentPuzzleInstance.GetPuzzleID());
        //EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(info, false));
        grid.ResetGrid();
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




    private int solutionOffset = 0;
    private int translationIndex = 0;

    public void CheckIfClearedSymbol(string currentSolution)
    {

        

        bool symbolComplete = false;
        int solutionIndex = 0;
        List<bool> clearedSymbols = new List<bool>();
        Debug.Log(clearedSymbols.Count + " BOOLS");
        Debug.Log(translations.Count + " Translations");


        foreach(string s in translations)
        {
            Debug.Log(s);
        }


        //goes through each index of strings in translations
        for (int i = 0; i < translations.Count; i++)
        {

            //If the bool before this one is false, this one is false
            if (i - 1 > 0 && clearedSymbols[i - 1] == false)
            {
                clearedSymbols[i] = false;
                continue;
            }



            //goes through the string at [i] and checks the chars compared to currentSolution
            for (int j = 0; j < translations[i].Length && solutionIndex + solutionOffset < currentSolution.Length; j++)
            {
                Debug.Log("i = " + i + " j = " + j);
                if (currentSolution[solutionIndex + solutionOffset] == translations[i][j])
                {
                    if(solutionIndex + solutionOffset + 1 == translations[i].Length)
                    {
                        clearedSymbols.Add(true); //?????????????????
                        symbolComplete = true;
                        solutionOffset += solutionIndex + 1;
                        solutionIndex = 0;
                        //translationIndex++;
                        break;
                    }

                    solutionIndex++;
                    continue;
                }  
                else
                {
                    
                    break;
                }
            }

            if(symbolComplete == false)
            {
                if (currentSolution.Length - solutionOffset < translations[i].Length)
                    clearedSymbols.Add(false);
            }
            

            //DETTA E FEL VI HAR REDAN LAGT TILL? VI BORDE VETA DETTA I LOOPEN
            

        }

        for(int i = 0; i < placedSymbols.Count; i++)
        {
            if (clearedSymbols[i] == true)
                placedSymbols[i].TurnOn();
            else
                placedSymbols[i].TurnOff();
        }
     }


    public void ExitPuzzle(ExitPuzzleEvent eve)
    {
        if (eve.success != true)
        {
            if (eve.info.ID == currentPuzzleInstance.GetPuzzleID())
            {
                if(eve.success == false)
                {
                    grid.ResetGrid();
                }
                    
            }
        }

    }


    #region Place Symbols
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

   /* private void UnevenPlaceSymbols()
    {
        Quaternion emptyQ = new Quaternion(0, 0, 0, 0);
        int mid = placedSymbols.Count / 2;
        placedSymbols[mid].transform.localPosition = Vector3.zero;
        placedSymbols[mid].transform.localRotation = emptyQ;

        for (int i = 1; i <= mid; i++)
        {
            Vector3 tempPos = Vector3.zero;
            tempPos -= i * (symbolOffset * Vector3.right);
            placedSymbols[mid - i].transform.localPosition = tempPos;
            placedSymbols[mid - i].transform.rotation = symbolPos.rotation;
            placedSymbols[mid - i].transform.localPosition =
                new Vector3(placedSymbols[mid - i].transform.localPosition.x, 0, 0);



            tempPos = Vector3.zero;
            tempPos += i * (symbolOffset * Vector3.right);
            placedSymbols[mid + i].transform.localPosition = tempPos;
            placedSymbols[mid + i].transform.rotation = symbolPos.rotation;
            placedSymbols[mid + i].transform.localPosition =
                new Vector3(placedSymbols[mid + i].transform.localPosition.x, 0, 0);
        }
    }

    private void EvenPlaceSymbols()
    {


        int midRight = placedSymbols.Count / 2;
        int midLeft = midRight - 1;

        Vector3 midLeftPos = (Vector3.left * (symbolOffset / 2));
        Vector3 midRightPos = (Vector3.right * (symbolOffset / 2));

        placedSymbols[midLeft].transform.localPosition = midLeftPos;
        placedSymbols[midLeft].transform.localRotation = new Quaternion(0, 0, 0, 0);

        placedSymbols[midRight].transform.localPosition = midRightPos;
        placedSymbols[midRight].transform.rotation = new Quaternion(0, 0, 0, 0);

        for (int i = 1; i <= midLeft; i++)
        {
            Vector3 tempPos = midLeftPos;
            tempPos -= i * (symbolOffset * Vector3.right);
            placedSymbols[midLeft - i].transform.localPosition = tempPos;
            placedSymbols[midLeft - i].transform.rotation = symbolPos.rotation;

            tempPos = midRightPos;
            tempPos += i * (symbolOffset * Vector3.right);
            placedSymbols[midRight + i].transform.localPosition = tempPos;
            placedSymbols[midRight + i].transform.rotation = symbolPos.rotation;
        }
    }*/
    #endregion    
}



