using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resolutioner {

    private TMP_Dropdown resolutionList;

    public Resolutioner(TMP_Dropdown resolutionList, bool autoDetectResolution) {
        this.resolutionList = resolutionList;
        AddResolutionOptions();
        
        //if(autoDetectResolution)
          //  AutoDetectResolution();
    }

    private void AutoDetectResolution() {
        int resolutionIndex = GetResolutionOptionIndex(Screen.currentResolution);

        Resolution autoDetectedSResolution = ConvertStringToResolution(resolutionIndex);
        
        Screen.SetResolution(autoDetectedSResolution.width, autoDetectedSResolution.height, FullScreenMode.FullScreenWindow);
    }
    
    private void AddResolutionOptions() {

        List<string> options = new List<string>(Screen.resolutions.Length);

      /*  foreach (Resolution resolution in Screen.resolutions) {
            string[] displayReadyText = resolution.ToString().Split(' ');
            options.Add(displayReadyText[0] + "x" + displayReadyText[2]);
        }
        */
        resolutionList.AddOptions(options);
    }
    
    private int GetResolutionOptionIndex(Resolution resolution) {
        return resolutionList.options.FindIndex(resolutionOption => resolutionOption.text.Equals($"{resolution.width}x{resolution.height}"));
    }
    
    private Resolution ConvertStringToResolution(int index) {
        
        string[] chosenResolution = resolutionList.options[index].text.Split('x');
        
        int width = int.Parse(chosenResolution[0]);
        int height = int.Parse(chosenResolution[2]);

        Resolution sResolution = new Resolution {
            width = width,
            height = height
        };

        return sResolution;
    }
    
}
