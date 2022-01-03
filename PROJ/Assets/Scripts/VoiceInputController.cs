using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceInputController : MonoBehaviour
{
    private InputMaster inputMaster;
    public GameObject armlessCamera;
    [SerializeField] private GameObject player;


    void Awake()
    {
        inputMaster = new InputMaster();            
    }

    private void Update()
    {
        ButtonClicker();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
        EventHandler<SaveSettingsEvent>.RegisterListener(OnSaveSettings);
    }
    private void OnDisable()
    {
        EventHandler<SaveSettingsEvent>.UnregisterListener(OnSaveSettings);
        inputMaster.Disable();
    }

    private void ButtonClicker()
    {
        if (inputMaster.Voice.TurnOffBoth.triggered)
        {
            GetComponent<VoiceMovementArmless>().enabled = false;
            GetComponent<VoiceMovementMouse>().enabled = false;
            armlessCamera.SetActive(false);

            Debug.Log("1");
        }
        if (inputMaster.Voice.TurnOnMouse.triggered)
        {
            GetComponent<VoiceMovementArmless>().enabled = false;
            GetComponent<VoiceMovementMouse>().enabled = true;
            armlessCamera.SetActive(false);

            Debug.Log("2");

        }
        if (inputMaster.Voice.TurnOnArmless.triggered)
        {
            GetComponent<VoiceMovementMouse>().enabled = false;
            GetComponent<VoiceMovementArmless>().enabled = true;
       //     armlessCamera.SetActive(true);
       //     player.transform.rotation = Quaternion.Euler(0, 90, 0);
            Debug.Log("3");

        }
    }
}
  
 
/*
public void DropDownHandler(int choice)
{
    if(choice == 0)
    {
        GetComponent<VoiceMovementArmless>().enabled = false;
        GetComponent<VoiceMovementMouse>().enabled = false;
    }
    if (choice == 1)
    {
        GetComponent<VoiceMovementMouse>().enabled = true;
        GetComponent<VoiceMovementArmless>().enabled = false;

    }
    if (choice == 2)
    {
        GetComponent<VoiceMovementArmless>().enabled = true;
        GetComponent<VoiceMovementMouse>().enabled = false;

    }
}
*/


