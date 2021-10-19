using System;
using UnityEngine;

namespace DynamicCamera {
    public abstract class CameraBehaviour : ScriptableObject {

        protected CameraData CameraData;

        public void Initialize(CameraData cameraData) => CameraData = cameraData;
        
        public abstract void ExecuteBehaviour(Transform cameraTransform, Transform target);
    }
}


