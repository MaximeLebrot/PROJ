using UnityEngine;
using UnityEngine.VFX;

public class ModularHintSystem : MonoBehaviour
{
    public GameObject hintGO;
    public Material downOG;
    public Material downHint;
    public Material rightOG;
    public Material rightHint;
    public Puzzle puzzle;

    private InputMaster inputMaster;

    private bool showHints = true;
   

    void Start()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
        foreach (PuzzleObject symbol in puzzle.placedSymbols)
        {
            Debug.Log(symbol.name);
            foreach (Component component in symbol.GetComponents(typeof(Component)))
            {
                Debug.Log(component.GetType());
            }
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    void Update()
    {
        //if (inputMaster.PuzzleDEBUGGER.Hint.triggered)
        //{
        //    Debug.Log(showHints);
        //    TriggerHintSystem(showHints);
        //}
    }

    private void TriggerHintSystem(bool showHints)
    {
        if (showHints)
        {
            hintGO.SetActive(true);

            for (int i = 0; i < puzzle.placedSymbols.Count; i++)
            {
                if (i % 2 != 0)
                    puzzle.placedSymbols[i].GetComponent<MeshRenderer>().material = rightHint;
                else
                    puzzle.placedSymbols[i].GetComponent<MeshRenderer>().material = downHint;
            }
        }
        else
        {
            hintGO.SetActive(false);

            for (int i = 0; i < puzzle.placedSymbols.Count; i++)
            {
                if (i % 2 != 0)
                    puzzle.placedSymbols[i].GetComponent<MeshRenderer>().material = rightOG;
                else
                    puzzle.placedSymbols[i].GetComponent<MeshRenderer>().material = downOG;
            }
        }
        this.showHints = !showHints;
    }
}
