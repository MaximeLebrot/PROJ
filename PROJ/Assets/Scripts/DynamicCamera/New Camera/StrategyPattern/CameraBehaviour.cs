using UnityEngine;

[System.Serializable]
public class CameraBehaviour {

    protected Vector3 referenceVelocity;
    protected readonly Transform thisTransform;
    protected readonly Transform target;

    public CameraBehaviour(Transform transform, Transform target) {
        thisTransform = transform;
        this.target = target;
    } 
    
    public virtual Vector3 ExecuteMove(Vector3 offset, float followSpeed) {
        return Vector3.SmoothDamp(thisTransform.position, target.position + offset, ref referenceVelocity, followSpeed);
    }

    public virtual Quaternion ExecuteRotate() {
        Vector3 direction = (target.position - thisTransform.position).normalized;
        
        return Quaternion.LookRotation(direction, Vector3.up);
    }
    
    public virtual Vector3 ExecuteCollision(Vector2 input, Vector3 offset, CameraBehaviourData data) {
        
        target.rotation = Quaternion.Euler(input.x, input.y, 0);
        
        Vector3 collisionOffset = target.rotation * offset;
        
        if (Physics.SphereCast(target.position, data.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, data.CollisionMask))
            collisionOffset = collisionOffset.normalized * hitInfo.distance;

        return collisionOffset;
    }

    public virtual Vector2 ClampMovement(Vector2 input, Vector2 values) {
        input.x = Mathf.Clamp(input.x, values.x, values.y);

        return input;
    }
}
