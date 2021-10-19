using UnityEngine;
using UnityEngine.InputSystem;

namespace DynamicCamera {
    
    [CreateAssetMenu(menuName = "DynamicCamera/Camera Behaviour/Follow Behaviour", fileName = "FollowBehaviour")]
    public class FollowBehaviour : CameraBehaviour {
        
        private const string XRotationInputName = "Mouse Y";
        private const string YRotationInputName = "Mouse X";
    
        private Vector2 input;
        private InputMaster inputMaster;

        private void OnEnable()
        {
            inputMaster = new InputMaster();
            inputMaster.Enable();
        }
        private void OnDisable()
        {
            inputMaster.Disable();
        }

        public override void ExecuteBehaviour(Transform cameraTransform, Transform target) {
            GetInput();
            RotateCamera(cameraTransform);
            MoveCamera(cameraTransform, target);
        }
        
        private void GetInput() {
            input.x -= inputMaster.Player.MoveCamera.ReadValue<Vector2>().y * cameraData.mouseSensitivity;
            input.y += inputMaster.Player.MoveCamera.ReadValue<Vector2>().x * cameraData.mouseSensitivity;

            input.x = Mathf.Clamp(input.x, -80, 80);
        }
        
        private void RotateCamera(Transform transform) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(input.x, input.y, 0), cameraData.rotationSpeed * Time.time);
        }

        private void MoveCamera(Transform transform, Transform target) {
            Vector3 offsetPosition = transform.rotation * cameraData.offset;

            transform.position = Vector3.Slerp(transform.position, target.position + offsetPosition, cameraData.movementSpeed * Time.deltaTime);
            
        }
        
    }
}
