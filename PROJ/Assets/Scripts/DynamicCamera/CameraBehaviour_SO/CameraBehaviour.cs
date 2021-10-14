using UnityEngine;

namespace DynamicCamera {
    public abstract class CameraBehaviour : ScriptableObject {

        [SerializeField] protected CameraData cameraData;
        
        public abstract void ExecuteBehaviour(Transform cameraTransform, Transform target);
    }
}
