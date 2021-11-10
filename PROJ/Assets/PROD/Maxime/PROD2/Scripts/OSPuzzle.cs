using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSPuzzle : MonoBehaviour
{
    [SerializeField] private List<Button> nodes;
    [SerializeField] private GameObject parent;

    [SerializeField, Range(0.1f, 0.9f)] private float speed;
    [SerializeField] private float time = 1f, timer;
    [SerializeField] private int iterator = 0;
    [SerializeField] private float frameCounter;
    [SerializeField, Range(0.01f, 0.3f)] private float holdingButtonLimit = 0.2f;
    [SerializeField] private bool pressingButton = false;
    private bool giveLostTime = true;
    private InputMaster inputMaster;

    public void StartOSPuzzle(StartPuzzleEvent eve)
    {
        //player.velocity = Vector3.zero
        parent.SetActive(true);
        timer = time;
    }

    public void ExitOSPuzzle(ExitPuzzleEvent eve)
    {
        Debug.Log("BOOHJubshd'");
        parent.SetActive(false);
    }

    private void Update()
    {
        time = 1f - speed;
        if (inputMaster.OneSwitch.PuzzleTest.ReadValue<float>() != 0)
        {
            pressingButton = true;
            frameCounter += Time.deltaTime;
            if (frameCounter >= holdingButtonLimit)
            {
                if (giveLostTime)
                    timer += frameCounter;
                giveLostTime = false;
                if (timer >= time)
                {
                    if (iterator >= nodes.Count)
                        iterator = 0;
                    foreach (Button node in nodes)
                        node.image.color = Color.white;
                    nodes[iterator].image.color = Color.red;
                    iterator++;
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
        else
        {
            if (pressingButton)
            {
                if (frameCounter < holdingButtonLimit)
                {
                    Debug.Log("Triggered");
                }
                frameCounter = 0;
                pressingButton = false;
                giveLostTime = true;
            }

        }
    }

    internal void StartOSPuzzle()
    {
        throw new NotImplementedException();
    }

    #region UES
    private void Start()
    {
        if (parent == null)
            return;
        if (nodes.Count == 0)
            nodes.AddRange(parent.GetComponentsInChildren<Button>());
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
    #endregion
}