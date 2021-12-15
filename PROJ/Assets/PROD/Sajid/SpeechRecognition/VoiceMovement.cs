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
    private PlayerController mpc;
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

        mpc = GetComponent<PlayerController>();
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
            //    transform.Translate(Vector3.forward * Time.deltaTime * speed);
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
        // transform.Translate(1, 0, 0);
        // transform.Translate(Vector3.forward * Time.deltaTime * speed);
        walkingForward = true;
        direction.text = "Forward";
        direction.GetComponent<Animator>().SetTrigger("active");
        //animator.SetFloat(x, 1);
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
        transform.Rotate(0, -90, 0);
    }

    private void Right()
    {
        transform.Rotate(0, 90, 0);

    }

 


}
