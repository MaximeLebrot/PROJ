using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/WorldBehaviour", fileName = "World Behaviour")]
public class WorldBehaviour : CameraBehaviour {
    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 eulerRotation;

    [SerializeField] private float travelTime;
    [SerializeField] private float maxSpeed;
    
    private Vector3 velocity;
    
    public override void Behave() {
                
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, travelTime, maxSpeed);
        
        transform.eulerAngles = eulerRotation;
    }
}
