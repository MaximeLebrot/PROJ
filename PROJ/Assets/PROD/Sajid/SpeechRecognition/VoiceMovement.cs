using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
   // [SerializeField] private GameObject Text;
    public Text direction;

    private void Start()
    {
        actions.Add("forward", Forward);
       // actions.Add("up", Up);
       // actions.Add("down", Down);
        actions.Add("back", Back);
        actions.Add("right", Right);
        actions.Add("left", Left);
        actions.Add("rotate", Rotate);
        actions.Add("Wow", Owen);
        direction.text = "";
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
        direction.text = "Forward";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Back()
    {
        transform.Translate(-1, 0, 0);
        direction.text = "Back";
        direction.GetComponent<Animator>().SetTrigger("active");
    }
    private void Left()
    {
        transform.Translate(0, 0, 1);
        direction.text = "Left";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Right()
    {
        transform.Translate(0, 0, -1);
        direction.text = "Right";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Rotate()
    {
        transform.Rotate(0, 90, 0);
        direction.text = "Rotate";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Owen()
    {
        //transform.Rotate(0, 90, 0);
        direction.text = "Wooooow";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

}
