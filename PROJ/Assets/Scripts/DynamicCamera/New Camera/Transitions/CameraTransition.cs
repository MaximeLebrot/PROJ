using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraTransition 
{
    protected readonly Transform transform;
    protected Vector3 referenceVelocity;
    
    protected CameraTransition(ref Transform transform) => this.transform = transform;
    
    public abstract Task Transit();
}
