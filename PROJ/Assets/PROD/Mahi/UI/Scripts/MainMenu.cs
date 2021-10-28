using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject prototypeMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    private Animator anim;
    private InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
     //   Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        optionsMenu.SetActive(false);
        prototypeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    private void Update()
    {
        if (inputMaster.PuzzleDEBUGGER.PressAnyButton.triggered)
        {
            PressAnyKey();
        }
    }

    public void OpenPrototype()
    {
        optionsMenu.SetActive(false);
        prototypeMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        optionsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToMain()
    {
        optionsMenu.SetActive(true);
        prototypeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PressAnyKey()
    {
        BackToMain();
    }
}
