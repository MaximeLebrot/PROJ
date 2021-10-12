using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPuzzle : Puzzle
{
    //Vill ha tillgång till alla dörrar i pusslet och ska kunna spela upp vindarna i rätt ordning? kan också ta in symboler?


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Translate(puzzleObjects);
        }
    }
}
