using System;
using UnityEngine;

namespace NewCamera
{
    [Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Behaviour", fileName = "Idle Behaviour")]
    public class IdleBehaviour : BaseCameraBehaviour {
        
        private float pointOnCurve;
        public override void EnterBehaviour() {
            pointOnCurve = 0;
        }

        public override Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(ThisTransform.position, PivotTarget.parent.position + calculatedOffset, ref ReferenceVelocity, behaviourValues.FollowSpeed);
        }

        public override Quaternion ExecuteRotate() {

            float newIndex = BehaviourData<IdleBehaviourData>().RotationCurve.Evaluate(pointOnCurve);
            pointOnCurve +=  Time.deltaTime / BehaviourData<IdleBehaviourData>().FollowSpeed;
            
            return Quaternion.Lerp(ThisTransform.rotation, Quaternion.LookRotation(PivotTarget.parent.forward), newIndex);
        }

        public override Vector3 ExecuteCollision(GlobalCameraSettings data) {
            
            Vector3 collisionOffset = PivotTarget.parent.rotation * BehaviourData<IdleBehaviourData>().Offset;
        
            if (Physics.SphereCast(PivotTarget.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
            
        }
        
        

        public override void ManipulatePivotTarget(CustomInput input) => PivotTarget.rotation = ThisTransform.rotation;
        
    }

}