using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraTransition {

    protected readonly Transform transform;
    protected readonly Vector3 endPosition;
    protected readonly Quaternion endRotation;

    public CameraTransition(ref Transform transform, Vector3 endPosition, Quaternion endRotation ) {
        this.transform = transform;
        this.endPosition = endPosition;
        this.endRotation = endRotation;
    }

    public abstract Task Transit();

}
