using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartScene : MonoBehaviour
{
    private InputMaster inputMaster;
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

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
        if (inputMaster.UI.RestartScene.triggered)
        {
            ReloadScene();
        }
    }


}