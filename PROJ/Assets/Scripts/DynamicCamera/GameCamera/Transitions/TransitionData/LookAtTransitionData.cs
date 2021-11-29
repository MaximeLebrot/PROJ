using UnityEngine;

/// <summary>     
/// Used for rotating in place, towards some destination
/// </summary>    
[CreateAssetMenu(menuName = "Camera/Camera Transitions/Look At/Look At Event Data")]   
public class LookAtTransitionData : TransitionData {
    
    [SerializeField] private float rotationSpeed;
    
    public float RotationSpeed => rotationSpeed;
}
 