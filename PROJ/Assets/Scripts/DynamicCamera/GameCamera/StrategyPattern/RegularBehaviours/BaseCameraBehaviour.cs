using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NewCamera {
        
    [Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Base Behaviour", fileName = "Base Behaviour")]
    public class BaseCameraBehaviour : ScriptableObject {
        
        [SerializeField] protected BehaviourData behaviourValues;
        
        protected Vector3 referenceVelocity;
        protected Quaternion previousRotation;
        protected Transform thisTransform;
        protected Transform target;

        //"Constructor"
        public virtual void InjectReferences(Transform transform, Transform target) {
            thisTransform = transform;
            this.target = target;
        }
        
        public virtual void EnterBehaviour() => previousRotation = target.rotation;


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

        public virtual void ManipulatePivotTarget(CustomInput input) {
            
           /* //If no input or movement.x is 0 OR lower than input dead zone (for controllers). 
            if (input.aim == Vector2.zero && (input.movement.x == 0 || Mathf.Abs(input.movement.x) < behaviourValues.InputDeadZone)) {
                target.rotation = previousRotation;
                return;
            }
            */

           //If no input is given, don't rotate with parent 
           if (input.aim == Vector2.zero) {
               target.rotation = previousRotation;
               return;
           }

           Vector3 desiredRotation  = target.eulerAngles + (Vector3)input.aim;
            
            if (desiredRotation.x > 180)
                desiredRotation.x -= 360;
            if (desiredRotation.y > 180)
                desiredRotation.y -= 360;
            
            desiredRotation.x = Mathf.Clamp(desiredRotation.x, behaviourValues.ClampValues.x, behaviourValues.ClampValues.y);
            
            target.eulerAngles = desiredRotation;
            previousRotation = target.rotation;
        }

        protected T BehaviourData<T>() where T : BehaviourData => behaviourValues as T;
    }
}