using System.Threading.Tasks;
using UnityEngine;

public class LookAtTransition : CameraTransition<LookAtEventData> {
    
    private readonly Quaternion endRotation;

    public LookAtTransition(ref Transform transform, Quaternion endRotation, LookAtEventData lookAtEventData) : base(ref transform, lookAtEventData) {
        this.endRotation = endRotation;
    }
    
    public override async Task Transit() {

        float startTime = Time.time + eventData.TransitionTime;

        while (Time.time < startTime) {
            transform.rotation = Quaternion.Slerp( transform.rotation, endRotation, Time.deltaTime * eventData.RotationSpeed);
            await Task.Yield();
        }

        await Task.Delay((int)eventData.DelayWhenDone * 1000);
    }



}
