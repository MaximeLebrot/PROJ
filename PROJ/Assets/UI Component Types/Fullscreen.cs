//=======AUTO GENERATED CODE=========//
//=======Tool Author: Jonathan Haag=========//

using UnityEngine;

public class Fullscreen : ToggleSetting {
    public override void Initialize() {
        AddListener((value) => {
            Screen.fullScreenMode = value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        });
    }
}
