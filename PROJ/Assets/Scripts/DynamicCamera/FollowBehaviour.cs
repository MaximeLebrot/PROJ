using UnityEngine;

namespace DynamicCamera {
    
    public class FollowBehaviour : CameraBehaviour {
        
        private const string XRotationInputName = "Mouse Y";
        private const string YRotationInputName = "Mouse X";
    
        private Vector2 input;
    
        private readonly Vector3 offset;
        private readonly float mouseSensitivity;
        private readonly float speed;
    
        public FollowBehaviour(Vector3 offset, float mouseSensitivity, float speed) {
            this.mouseSensitivity = mouseSensitivity;
            this.offset = offset;
            this.speed = speed;
        }
    
        public override void Behave(Transform cameraTransform, Transform target) {
            GetInput();
            RotateCamera(cameraTransform);
            MoveCamera(cameraTransform, target);
        }
        
        private void GetInput() {
            input.x -= Input.GetAxis(XRotationInputName) * mouseSensitivity;
            input.y += Input.GetAxis(YRotationInputName) * mouseSensitivity;

            input.x = Mathf.Clamp(input.x, -40, 40);
        }
        
        private void RotateCamera(Transform transform) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(input.x, input.y, 0), speed * Time.deltaTime);
        }

        private void MoveCamera(Transform transform, Transform target) {
            Vector3 offsetPosition = transform.rotation * offset;
        
            transform.position = Vector3.Lerp(transform.position, target.position + offsetPosition, speed * Time.deltaTime);
        }
    }
}
