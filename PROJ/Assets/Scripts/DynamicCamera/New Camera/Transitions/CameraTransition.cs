using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraTransition<T> {

    protected readonly Transform transform;
    protected readonly T eventData;
    protected Vector3 referenceVelocity;


    protected CameraTransition(ref Transform transform, T eventData) {
        this.transform = transform;
        this.eventData = eventData;
    }

    public abstract Task Transit();
    
}
