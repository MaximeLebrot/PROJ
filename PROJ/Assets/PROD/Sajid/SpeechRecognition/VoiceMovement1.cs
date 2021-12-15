/*
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
    public Text direction;

    public Animator animator;

    private int i = 0;
    private int x, y;

    public float speed;

    private bool walkingForward;

    private void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("back", Back);
        actions.Add("right", Right);
        actions.Add("left", Left);
        actions.Add("stop", Stop);

        actions.Add("rotate ninety", Rotate);
        actions.Add("rotate", Rotate);

        actions.Add("rotate onehundred and eighty", Rotate180);
        actions.Add("rotate one eighty", Rotate180);

        actions.Add("rotate twohundred and seventy", Rotate270);
        actions.Add("rotate two seventy", Rotate270);

        actions.Add("Dance", Dance);

        x = Animator.StringToHash("speed");
        y = Animator.StringToHash("direction");

        direction.text = "";
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void Update()
    {
        if(walkingForward)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Forward()
    {
        // transform.Translate(1, 0, 0);
        // transform.Translate(Vector3.forward * Time.deltaTime * speed);
        walkingForward = true;
        direction.text = "Forward";
        direction.GetComponent<Animator>().SetTrigger("active");
        animator.SetFloat(x, 1);
    }

    private void Stop()
    {
        walkingForward = false;
        animator.SetFloat(x, 0);

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
        direction.text = "Rotate 90";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Rotate180()
    {
        transform.Rotate(0, 180, 0);
        direction.text = "Rotate 180";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Rotate270()
    {
        transform.Rotate(0, 270, 0);
        direction.text = "Rotate 270";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

    private void Dance()
    {
        animator.SetTrigger("Dance");
        direction.text = "Rick Rolled";
        direction.GetComponent<Animator>().SetTrigger("active");
    }


}
*/