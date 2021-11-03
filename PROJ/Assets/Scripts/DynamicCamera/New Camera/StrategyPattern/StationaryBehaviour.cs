using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    public class StationaryBehaviour : CameraBehaviour
    {

        public StationaryBehaviour(Transform transform, Transform target, Vector3 offset) : base(transform, target, offset)
        {
        }

        public override Quaternion ExecuteRotate()
        {
            Vector3 lookDirection = (target.position - thisTransform.position).normalized;

            return Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}