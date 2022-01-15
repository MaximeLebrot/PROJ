using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject prototypeMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private ControllerInputReference  inputMaster;

    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
    }
    
    void Start() {
        optionsMenu.SetActive(false);
        prototypeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        inputMaster.InputMaster.Anykey.performed += PressAnyKey;
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

    private void PressAnyKey(InputAction.CallbackContext e) {
        inputMaster.InputMaster.Anykey.performed -= PressAnyKey;
        BackToMain();
    }

}
