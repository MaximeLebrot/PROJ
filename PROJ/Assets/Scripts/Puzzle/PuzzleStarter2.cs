using UnityEngine;

public class PuzzleStarter2 : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    [SerializeField] private GameObject starterText; //jag la till det här för min prototyp 
    [SerializeField] private GameObject enderText; //jag la till det här för min prototyp 
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource source2;
    [SerializeField] private GameObject particles; 

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
            source.Play();
            particles.SetActive(true);
        }

        if (Input.GetKeyDown("c"))
        {
            enderText.SetActive(false);
            source2.Play();
            particles.SetActive(false);


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
