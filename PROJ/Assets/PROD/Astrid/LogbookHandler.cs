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

    private FMOD.Studio.EventInstance BookOpen;
    private FMOD.Studio.EventInstance BookClose;
    private FMOD.Studio.EventInstance PageOpen;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);    
    }

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
                Cursor.visible = false;
                BookClose = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/BookClose");
                BookClose.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                BookClose.start();
                BookClose.release();
            }
            else
            {
                logbook.SetActive(true);
                animator.SetTrigger("trigger");
                isOpen = true;
                Cursor.visible = true;
                BookOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/BookOpen");
                BookOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                BookOpen.start();
                BookOpen.release();
            }
        }
        if (isOpen)
        {
            // Change page with keys
            if (inputMaster.Logbook.TurnLeft.triggered)
            {
                TurnPageLeft();          
            }
            if (inputMaster.Logbook.TurnRight.triggered)
            {
                TurnPageRight();
            }
        }
    }

    public void TurnPageLeft()
    {
       //animator.SetTrigger("left");
       logbookScript.TurnPageLeft();

        PageOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageOpen");
        PageOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PageOpen.start();
        PageOpen.release();

    }

    public void TurnPageRight()
    {
        //animator.SetTrigger("right");
        logbookScript.TurnPageRight();

        PageOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageOpen");
        PageOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PageOpen.start();
        PageOpen.release();
    }
}
