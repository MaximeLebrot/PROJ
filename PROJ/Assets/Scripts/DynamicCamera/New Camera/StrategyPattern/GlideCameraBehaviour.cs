using UnityEngine;


namespace NewCamera { 
    
    [System.Serializable]
    public class GlideCameraBehaviour : CameraBehaviour {
        
        public GlideCameraBehaviour(Transform transform, Transform target, Vector3 offset) : base(transform, target, offset) {}
        
        public override Vector3 ExecuteCollision(Vector2 input, CameraBehaviourData data) {
            
            Vector3 collisionOffset = target.rotation * offset;
            
            if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
                collisionOffset = collisionOffset.normalized * hitInfo.distance;

            return collisionOffset;
            
        }

        public override Vector2 ClampMovement(Vector2 input, Vector2 values) {
            input = base.ClampMovement(input, values); //Clamped on the x-axis

            input.y = Mathf.Clamp(input.y, -45, 45);

            return input;
        }
    }
}