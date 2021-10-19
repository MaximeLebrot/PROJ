using UnityEngine;

namespace DynamicCamera {
    
    [CreateAssetMenu(menuName = "DynamicCamera/Camera Behaviour/Follow Behaviour", fileName = "FollowBehaviour")]
    public class FollowBehaviour : CameraBehaviour {
        
        private const string XRotationInputName = "Mouse Y";
        private const string YRotationInputName = "Mouse X";
    
        private Vector2 input;
        
        public override void ExecuteBehaviour(Transform cameraTransform, Transform target) {
            GetInput();
            RotateCamera(cameraTransform);
            MoveCamera(cameraTransform, target);
        }
        
        private void GetInput() {
            input.x -= Input.GetAxis(XRotationInputName) * CameraData.MouseSensitivity;
            input.y += Input.GetAxis(YRotationInputName) * CameraData.MouseSensitivity;

            input.x = Mathf.Clamp(input.x, -80, 80);
        }
        
        private void RotateCamera(Transform transform) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(input.x, input.y, 0), CameraData.RotationSpeed * Time.time);
        }

        private void MoveCamera(Transform transform, Transform target) {

            Vector3 collisionOffset = transform.rotation * CameraData.Offset;
            
            if (Physics.SphereCast(target.position, CameraData.Collider.radius, collisionOffset.normalized, out RaycastHit hitInfo, collisionOffset.magnitude, CameraData.CollisionMask)) {

                Vector3 projectOnPlane = Vector3.ProjectOnPlane(collisionOffset, hitInfo.normal);

                collisionOffset = projectOnPlane;

            }

            Debug.DrawLine(target.position, target.position + collisionOffset, Color.red);
            transform.position = Vector3.Slerp(transform.position, target.position + collisionOffset, CameraData.MovementSpeed * Time.deltaTime);    

        }
    }
}
