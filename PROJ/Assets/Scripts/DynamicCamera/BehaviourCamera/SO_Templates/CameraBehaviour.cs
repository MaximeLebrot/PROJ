using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CameraBehaviours {
    
    public abstract class CameraBehaviour : ScriptableObject {

        [SerializeField] protected ControllerInputReference inputReference;
        [SerializeField] protected GlobalCameraSettings globalCameraSettings;
        [SerializeField] protected Vector3 offset;
        [SerializeField] protected Vector2 clampValues;
        [SerializeField] protected float cameraMovementSpeed;
        [SerializeField] [Range(0, 1)] protected float rotationSpeed;

        protected Transform followTarget;
        protected Vector3 velocity;
        protected Transform transform;

        private Vector2 input;
        private Vector3 calculatedOffset;

        public virtual void Initialize(Transform objectTransform, Transform target) {
            transform = objectTransform;
            followTarget = target;


            Debug.Log("Init");

        }

        public void ExecuteBehaviour() {
            calculatedOffset = Vector3.zero;
            ReadInput();
            ClampInput();
            Collision();
            Behave();
        }

        protected virtual void Behave() {
            SmoothCollisionMovement();
            RotateCamera();
        }


        protected virtual async Task BehaveAsync() => await Task.Yield();

        protected virtual void SmoothCollisionMovement() {
            transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + calculatedOffset, ref velocity, cameraMovementSpeed);
        }

        protected virtual void Collision() {

            followTarget.rotation = Quaternion.Euler(input.x, input.y, 0);

            Vector3 collisionOffset = followTarget.rotation * offset;

            if (Physics.SphereCast(followTarget.position, globalCameraSettings.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, globalCameraSettings.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            calculatedOffset = collisionOffset;
        }

        protected virtual void RotateCamera() {

            Vector3 direction = (followTarget.position - transform.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
        }

        private void ReadInput() {
            Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();

            input.x += -inputDirection.y * globalCameraSettings.MouseSensitivity;
            input.y += inputDirection.x * globalCameraSettings.MouseSensitivity;

        }

        protected virtual void ClampInput() => input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);
    }
}
