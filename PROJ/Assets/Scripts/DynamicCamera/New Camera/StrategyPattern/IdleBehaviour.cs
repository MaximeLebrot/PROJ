using NewCamera;
using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    public class IdleBehaviour : CameraBehaviour
    {
        public IdleBehaviour(Transform transform, Transform target, OffsetAndCameraSpeed values) : base(transform, target, values) {
            Debug.Log("Created idleBehaviour");
        }

        public override Vector3 ExecuteMove(Vector3 calculatedOffset) {
            return Vector3.SmoothDamp(thisTransform.position, target.parent.position + calculatedOffset, ref referenceVelocity, values.followSpeed);
        }

        public override Quaternion ExecuteRotate()
        {
            return Quaternion.Lerp(thisTransform.rotation, Quaternion.LookRotation(target.parent.forward), Time.deltaTime);
        }

        public override Vector3 ExecuteCollision(Vector2 input, CameraBehaviourData data) {
            
            Vector3 collisionOffset = target.parent.rotation * values.offset;
        
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
            
        }
    }

}