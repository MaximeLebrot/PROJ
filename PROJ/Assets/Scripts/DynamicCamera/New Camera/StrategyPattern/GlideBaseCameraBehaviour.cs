using System;
using UnityEngine;


namespace NewCamera { 
    
    [Serializable]
    public class GlideBaseCameraBehaviour : BaseCameraBehaviour {

        private GlideBehaviourData glideBehaviourData;

        public GlideBaseCameraBehaviour(Transform transform, Transform target, BehaviourData values) : base(transform, target, values) {
            glideBehaviourData = values as GlideBehaviourData;
        }

        public override Vector3 ExecuteCollision(GlobalCameraSettings data) {
            
            /*
            target.rotation = input != Vector2.zero ? Quaternion.Euler(input.x, input.y, 0) : Quaternion.Lerp(target.rotation, target.parent.rotation, Time.deltaTime * glideBehaviourData.RotationSpeed);
            */
            
            Vector3 collisionOffset = target.rotation * values.Offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;
            
            return collisionOffset;
        }
        
    }
}