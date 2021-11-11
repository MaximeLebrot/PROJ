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
    public GameObject[] kanelbullar;
    public Animator animator;
    public GameObject song;
    public GameObject Light;

    private int i = 0;
    private void Start()
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

        actions.Add("Bulle", David);
        actions.Add("Cinnamon", David);

        actions.Add("Dance", Dance);
        actions.Add("Kill", Die);

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

    private void David()
    {
        kanelbullar[i].SetActive(true);
        i++;
      
    }

    private void Dance()
    {
        animator.SetTrigger("Dance");
        direction.text = "Rick Rolled";
        song.GetComponent<AudioSource>().Play();
        direction.GetComponent<Animator>().SetTrigger("active");
        Light.GetComponent<Animator>().SetTrigger("ActivateLight");
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        direction.text = "Dying";
        direction.GetComponent<Animator>().SetTrigger("active");
    }

}
