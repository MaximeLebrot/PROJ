using System;
using UnityEngine;


namespace NewCamera { 
    
    [Serializable]
    public class GlideBaseCameraBehaviour : BaseCameraBehaviour {
        
        public GlideBaseCameraBehaviour(Transform transform, Transform target, OffsetAndCameraSpeed values) : base(transform, target, values) {}
        
        public override Vector3 ExecuteCollision(Vector2 input, CameraBehaviourData data) {
            
            target.rotation = Quaternion.Euler(input.x, input.y, 0);

            Vector3 collisionOffset = target.rotation * values.offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;
            
            return collisionOffset;
        }
        
    }
}