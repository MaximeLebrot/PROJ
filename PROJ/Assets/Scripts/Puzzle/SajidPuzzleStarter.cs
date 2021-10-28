using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SajidPuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;

    [SerializeField] private AudioSource source;
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
        enderText.SetActive(true);


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
            enderText.SetActive(false);

        }
    }
}
