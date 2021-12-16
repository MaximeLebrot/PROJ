using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VoiceMovementMouse : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private PlayerController mpc;
    public Animator animator;
    public PuzzlePlayerController puzzleMovement;
    private int i = 0;
    private int x, y;

    public float speed;

    private bool walking, running, puzzleActive;

    Quaternion activePuzzleRotation;

    private void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("up", Forward);
        actions.Add("straight", Forward);
        actions.Add("walk", Forward);
        actions.Add("go", Forward);

        actions.Add("diagonal right up", DiagonalRightUp);
        actions.Add("diagonal up right", DiagonalRightUp);
        actions.Add("up right", DiagonalRightUp);
        actions.Add("right up", DiagonalRightUp);
        actions.Add("forward right", DiagonalRightUp);
        actions.Add("right forward", DiagonalRightUp);
        actions.Add("diagonal right left", DiagonalRightUp);
        actions.Add("diagonal right forward", DiagonalRightUp);

        actions.Add("diagonal left up", DiagonalLeftUp);
        actions.Add("diagonal up left", DiagonalLeftUp);
        actions.Add("left up", DiagonalLeftUp);
        actions.Add("up left", DiagonalLeftUp);
        actions.Add("forward left", DiagonalLeftUp);
        actions.Add("left forward", DiagonalLeftUp);
        actions.Add("diagonal forward left", DiagonalLeftUp);
        actions.Add("diagonal left forward", DiagonalLeftUp);

        actions.Add("diagonal right down", DiagonalRightDown);
        actions.Add("diagonal down right", DiagonalRightDown);
        actions.Add("right down", DiagonalRightDown);
        actions.Add("down right", DiagonalRightDown);
        actions.Add("right down", DiagonalRightDown);
        actions.Add("diagonal down right", DiagonalRightDown);
        actions.Add("diagonal down right", DiagonalRightDown);

        actions.Add("diagonal left down", DiagonalLeftDown);
        actions.Add("diagonal down left", DiagonalLeftDown);
        actions.Add("left down", DiagonalLeftDown);
        actions.Add("down left", DiagonalLeftDown);
        actions.Add("left down", DiagonalLeftDown);
        actions.Add("diagonal down left", DiagonalLeftDown);
        actions.Add("diagonal down left", DiagonalLeftDown);

        actions.Add("down", Down);
        actions.Add("back", Down);

        actions.Add("right", Right);
        actions.Add("left", Left);

        actions.Add("stop", Stop);
        actions.Add("cancel", Stop);
        actions.Add("halt", Stop);
        actions.Add("chill", Stop);


        mpc = GetComponent<PlayerController>();
        x = Animator.StringToHash("speed");
        y = Animator.StringToHash("direction");

      //  direction.text = "";
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    private void Update()
    {
        if(walking)
        {
            mpc.InputWalk(new Vector3(0,1,0));
            animator.SetFloat(x, 1);
        }
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Forward()
    {
        if(puzzleActive == false)

        walking = true; 

        else
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.forward * 3;
            transform.position+=puzzleMovement;
        }
    }
    private void Down()
    {
        if(puzzleActive)
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.forward * 3;
            transform.position -= puzzleMovement;
        }
    }
    private void Left()
    {
        if (puzzleActive == false)
        {
            transform.Rotate(0, -90, 0);
        } else
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.right * 3;
            transform.position -= puzzleMovement;

        }
    }
    private void Right() {
        if (puzzleActive == false)
            transform.Rotate(0, 90, 0);
        else
        {
            Vector3 puzzleMovement = activePuzzleRotation * Vector3.right * 3;
            transform.position += puzzleMovement;
        }
    }
    private void DiagonalRightUp()
    {
        if (puzzleActive)
        {
            Vector3 puzzleMovement = activePuzzleRotation * (Vector3.right + Vector3.forward) * 3;
            transform.position += puzzleMovement;
        }
    }
    private void DiagonalLeftUp()
    {
        if (puzzleActive)
        {
            Vector3 puzzleMovement = activePuzzleRotation * (-Vector3.right + Vector3.forward) * 3;
            transform.position += puzzleMovement;
        }
    }
    private void DiagonalLeftDown()
    {
        if (puzzleActive)
        {
            Vector3 puzzleMovement = activePuzzleRotation * (Vector3.right + Vector3.forward) * 3;
            transform.position -= puzzleMovement;
        }
    }
    private void DiagonalRightDown()
    {
        if (puzzleActive)
        {
            Vector3 puzzleMovement = activePuzzleRotation * (-Vector3.right + Vector3.forward) * 3;
            transform.position -= puzzleMovement;
        }
    }
    private void Stop()
    {
        walking = false;
        running = false;
        animator.SetFloat(x, 0);

    }
    private void OnStartPuzzle(StartPuzzleEvent eve)
    {

        puzzleActive = true;
        walking = false;
        Debug.Log("PuzzleStarted");
        animator.SetFloat(x, 0);
        activePuzzleRotation = eve.info.puzzlePos.rotation;
    //    transform.rotation = activePuzzleRotation;
        Debug.Log("activepuzzlerotation: "+ activePuzzleRotation);
        
    }
    private void OnExitPuzzle(ExitPuzzleEvent eve)
    {
        puzzleActive = false;
        Debug.Log("PuzzleEnded");

    }
    private void OnEnable()
    {
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
        Debug.Log("Stopped");

    }

}


