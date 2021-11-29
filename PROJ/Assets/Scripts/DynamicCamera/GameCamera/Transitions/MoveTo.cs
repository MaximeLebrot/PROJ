using System.Threading.Tasks;
using UnityEngine;

public class MoveTo : CameraTransition<MoveToTransitionData> {

    public Vector3 endPosition;

    private Vector3 referenceVelocity;

    public MoveTo(Vector3 endPosition, MoveToTransitionData transitionData) : base(transitionData) {
        this.endPosition = endPosition;
    }

    public override async Task RunTransition(Transform transform) {

        while (Vector3.Distance(transform.position, endPosition) > .1f) {
            transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref referenceVelocity, transitionData.MoveSpeed);
            await Task.Yield();
        }
    }

}
    

