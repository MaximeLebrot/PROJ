using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Camera/Behaviour Data/Glide Behaviour Data", fileName = "New Glide Behaviour Data")]
public class GlideBehaviourData : BehaviourData {

    [SerializeField] private float rotationSpeed;

    public float RotationSpeed => rotationSpeed;
}
