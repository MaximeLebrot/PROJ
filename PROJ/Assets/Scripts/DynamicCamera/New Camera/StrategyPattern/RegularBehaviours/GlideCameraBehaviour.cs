using System;
using UnityEngine;

namespace NewCamera { 
    
    [Serializable]
    public class GlideCameraBehaviour : BaseCameraBehaviour {

        public GlideCameraBehaviour(Transform transform, Transform target, BehaviourData behaviourValues) : base(transform, target, behaviourValues) {}
        
        public override Quaternion ExecuteRotate() {
            
            Quaternion targetRotation = Quaternion.LookRotation(((target.position - thisTransform.position) + BehaviourData<GlideData>().LookRotationOffset));
            
            return Quaternion.Slerp(thisTransform.rotation, targetRotation, Time.deltaTime * behaviourValues.RotationSpeed);
        }
        
        
    }
    
    
}