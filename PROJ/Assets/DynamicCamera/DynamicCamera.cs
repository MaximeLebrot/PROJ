using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {

        [Header("Camera Follow Target")]
        [SerializeField] private Transform target;
        
        [Header("Settings")]
        [SerializeField] private Vector3 offset;
        [SerializeField] private float mouseSensitivity;
        [SerializeField][Range(0, 1)] private float averageMovementPerFrame;
        
        private const string XRotationInputName = "Mouse Y";
        private const string YRotationInputName = "Mouse X";

        private Transform thisTransform;
        
        private Vector2 input;

        private void Awake() {
            thisTransform = transform;
        }
        
        private void LateUpdate() {
            
            GetInput();

            MoveCamera();
            
            RotateCamera();
            
        }

        private void GetInput() {
            input.x -= Input.GetAxis(XRotationInputName) * mouseSensitivity * Time.deltaTime;
            input.y += Input.GetAxis(YRotationInputName) * mouseSensitivity * Time.deltaTime;
        }
        
        private void RotateCamera() {
            transform.rotation = Quaternion.Euler(input.x, input.y, 0);
        }

        private void MoveCamera() {
            Vector3 offsetPosition = thisTransform.rotation * offset;
            
            Vector3 positionBlend = (thisTransform.position * (1 - averageMovementPerFrame) + (target.position + offsetPosition) * averageMovementPerFrame);
            
            thisTransform.position = positionBlend;
        }

        private void RaycastFromTarget() {
            Ray ray = new Ray(target.position + Vector3.up, Vector3.forward + Vector3.down,  )
                
            Debug.Log();
            
        }
        
    }
    
}

