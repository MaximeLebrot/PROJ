using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Behaviour Data/Default Behaviour Data", fileName = "New Default Behaviour Data")]
public class BehaviourData : ScriptableObject {

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector2 clampValues;
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float inputDeadZone;
    public Vector3 Offset => offset;
    public float FollowSpeed => followSpeed;
    public float RotationSpeed => rotationSpeed;
    public float InputDeadZone => inputDeadZone;

    public Vector2 ClampValues => clampValues;
}
