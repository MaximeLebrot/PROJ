using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleStarter : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    //Image?
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayUIText();
            //Interact pressed
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveUIText();
        }
    }

    private void DisplayUIText()
    {
        transform.LookAt(Camera.main.transform);
        transform.eulerAngles += new Vector3(0f, transform.rotation.y + 180f, 0f);

        textMeshPro.enabled = true;
    }
    private void RemoveUIText()
    {
        textMeshPro.enabled = false;
    }
}
