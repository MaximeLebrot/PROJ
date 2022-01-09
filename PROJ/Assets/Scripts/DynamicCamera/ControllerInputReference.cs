using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "Input Reference/Controller Input Reference", fileName = "Controller Input Reference")]
public class ControllerInputReference : ScriptableObject {
    
    public InputMaster inputMaster;
    public InputMaster.PlayerActions InputMaster {
        get {
            if(inputMaster == null)
                Initialize();

            return inputMaster.Player;
        }
    }

    public InputMaster Asset {

        get {
            if(inputMaster == null)
                Initialize();

            return inputMaster;
        }
        
    }

    public InputMaster.OneSwitchActions OneSwitchInputMaster => inputMaster.OneSwitch;

    public void Initialize() {       
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }
}

