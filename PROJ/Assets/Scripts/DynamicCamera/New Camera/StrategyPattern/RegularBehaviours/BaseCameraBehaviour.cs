using UnityEngine;

namespace NewCamera {
        
    [System.Serializable]
    public class BaseCameraBehaviour {

        protected Vector3 referenceVelocity;
        protected Quaternion previousRotation;
        protected readonly Transform thisTransform;
        protected readonly Transform target;
        public readonly BehaviourData behaviourValues;


        public BaseCameraBehaviour(Transform transform, Transform target, BehaviourData behaviourValues) {
            thisTransform = transform;
            this.target = target;
            this.behaviourValues = behaviourValues;
            previousRotation = target.rotation;
        } 
        
        public virtual Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(thisTransform.position, target.position + calculatedOffset, ref referenceVelocity, behaviourValues.FollowSpeed);
        }

        public virtual Quaternion ExecuteRotate() {
            
            Quaternion targetRotation = Quaternion.LookRotation((target.position - thisTransform.position));
            
            return Quaternion.Slerp(thisTransform.rotation, targetRotation, Time.deltaTime * behaviourValues.RotationSpeed);
        }

        public virtual Vector3 ExecuteCollision(GlobalCameraSettings data) {
            
            Vector3 collisionOffset = target.rotation * behaviourValues.Offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
        }

        public virtual void ManipulatePivotTarget(CustomInput input, Vector2 clampValues) {
            
            //If no input, use previous rotation
            if (input.aim == Vector2.zero && (input.movement.x == 0 || Mathf.Abs(input.movement.x) < .11f)) {
                target.rotation = previousRotation;
                return;
            }
            
            Vector3 desiredRotation  = target.eulerAngles + (Vector3)input.aim;
            
            if (desiredRotation.x > 180)
                desiredRotation.x -= 360;
            if (desiredRotation.y > 180)
                desiredRotation.y -= 360;
            
            desiredRotation.x = Mathf.Clamp(desiredRotation.x, clampValues.x, clampValues.y);
            
            target.eulerAngles = desiredRotation;
            previousRotation = target.rotation;
        }
        
    }
}