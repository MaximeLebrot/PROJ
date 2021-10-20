using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguagePuzzle : Puzzle
{
    //Place the symbols at the puzzle locations
    private List<Transform> symbolPositions = new List<Transform>();
    

    private void PlaceSymbols()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            symbolPositions.Add(transform.GetChild(i));
        }

        for(int i = 0; i < puzzleObjects.Count; i++)
        {
            puzzleObjects[i].transform.position = symbolPositions[i].position;
        }
    }
}
