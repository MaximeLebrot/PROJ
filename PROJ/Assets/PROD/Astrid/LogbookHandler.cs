using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogbookHandler : MonoBehaviour
{
    private InputMaster inputMaster;
    private bool isOpen;
    [SerializeField] private GameObject logbook;
    private Animator animator;
    private Logbook logbookScript;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        inputMaster.Disable();
        
    }

    private void Start()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
        isOpen = false;
        logbook.SetActive(false);
        animator = logbook.GetComponent<Animator>();
        logbookScript = logbook.GetComponent<Logbook>();
    }

    private void Update()
    {
        // Open and close the logbook
        if (inputMaster.Player.Logbook.triggered)
        {
            // Put animation here.
            if (isOpen)
            {
                animator.SetTrigger("trigger");
                //logbook.SetActive(false);
                isOpen = false;
            }
            else
            {
                logbook.SetActive(true);
                animator.SetTrigger("trigger");
                isOpen = true;
            }
        }
        // Change page with keys
    }

    public void TurnPageLeft()
    {
       animator.SetTrigger("left");
       logbookScript.TurnPageLeft();
    }

    public void TurnPageRight()
    {
        animator.SetTrigger("right");
        logbookScript.TurnPageRight();
    }
}
