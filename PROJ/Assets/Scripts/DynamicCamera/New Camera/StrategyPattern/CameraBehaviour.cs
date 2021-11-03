using UnityEngine;

namespace NewCamera
{
    
[System.Serializable]
public class CameraBehaviour {

    protected Vector3 referenceVelocity;
    protected readonly Transform thisTransform;
    protected readonly Transform target;
    protected readonly Vector3 offset;

    public CameraBehaviour(Transform transform, Transform target, Vector3 offset) {
        thisTransform = transform;
        this.target = target;
        this.offset = offset;
    } 
    
    public virtual Vector3 ExecuteMove(Vector3 calculatedOffset, float followSpeed) {
        return Vector3.SmoothDamp(thisTransform.position, target.position + calculatedOffset, ref referenceVelocity, followSpeed);
    }

    public virtual Quaternion ExecuteRotate() => target.rotation;

    public virtual Vector3 ExecuteCollision(Vector2 input, CameraBehaviourData data) {
        
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
}