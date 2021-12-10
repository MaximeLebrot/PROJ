using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ControllerValues/ControllerValues")]
public class ControllerValues : ScriptableObject
{
    //Friction
    [Range(0, 1)]public float staticFriction;
    [Range(0, 1)]public float kineticFriction;
    [Range(0, 1)]public float airResistance;

    public float maxSpeed;
    public float gravity = 9.81f;
    
}
