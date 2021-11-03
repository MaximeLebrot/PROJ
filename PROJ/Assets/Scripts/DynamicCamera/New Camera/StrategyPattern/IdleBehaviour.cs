using NewCamera;
using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    public class IdleBehaviour : CameraBehaviour
    {
        public IdleBehaviour(Transform transform, Transform target, Vector3 offset) : base(transform, target, offset) {}

        public override Quaternion ExecuteRotate()
        {
            return Quaternion.identity;
        }
    }

}