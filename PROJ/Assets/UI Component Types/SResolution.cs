//=======AUTO GENERATED CODE=========//
//=======Tool Author: Jonathan Haag=========//

using System.Collections.Generic;
using UnityEngine;

public class SResolution : DropDownItem {
    public override void Initialize() {

        Resolution[] resolutions = Screen.resolutions;

        List<string> options = new List<string>(Screen.resolutions.Length);
        
        foreach (Resolution resolution in resolutions) {
            string[] displayReadyText = resolution.ToString().Split(' ');
            options.Add(displayReadyText[0] + "x" + displayReadyText[2]);
        }
          
        dropdownList.AddOptions(options);
    }
}
