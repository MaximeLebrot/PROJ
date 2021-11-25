using System.Threading.Tasks;
using UnityEngine;

public class ResetBehindPlayerTransition : BasicCameraTransition {

    private readonly Vector3 endPosition;
    private readonly Quaternion endRotation;
    private readonly float moveSpeed;

    
    public ResetBehindPlayerTransition(ref Transform transform, Vector3 endPosition, Quaternion endRotation, float moveSpeed) : base(ref transform) {
        this.endPosition = endPosition;
        this.endRotation = endRotation;
        this.moveSpeed = moveSpeed;
    }

    public override async Task Transit() {

        float currentDistance = float.PositiveInfinity;

        while (currentDistance > .1f) {
            transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref referenceVelocity, moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, Time.deltaTime * 20);
            currentDistance = Vector3.Distance(transform.position, endPosition);
            
            await Task.Yield();
        }
        
    }
}
