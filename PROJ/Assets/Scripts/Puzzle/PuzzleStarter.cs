using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleStarter : MonoBehaviour
{
    private Puzzle puzzle;
    private int puzzleID;
    private TextMeshPro textMeshPro;
    private InputMaster inputMaster;
    //Image?
    private void Awake()
    {
        inputMaster = new InputMaster();
        textMeshPro = GetComponent<TextMeshPro>();
        
        puzzle = GetComponentInParent<Puzzle>();
        puzzleID = puzzle.GetPuzzleID();
    }
    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding with : " + other.gameObject);
        DisplayUIText();
        //Interact pressed
        if(inputMaster.Player.Interact.triggered)
            EventHandler<StartPuzzleEvent>.FireEvent(new StartPuzzleEvent(new PuzzleInfo(puzzleID)));
    }
    private void OnTriggerExit(Collider other)
    {
        RemoveUIText();
    }

    private void DisplayUIText()
    {
        Quaternion direction = Quaternion.LookRotation((transform.position - Camera.main.transform.position), Vector3.up);
        Vector3 euler = direction.eulerAngles;
        euler.x = 0;
        transform.eulerAngles = euler;

        textMeshPro.enabled = true;
    }
    private void RemoveUIText()
    {
        textMeshPro.enabled = false;
    }
}
