using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    public class StationaryBehaviour : BaseCameraBehaviour
    {

        public StationaryBehaviour(Transform transform, Transform target, BehaviourData values) : base(transform, target, values) {}
        
        public override Quaternion ExecuteRotate()
        {
            Vector3 lookDirection = (target.position - thisTransform.position).normalized;

            return Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}