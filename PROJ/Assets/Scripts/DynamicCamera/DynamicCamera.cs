using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField] private Transform target;

        [SerializeField] private CameraBehaviour cameraBehaviour;
        
        private Transform thisTransform;
        
        private void Awake() {
            Cursor.lockState = CursorLockMode.Locked;
            thisTransform = transform;


        }
        
        private void LateUpdate() => cameraBehaviour.ExecuteBehaviour(thisTransform, target);
        
    }
    
}

