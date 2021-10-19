using UnityEngine;

namespace DynamicCamera {
    public abstract class CameraBehaviour : ScriptableObject {

        [SerializeField] public CameraData cameraData;
        
        public abstract void ExecuteBehaviour(Transform cameraTransform, Transform target);
    }
}
