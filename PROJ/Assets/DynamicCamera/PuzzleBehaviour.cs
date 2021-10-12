using UnityEngine;

namespace DynamicCamera {
    
    public class PuzzleBehaviour : CameraBehaviour {

        private readonly float speed;
    
        public PuzzleBehaviour(float speed) {
            this.speed = speed;
        }
    
        public override void Behave(Transform cameraTransform, Transform target) {
            RotateCamera(cameraTransform, target);
            MoveCamera(cameraTransform, target);
        }
    
        private void RotateCamera(Transform transform, Transform targetPosition) {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPosition.rotation, speed * Time.deltaTime);
        }

        private void MoveCamera(Transform transform, Transform target) {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
