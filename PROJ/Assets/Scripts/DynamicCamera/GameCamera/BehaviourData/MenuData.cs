using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Behaviour Data/Menu Data", fileName = "Menu Data")]
public class MenuData : BehaviourData {
    [SerializeField] private Vector3 eulerRotation;

    public Vector3 EulerRotation => eulerRotation;

}
