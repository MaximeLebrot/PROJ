using UnityEngine;

public class ResolutionHandler : MonoBehaviour {

    private Resolutioner resolutioner;
    
    private void Awake() {
        (GameMenuController.Instance.RequestOption<SResolution>() as SResolution).AddListener(ChangeScreenResolution);
    }

    private void ChangeScreenResolution(int value) {
        string resolution = (GameMenuController.Instance.RequestOption<SResolution>() as SResolution).GetValue();

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
