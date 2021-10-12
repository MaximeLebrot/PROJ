using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPuzzle : Puzzle
{
    //play the winds in the order of the puzzleobjects


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Translate(puzzleObjects);
        }
    }
}
