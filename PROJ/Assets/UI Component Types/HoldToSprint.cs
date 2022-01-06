//=======AUTO GENERATED CODE=========//
//=======Tool Author: Jonathan Haag=========//

using UnityEngine;

public class HoldToSprint : ToggleSetting {

    [SerializeField] private ToggleSetting pressToSprint;

    public override void Initialize() {
        pressToSprint.AddListener((pressToSprintValue) => toggle.isOn = !pressToSprintValue);
    }
}
