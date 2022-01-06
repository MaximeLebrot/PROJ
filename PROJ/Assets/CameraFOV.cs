using UnityEngine;

public class CameraFOV : MonoBehaviour {
    
    private Camera camera;

    private void Awake() {
        camera = GetComponent<Camera>();
        
        FieldOfView fov = GameMenuController.Instance.RequestOption<FieldOfView>() as FieldOfView;
        
        
        fov?.AddListener(UpdateFOV);
        UpdateFOV(fov.GetValue());
    }


    private void UpdateFOV(float value) => camera.fieldOfView = (int)(value * 100);
}
