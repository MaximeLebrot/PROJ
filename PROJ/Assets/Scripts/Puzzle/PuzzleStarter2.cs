using UnityEngine;

public class PuzzleStarter2 : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    public GameObject starterText; //jag la till det här för min prototyp 
    public GameObject enderText; //jag la till det här för min prototyp 

    private void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }


    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartPussel();
        }

        if (Input.GetKeyDown("c"))
        {
            enderText.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start text");
        starterText.SetActive(true);
        enderText.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("End text");
        starterText.SetActive(false);
        enderText.SetActive(true);
    }

    private void StartPussel()
    {
        Debug.Log("Start Puzzle");
        starterText.SetActive(false);
        enderText.SetActive(false);

        EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzleID, transform)));
    }

}
