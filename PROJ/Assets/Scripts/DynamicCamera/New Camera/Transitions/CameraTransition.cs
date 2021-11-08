using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraTransition {

    protected readonly Transform transform;
    protected readonly Vector3 endPosition;
    protected Quaternion endRotation;
    protected Vector3 referenceVelocity;

    public CameraTransition(ref Transform transform, Vector3 endPosition, Quaternion endRotation ) {
        this.transform = transform;
        this.endPosition = endPosition;
        this.endRotation = endRotation;
    }

    public abstract Task Transit();

    public abstract bool IsTransitionDone();

}
