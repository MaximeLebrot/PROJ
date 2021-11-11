using System;
using UnityEngine;

namespace NewCamera { 
    
    [Serializable]
    public class GlideBaseCameraBehaviour : BaseCameraBehaviour {
        public GlideBaseCameraBehaviour(Transform transform, Transform target, BehaviourData behaviourValues) : base(transform, target, behaviourValues) {}
        
    }
}