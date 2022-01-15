using System;
using UnityEngine;

namespace NewCamera { 
    
    [Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Glide Behaviour", fileName = "Glide Behaviour")]
    public class GlideCameraBehaviour : BaseCameraBehaviour {
        
        public override Quaternion ExecuteRotate() {
            
            Quaternion targetRotation = Quaternion.LookRotation(((PivotTarget.position - ThisTransform.position) + BehaviourData<GlideData>().LookRotationOffset));
            
            return Quaternion.Slerp(ThisTransform.rotation, targetRotation, Time.deltaTime * behaviourValues.RotationSpeed);
        }
        
        
    }
    
    
}