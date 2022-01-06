using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoSettings : MenuSettings {

    [SerializeField] private TMP_Dropdown resolutionList;

    private Resolutioner resolutioner;

    protected override void SubMenuInitialize() {
        resolutioner = new Resolutioner(resolutionList, true);
    }

    public override void SetMenuItems(SettingsData settingsData) {
      /*  
        menuOptions[ExtractMenuItem("Field of View").ID].SetValue(settingsData.fieldOfView);
        menuOptions[ExtractMenuItem("Brightness").ID].SetValue(settingsData.brightness);
        //menuOptions[ExtractMenuItem("Quality").ID].SetValue(settingsData.quality);
        menuOptions[ExtractMenuItem("Fullscreen").ID].SetValue(settingsData.fullscreen);
       
        //From Resolution to string
        
        //string resolution = settingsData.screenResolution.ToString();
       // menuOptions[ExtractMenuItem("Resolution").ID].SetValue(resolution);
        */
    }

    public override void ApplyItemValues(ref SettingsData settingsData) {
        /*
        settingsData.fieldOfView = menuOptions[ExtractMenuItem("Field of View").ID].GetValue();
        settingsData.brightness = menuOptions[ExtractMenuItem("Brightness").ID].GetValue();
        settingsData.quality = menuOptions[ExtractMenuItem("Quality").ID].GetValue();
        settingsData.fullscreen = menuOptions[ExtractMenuItem("Fullscreen").ID].GetValue();
        settingsData.screenResolution = "1920x1080";
*/
    }

 
    
}
