using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField] private GameObject cube;
    private void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("back", Back);
        actions.Add("right", Right);
        actions.Add("left", Left);
        actions.Add("rotate", Rotate);
        cube.transform.Translate(0, 0, 0);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Forward()
    {
        transform.Translate(1, 0, 0);
    }

    private void Back()
    {
        transform.Translate(-1, 0, 0);
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void Left()
    {
        transform.Translate(0, 0, 1);
    }

    private void Right()
    {
        transform.Translate(0, 0, -1);
    }

    private void Rotate()
    {
        transform.Rotate(0, 90, 0);
    }
}
