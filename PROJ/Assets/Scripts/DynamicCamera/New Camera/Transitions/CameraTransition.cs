using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class CameraTransition : ScriptableObject
{
    protected readonly Transform transform;
    protected Vector3 referenceVelocity;
    
    protected CameraTransition(ref Transform transform) => this.transform = transform;

    public abstract Task Transit();
    
}
