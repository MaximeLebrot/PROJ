using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRend;
    [SerializeField] private float lineLengthMultiplier;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lineRend.positionCount++;
            lineRend.SetPosition(lineRend.positionCount - 1, transform.position + Vector3.up * lineLengthMultiplier);
            transform.position = lineRend.GetPosition(lineRend.positionCount-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lineRend.positionCount++;
            lineRend.SetPosition(lineRend.positionCount - 1, transform.position + Vector3.right * lineLengthMultiplier);
            transform.position = lineRend.GetPosition(lineRend.positionCount - 1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lineRend.positionCount++;
            lineRend.SetPosition(lineRend.positionCount - 1, transform.position + Vector3.down * lineLengthMultiplier);
            transform.position = lineRend.GetPosition(lineRend.positionCount - 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lineRend.positionCount++;
            lineRend.SetPosition(lineRend.positionCount - 1, transform.position + Vector3.left * lineLengthMultiplier);
            transform.position = lineRend.GetPosition(lineRend.positionCount - 1);
        }


    }
}
