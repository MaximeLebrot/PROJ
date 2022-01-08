//=======AUTO GENERATED CODE=========//
//=======Tool Author: Jonathan Haag=========//

using UnityEngine;

public class PressToSprint : ToggleSetting {
    
    [SerializeField] private ToggleSetting holdToSprint;

    public override void Initialize() {
        holdToSprint.AddListener((holdToSprintValue) => toggle.isOn = !holdToSprintValue);
    }
    
}
