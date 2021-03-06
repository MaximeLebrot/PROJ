using System;
using UnityEngine;

namespace NewCamera { 
    
    [Serializable]
    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Glide Behaviour", fileName = "Glide Behaviour")]
    public class GlideCameraBehaviour : BaseCameraBehaviour {
        
        public override Quaternion ExecuteRotate() {
            
            Debug.Log("Glide camera");
            
            Quaternion targetRotation = Quaternion.LookRotation(((target.position - thisTransform.position) + BehaviourData<GlideData>().LookRotationOffset));
            
            return Quaternion.Slerp(thisTransform.rotation, targetRotation, Time.deltaTime * behaviourValues.RotationSpeed);
        }
        
        
    }
    
    
}