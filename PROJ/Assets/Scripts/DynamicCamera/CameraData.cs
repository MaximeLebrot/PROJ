using UnityEngine;

public struct CameraData {
    
    public Vector3 Offset;
    public float MovementSpeed;
    public float MouseSensitivity;
    public float RotationSpeed;
    public SphereCollider Collider;
    public LayerMask CollisionMask;

    public CameraData(Vector3 offset, SphereCollider collider, float movementSpeed, float rotationSpeed, float mouseSensitivity, LayerMask collisionMask) {
        Offset = offset;
        Collider = collider;
        MovementSpeed = movementSpeed;
        RotationSpeed = rotationSpeed;
        MouseSensitivity = mouseSensitivity;
        CollisionMask = collisionMask;
    }
}
