using UnityEngine;

public class PuzzleShortcut : MonoBehaviour
{
    private InputMaster inputMaster;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private bool inPuzzle;
    private static VoiceMovementArmless vma;

    void Awake()
    {
        if (vma == null)
        {
            vma = GameObject.FindGameObjectWithTag("Player").GetComponent<VoiceMovementArmless>();
            Debug.Log(vma);
        }
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
        {
            inPuzzle = true;
            if (other.GetComponent<VoiceMovementArmless>().enabled)
                vma.InZone(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPuzzle = false;
            if (other.GetComponent<VoiceMovementArmless>().enabled)
                vma.InZone(transform);
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
}
