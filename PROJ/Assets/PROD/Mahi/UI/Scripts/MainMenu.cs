using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject prototypeMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Slider fovSlider;
    [SerializeField] private TextMeshProUGUI fovText;
    private Animator anim;
    private InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
     //   Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        fovSlider.value = Settings.FieldOfView;

        Camera.main.fieldOfView = Settings.FieldOfView;

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

        int value = (int)fovSlider.value;
        
        fovText.text = value.ToString();

        Settings.FieldOfView = fovSlider.value;

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
