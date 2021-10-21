using UnityEngine;

public class PuzzleCamera : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 eulerRotation;

    [SerializeField] private float travelTime;
    [SerializeField] private float maxSpeed;
    
    private Vector3 velocity;
    
    private void LateUpdate() {
        
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, travelTime, maxSpeed);
        
        transform.eulerAngles = eulerRotation;
    }
    
    
}
