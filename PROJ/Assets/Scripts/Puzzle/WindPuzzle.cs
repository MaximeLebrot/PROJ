using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPuzzle : Puzzle
{
    //Vill ha tillg�ng till alla d�rrar i pusslet och ska kunna spela upp vindarna i r�tt ordning? kan ocks� ta in symboler?


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Translate(puzzleObjects);
        }
    }
}
