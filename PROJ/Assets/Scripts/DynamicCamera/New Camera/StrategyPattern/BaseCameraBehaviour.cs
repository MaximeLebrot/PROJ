using UnityEngine;

namespace NewCamera {
        
    [System.Serializable]
    public class BaseCameraBehaviour {

        protected Vector3 referenceVelocity;
        protected readonly Transform thisTransform;
        protected readonly Transform target;
        public readonly BehaviourData values;
        
        public BaseCameraBehaviour(Transform transform, Transform target, BehaviourData values) {
            thisTransform = transform;
            this.target = target;
            this.values = values;
        } 
        
        public virtual Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(thisTransform.position, target.position + calculatedOffset, ref referenceVelocity, values.FollowSpeed);
        }

        public virtual Quaternion ExecuteRotate() {
            
            Quaternion targetRotation = Quaternion.LookRotation((target.position - thisTransform.position));
            
            return Quaternion.Slerp(thisTransform.rotation, targetRotation, Time.deltaTime * 50);
        }

        public virtual Vector3 ExecuteCollision(Vector2 input, GlobalCameraSettings data) {

            target.rotation = Quaternion.Euler(input.x, input.y, 0);
            
            Vector3 collisionOffset = target.rotation * values.Offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
        }
        
        
        public virtual Vector2 ClampMovement(Vector2 input, Vector2 clampValues) {
            input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);

            return input;
        }
        
    }
}