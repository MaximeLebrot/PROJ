using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    private InputMaster inputMaster;

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


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Mahi_MainMenu_Prototyp");
    }
      
}
