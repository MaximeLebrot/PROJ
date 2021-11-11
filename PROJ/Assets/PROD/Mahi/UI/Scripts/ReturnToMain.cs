using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    private InputMaster inputMaster;
    [SerializeField] string sceneName;

    void Start()
    {
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
    private void Update()
    {
        if (inputMaster.UI.BackToMain.triggered)
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        if (sceneName == "")
            sceneName = "MainMenu";
        SceneManager.LoadScene(sceneName);
    }
      
}
