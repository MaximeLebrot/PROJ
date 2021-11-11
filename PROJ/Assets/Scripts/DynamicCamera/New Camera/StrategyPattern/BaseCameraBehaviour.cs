using UnityEngine;

namespace NewCamera {
        
    [System.Serializable]
    public class BaseCameraBehaviour {

        protected Vector3 referenceVelocity;
        protected readonly Transform thisTransform;
        protected readonly Transform target;
        public readonly BehaviourData values;

        private Vector3 previousRotation;
        
        public BaseCameraBehaviour(Transform transform, Transform target, BehaviourData values) {
            thisTransform = transform;
            this.target = target;
            this.values = values;
            previousRotation = target.eulerAngles;
        } 
        
        public virtual Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(thisTransform.position, target.position + calculatedOffset, ref referenceVelocity, values.FollowSpeed);
        }

        public virtual Quaternion ExecuteRotate() {
            
            Quaternion targetRotation = Quaternion.LookRotation((target.position - thisTransform.position));
            
            return Quaternion.Slerp(thisTransform.rotation, targetRotation, Time.deltaTime * 50);
        }

        public virtual Vector3 ExecuteCollision(GlobalCameraSettings data) {
            
            Vector3 collisionOffset = target.rotation * values.Offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
        }

        public virtual void ManipulatePivotTarget(CustomInput input, Vector2 clampValues) {
            
            if (input.aim == Vector2.zero && input.movement.x == 0) {
                target.eulerAngles = previousRotation;
                return;
            }
            
            if(input.aim == Vector2.zero && input.movement.x != 0) {
                Vector3 parentRotation = target.parent.eulerAngles;
                parentRotation.x = 0;
                target.eulerAngles = parentRotation;
                previousRotation = target.eulerAngles;
                return;
            }
            
   
            

            Vector3 desiredRotation  = target.eulerAngles + (Vector3)input.aim;
            
            if (desiredRotation.x > 180)
                desiredRotation.x -= 360;
            if (desiredRotation.y > 180)
                desiredRotation.y -= 360;
            
            desiredRotation.x = Mathf.Clamp(desiredRotation.x, clampValues.x, clampValues.y);
            
            target.eulerAngles = desiredRotation;
            previousRotation = target.eulerAngles;

        }
        
        
        
    }
}