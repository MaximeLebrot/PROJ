using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Behaviour Data", fileName = "New Behaviour Data")]
public class BehaviourData : ScriptableObject {

    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;

    public Vector3 Offset => offset;
    public float FollowSpeed => followSpeed;

}
