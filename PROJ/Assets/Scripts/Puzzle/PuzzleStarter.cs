using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;
    private void Awake()
    {
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start Puzzle");
        EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzleID)));
    }

}
