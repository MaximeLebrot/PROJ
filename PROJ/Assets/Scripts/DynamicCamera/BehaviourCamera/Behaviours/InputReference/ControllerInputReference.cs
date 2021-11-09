using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reference/Controller Input Reference", fileName = "Controller Input Reference")]
public class ControllerInputReference : ScriptableObject {
    
    private InputMaster inputMaster;
    public InputMaster.PlayerActions InputMaster => inputMaster.Player;

    public void OnEnable() {
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    
}

