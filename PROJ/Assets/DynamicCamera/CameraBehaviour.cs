using UnityEngine;

namespace DynamicCamera {
    public abstract class CameraBehaviour {
    
        public abstract void Behave(Transform cameraTransform, Transform target);
    }
}
