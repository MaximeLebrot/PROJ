using UnityEngine;

public class PuzzleShortcut : MonoBehaviour
{
    private InputMaster inputMaster;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private bool inPuzzle;

    void Awake()
    {
        inputMaster = new InputMaster();
        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();
    }

    private void Update()
    {
        if (inPuzzle)
        { 
            if (inputMaster.PuzzleDEBUGGER.AutoPilotPuzzle.triggered)
                puzzle.GoToNextPuzzle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inPuzzle = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inPuzzle = false;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
}
