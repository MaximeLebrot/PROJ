
using System.Threading.Tasks;
using UnityEngine;

public class MoveToTransition : EventDataCameraTransition<MoveToEventData> {
    
    private readonly Vector3 endPosition;

    public MoveToTransition(ref Transform transform, Vector3 endPosition, MoveToEventData moveToEventData) : base(ref transform, moveToEventData) {
        this.endPosition = endPosition;
    }
    
    public override async Task Transit() {
        float startTime = Time.time + eventData.TransitionTime;

        while (Time.time < startTime) {
            transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref referenceVelocity, eventData.MoveSpeed);
            await Task.Yield();
        }
        await Task.Delay(3000);
    }

    
}
