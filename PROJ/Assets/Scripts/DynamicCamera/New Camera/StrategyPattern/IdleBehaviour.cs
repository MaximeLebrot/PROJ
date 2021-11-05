using System;
using UnityEngine;

namespace NewCamera
{
    [Serializable]
    public class IdleBehaviour : BaseCameraBehaviour {
        
        private IdleBehaviourData idleBehaviourData;

        private float pointOnCurve;
        
        public IdleBehaviour(Transform transform, Transform target, BehaviourData values) : base(transform, target, values) {
            idleBehaviourData = values as IdleBehaviourData;
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(ResetCurveCount);
        }

        public override Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(thisTransform.position, target.parent.position + calculatedOffset, ref referenceVelocity, values.FollowSpeed);
        }

        public override Quaternion ExecuteRotate() {

            float newIndex = idleBehaviourData.RotationCurve.Evaluate(pointOnCurve);
            pointOnCurve +=  Time.deltaTime / idleBehaviourData.FollowSpeed;
            
            return Quaternion.Lerp(thisTransform.rotation, Quaternion.LookRotation(target.parent.forward), newIndex);
        }

        public override Vector3 ExecuteCollision(Vector2 input, GlobalCameraSettings data) {
            
            Vector3 collisionOffset = target.parent.rotation * idleBehaviourData.Offset;
        
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
            
        }

        private void ResetCurveCount(AwayFromKeyboardEvent e) {
            pointOnCurve = 0;
            
            Debug.Log(pointOnCurve);
            
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(ResetCurveCount);
        }
    }

}