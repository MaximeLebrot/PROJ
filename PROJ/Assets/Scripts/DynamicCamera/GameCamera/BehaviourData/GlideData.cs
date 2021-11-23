using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Camera/Behaviour Data/Glide Behaviour Data", fileName = "Glide Data")]
public class GlideData : BehaviourData {

    [SerializeField] private Vector3 lookRotationOffset;

    public Vector3 LookRotationOffset => lookRotationOffset;
}
