using UnityEngine;

public class ResolutionHandler : MonoBehaviour {

    private Resolutioner resolutioner;
    
    private void Start() => (GameMenuController.Instance.RequestOption<SResolution>() as SResolution).AddListener(ChangeScreenResolution);

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
