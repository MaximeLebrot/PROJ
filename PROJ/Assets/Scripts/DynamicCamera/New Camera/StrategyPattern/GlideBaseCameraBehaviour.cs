using System;
using UnityEngine;


namespace NewCamera { 
    
    [Serializable]
    public class GlideBaseCameraBehaviour : BaseCameraBehaviour {
        
        public GlideBaseCameraBehaviour(Transform transform, Transform target, BehaviourData values, bool isInputBehaviour) : base(transform, target, values, isInputBehaviour) {}
        
        public override Vector3 ExecuteCollision(Vector2 input, GlobalCameraSettings data) {
            
            target.rotation = Quaternion.Euler(input.x, input.y, 0);

            Vector3 collisionOffset = target.rotation * values.Offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;
            
            return collisionOffset;
        }
        
    }
}