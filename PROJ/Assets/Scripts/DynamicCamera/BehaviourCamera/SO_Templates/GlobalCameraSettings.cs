using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Data/Camera Behaviour Data)", fileName = "Camera Behaviour Data")]
public class GlobalCameraSettings : ScriptableObject {

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask collisionMask;

    public float MouseSensitivity => mouseSensitivity;
    public float CollisionRadius => collisionRadius;
    public LayerMask CollisionMask => collisionMask;
}
