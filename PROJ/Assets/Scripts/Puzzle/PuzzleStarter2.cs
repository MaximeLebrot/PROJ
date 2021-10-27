using UnityEngine;

public class PuzzleStarter2 : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    [SerializeField] private GameObject enderText; //jag la till det här för min prototyp 
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource source2;
    [SerializeField] private GameObject particles; 

    private void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start text");
        enderText.SetActive(false);
        EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzleID, transform)));

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("End text");
        enderText.SetActive(true);
    }

}
