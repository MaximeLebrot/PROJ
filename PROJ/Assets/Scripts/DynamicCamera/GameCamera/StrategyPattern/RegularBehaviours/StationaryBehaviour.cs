using UnityEngine;

namespace NewCamera
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Stationary Behaviour", fileName = "Stationary Behaviour")]
    public class StationaryBehaviour : BaseCameraBehaviour
    {

        public override Quaternion ExecuteRotate()
        {
            Vector3 lookDirection = (target.position - thisTransform.position).normalized;

            return Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}