using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogbookHandler : MonoBehaviour
{
    private InputMaster inputMaster;
    private bool isOpen;
    [SerializeField] private GameObject logbook;
    private Animator animator;

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
    }

    private void Update()
    {
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
    }

    public void TurnPageLeft()
    {
        animator.SetTrigger("left");
    }
    public void TurnPageRight()
    {
        animator.SetTrigger("right");
    }
}
