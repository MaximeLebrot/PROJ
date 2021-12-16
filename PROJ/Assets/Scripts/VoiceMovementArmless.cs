using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class VoiceMovementArmless : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private bool puzzleActive;
    Quaternion activePuzzleRotation;

    public Animator animator;

    /*
    private void Start()
    {
        Debug.Log("Started Armless");
        AddActions();

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    */
    private void AddActions()
    {
        actions.Add("forward", Forward);
        actions.Add("back", Back);
        actions.Add("right", Right);
        actions.Add("left", Left);

        actions.Add("rotate ninety", Rotate);
        actions.Add("rotate", Rotate);

        actions.Add("rotate onehundred and eighty", Rotate180);
        actions.Add("rotate one eighty", Rotate180);

        actions.Add("rotate twohundred and seventy", Rotate270);
        actions.Add("rotate two seventy", Rotate270);
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Forward()
    {
        if (puzzleActive == false)
        {
            transform.Translate(0, 0, 5);
        } else
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.forward * 3;
            transform.position += puzzleMovement;
        }
    }

    private void Back()
    {
        if(puzzleActive == false)
        transform.Translate(0, 0, -5);
        else 
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.forward * 3;
            transform.position -= puzzleMovement;
        }
    }
    private void Left()
    {
        if (puzzleActive == false)
            transform.Translate(-5, 0, 0);
        else
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.right * 3;
            transform.position -= puzzleMovement;

        }
    }

    private void Right()
    {
        if (puzzleActive == false)

            transform.Translate(5, 0, 0);
        else
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.right * 3;
            transform.position += puzzleMovement;
        }
    }

    private void Rotate()
    {
            transform.Rotate(0, 90, 0);
    }

    private void Rotate180()
    {
        transform.Rotate(0, 180, 0);
    }

    private void Rotate270()
    {
        transform.Rotate(0, 270, 0);
    }

    private void OnStartPuzzle(StartPuzzleEvent eve)
    {

        puzzleActive = true;
        Debug.Log("PuzzleStarted");
        activePuzzleRotation = eve.info.puzzlePos.rotation;
        //    transform.rotation = activePuzzleRotation;
        Debug.Log("activepuzzlerotation: " + activePuzzleRotation);

    }
    private void OnExitPuzzle(ExitPuzzleEvent eve)
    {
        puzzleActive = false;
        Debug.Log("PuzzleEnded");

    }
    private void OnEnable()
    {
        if (actions.Count == 0)
        {
            AddActions();

        }

        EventHandler<StartPuzzleEvent>.RegisterListener(OnStartPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnExitPuzzle);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }
    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnStartPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnExitPuzzle);
        keywordRecognizer.Dispose();
        Debug.Log("Stopped1");

    }

}
