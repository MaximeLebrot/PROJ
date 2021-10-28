using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    private InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
    }
    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
    private void Update()
    {
        if (inputMaster.UI.BackToMain.triggered)
        {
            SceneManager.LoadScene("Mahi_MainMenu_Prototyp");
        }
    }
      
}
