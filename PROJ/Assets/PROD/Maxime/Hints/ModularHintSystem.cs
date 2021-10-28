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
    
    void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void Start()
    {
        foreach (PuzzleObject symbol in puzzle.instantiatedSymbols)
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
        inputMaster.Enable();
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

            for (int i = 0; i < puzzle.instantiatedSymbols.Count; i++)
            {
                if (i % 2 != 0)
                    puzzle.instantiatedSymbols[i].GetComponent<MeshRenderer>().material = rightHint;
                else
                    puzzle.instantiatedSymbols[i].GetComponent<MeshRenderer>().material = downHint;
            }
        }
        else
        {
            hintGO.SetActive(false);

            for (int i = 0; i < puzzle.instantiatedSymbols.Count; i++)
            {
                if (i % 2 != 0)
                    puzzle.instantiatedSymbols[i].GetComponent<MeshRenderer>().material = rightOG;
                else
                    puzzle.instantiatedSymbols[i].GetComponent<MeshRenderer>().material = downOG;
            }
        }
        this.showHints = !showHints;
    }
}
