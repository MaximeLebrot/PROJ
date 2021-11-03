using UnityEngine;


namespace NewCamera { 
    
    [System.Serializable]
    public class GlideBaseCameraBehaviour : BaseCameraBehaviour {
        
        public GlideBaseCameraBehaviour(Transform transform, Transform target, OffsetAndCameraSpeed values) : base(transform, target, values) {}
        
        public override Vector3 ExecuteCollision(Vector2 input, CameraBehaviourData data) {
            
            Vector3 collisionOffset = target.rotation * values.offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
            
        }

        public override Quaternion ExecuteRotate() {
            return base.ExecuteRotate();
        }

        public override Vector2 ClampMovement(Vector2 input, Vector2 values) {
            input = base.ClampMovement(input, values); //Clamped on the x-axis

            input.y = Mathf.Clamp(input.y, -45, 45);

            return input;
        }
    }
}