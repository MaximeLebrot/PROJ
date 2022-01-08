using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogbookHandler : MonoBehaviour
{
    [SerializeField]private ControllerInputReference inputReference;
    [SerializeField] private GameObject logbook;

    public Logbook Logbook => logbook.GetComponent<Logbook>();

    private bool isOpen;
    private Animator animator;
    private Logbook logbookScript;

    private FMOD.Studio.EventInstance BookOpen;
    private FMOD.Studio.EventInstance BookClose;
    private FMOD.Studio.EventInstance PageOpen;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);    
    }

    private void Start()
    {
        isOpen = false;
        animator = logbook.GetComponent<Animator>();
        logbookScript = logbook.GetComponent<Logbook>();
    }

    private void Update()
    {
        // Open and close the logbook
        if (inputReference.InputMaster.Logbook.triggered)
        {
            // Put animation here.
            if (isOpen)
                CloseBook();
            else
                OpenBook();
        }
        if (isOpen)
        {
            // Change page with keys
            if (inputReference.inputMaster.Logbook.TurnLeft.triggered)
                TurnPageLeft();          
            if (inputReference.inputMaster.Logbook.TurnRight.triggered)
                TurnPageRight();
        }
    }

    private void CloseBook()
    {
        animator.SetTrigger("trigger");
        isOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(false));

        BookClose = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/BookClose");
        BookClose.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        BookClose.start();
        BookClose.release();
    }

    private void OpenBook()
    {
        logbook.SetActive(true);
        animator.SetTrigger("trigger");
        isOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(true));

        BookOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/BookOpen");
        BookOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        BookOpen.start();
        BookOpen.release();
    }

    public void TurnPageLeft()
    {
        //animator.SetTrigger("left");
        logbookScript.FlipPage(false);

        PageOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageOpen");
        PageOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PageOpen.start();
        PageOpen.release();

    }

    public void TurnPageRight()
    {
        //animator.SetTrigger("right");
        logbookScript.FlipPage(true);

        PageOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageOpen");
        PageOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PageOpen.start();
        PageOpen.release();
    }
}
