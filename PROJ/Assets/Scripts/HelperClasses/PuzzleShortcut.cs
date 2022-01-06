using UnityEngine;

public class PuzzleShortcut : MonoBehaviour
{
    
    private InputMaster inputMaster;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private bool inPuzzle;
    private static VoiceMovementArmless vma;
    private static VoiceMovementMouse vmm;

    void Start()
    {
        if (vma == null)
            vma = GameObject.FindGameObjectWithTag("Player").GetComponent<VoiceMovementArmless>();
        if (vmm == null)
            vmm = GameObject.FindGameObjectWithTag("Player").GetComponent<VoiceMovementMouse>();

        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();
        
        inputMaster = new InputMaster();
        inputMaster.Enable();
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
            else if (other.GetComponent<VoiceMovementMouse>().enabled)
                vmm.InZone(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPuzzle = false;
            if (other.GetComponent<VoiceMovementArmless>().enabled)
                vma.InZone(transform);
            else if (other.GetComponent<VoiceMovementMouse>().enabled)
                vmm.InZone(transform);
        }
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
    
}
