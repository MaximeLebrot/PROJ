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
        EventHandler<ResetPuzzleEvent>.RegisterListener(OnReset);
    }

    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnExit);
        EventHandler<ResetPuzzleEvent>.UnregisterListener(OnReset);
    }

    private void OnExit(ExitPuzzleEvent eve)
    {
        ResetStarter(eve.info.ID);
    }

    private void OnReset(ResetPuzzleEvent eve)
    {
        ResetStarter(eve.info.ID);
    }

    private void ResetStarter(int id)
    {
        if (id == puzzleID)
        {
            Debug.Log("Exited");
            particles.Play();
            enderText.SetActive(false);

        }
    }
}