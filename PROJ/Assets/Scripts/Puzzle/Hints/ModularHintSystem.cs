using UnityEngine;

public class ModularHintSystem : MonoBehaviour
{
    public GameObject hintGO;
    private InputMaster inputMaster;
    public Material mat;
    public Puzzle puzzle;

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

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    hintGO.SetActive(true);
        //}
        if (inputMaster.PuzzleDEBUGGER.Hint.triggered)
        {
            hintGO.SetActive(true);
            foreach (PuzzleObject symbol in puzzle.instantiatedSymbols)
                symbol.GetComponent<Material>().color = Color.yellow;
            //foreach (PuzzleObject symbol in puzzle.instantiatedSymbols)
            //symbol.GetComponent<MeshRenderer>().material = mat;
        }
    }
}
