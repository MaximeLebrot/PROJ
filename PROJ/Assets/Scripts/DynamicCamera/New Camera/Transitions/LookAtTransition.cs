using System.Threading.Tasks;
using UnityEngine;

public class LookAtTransition : CameraTransition {

    private readonly float time;
    private readonly float waitWhenDone;
    private readonly float rotationSpeed;

    public LookAtTransition(ref Transform transform, Vector3 endPosition, Quaternion endRotation, float movementSpeed, float waitWhenDone, float rotationSpeed) : base(ref transform, endPosition, endRotation) {
        time = movementSpeed;
        this.waitWhenDone = waitWhenDone;
        this.rotationSpeed = rotationSpeed;
    }
    
    public override async Task Transit() {
        
        while (IsTransitionDone() == false) {
            transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref referenceVelocity, time);
            transform.rotation = Quaternion.Lerp( transform.rotation, endRotation, Time.deltaTime * rotationSpeed);
            await Task.Yield();
        }
        
        await Task.Delay((int)waitWhenDone * 1000);
    }
    
    public override bool IsTransitionDone() {
        
        return Vector3.Distance(transform.position, endPosition) < .01f;
    }
}
