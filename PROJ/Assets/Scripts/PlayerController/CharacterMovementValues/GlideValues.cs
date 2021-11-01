using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GlideValues/ControllerValues")]
public class GlideValues : ControllerValues
{
    [Header("Movement Smoothing")]
    public float smoothingMaxDistance;
    public int powerOf;
    public float surfThreshold;
}
