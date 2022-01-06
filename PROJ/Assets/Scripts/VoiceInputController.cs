using UnityEngine;

public class VoiceInputController : MonoBehaviour
{
    private InputMaster inputMaster;
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
        //EventHandler<SaveSettingsEvent>.RegisterListener(OnSaveSettings);
    }
    private void OnDisable()
    {
        //EventHandler<SaveSettingsEvent>.UnregisterListener(OnSaveSettings);
        inputMaster.Disable();
    }

    private void Start() {
        VoiceControl voiceControl = GameMenuController.Instance.RequestOption<VoiceControl>() as VoiceControl;
        
        voiceControl.AddListener((@dropdownValue) => {

            switch (dropdownValue) {
                case 0: 
                    NoVoiceMovement();
                    break;
                case 1:
                    VoiceMovementArmless();
                    break;
                case 2:
                    VoiceMovementMouse();
                    break;
            }
            
        });
        
    }

    private void ButtonClicker()
    {
        if (inputMaster.Voice.TurnOffBoth.triggered)
        {
            NoVoiceMovement();
        }
        if (inputMaster.Voice.TurnOnMouse.triggered)
        {
            VoiceMovementMouse();
        }
        if (inputMaster.Voice.TurnOnArmless.triggered)
        {
            VoiceMovementArmless();
        }
    }
    private void OnSaveSettings(SaveSettingsEvent eve)
    {
        //if(eve.settingsData.armlessVoiceMovement)
        //VoiceMovementArmless();

        //else if(eve.settingsData.mouseVoiceMovement)
        //VoiceMovementMouse();
        //else
        //NoVoiceMovement();
    }
    private void NoVoiceMovement()
    {
        //None
        GetComponent<VoiceMovementArmless>().enabled = false;
        GetComponent<VoiceMovementMouse>().enabled = false;

        Debug.Log("Voice DISABLED");
    }
    private void VoiceMovementMouse()
    {
        //Voice + mouse
        GetComponent<VoiceMovementArmless>().enabled = false;
        GetComponent<VoiceMovementMouse>().enabled = true;

        Debug.Log("Voice with mouse activated");
    }
    private void VoiceMovementArmless()
    {
        //Voice only
        GetComponent<VoiceMovementMouse>().enabled = false;
        GetComponent<VoiceMovementArmless>().enabled = true;
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
        Debug.Log("Armless voice activated");
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


