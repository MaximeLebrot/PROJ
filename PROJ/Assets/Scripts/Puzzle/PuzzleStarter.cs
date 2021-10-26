using UnityEngine;

public class PuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    [SerializeField] private AudioSource source;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject enderText;

    private void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start Puzzle");
        EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzleID, GetComponentInParent<Puzzle>().transform)));
        source.Play();
        particles.Stop();
        enderText.SetActive(true);

        //StartPuzzleEvent skickas även när pusslet är igång, fix plz.

    }

    private void OnEnable()
    {
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnExit);
    }

    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnExit);
    }

    private void OnExit(ExitPuzzleEvent eve)
    {
        if(eve.info.ID == puzzleID)
        {
            Debug.Log("Exited");
            particles.Play();
            enderText.SetActive(false);

        } else
        {
            Debug.Log(eve.info.ID + " " + puzzleID);
        }
    }

}