//=======AUTO GENERATED CODE=========//
//=======Tool Author: Jonathan Haag=========//

using System.Collections.Generic;
using UnityEngine;

public class SResolution : DropDownItem {

    [SerializeField] private List<string> options = new List<string>();
    
    
    public override void Initialize() {
        dropdownList.AddOptions(options);
        AddListener(ChangeScreenResolution);
    }

    
    private void ChangeScreenResolution(string value) {
        string resolution = value;

        bool fullscreen = (GameMenuController.Instance.RequestOption<Fullscreen>() as Fullscreen).GetValue();
        
        Resolution newResolution = ConvertStringToResolution(resolution);
        
        Screen.SetResolution(newResolution.width, newResolution.height, fullscreen);
    }
    
    private Resolution ConvertStringToResolution(string resolution) {
        
        string[] chosenResolution = resolution.Split('x');
        
        int width = int.Parse(chosenResolution[0]);
        int height = int.Parse(chosenResolution[1]);

        Resolution sResolution = new Resolution {
            width = width,
            height = height
        };

        return sResolution;
    }
    
}
