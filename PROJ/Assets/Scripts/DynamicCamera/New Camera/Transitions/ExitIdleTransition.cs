
using System.Threading.Tasks;
using UnityEngine;

public class ExitIdleTransition : CameraTransition {
    
    public ExitIdleTransition(ref Transform transform, Vector3 endPosition, Quaternion endRotation ) : base(ref transform, endPosition, endRotation ) {}
    
    public override async Task Transit() {

        Vector3 ha = Vector3.zero;
        
        transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref ha, 2);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime);
        
        await Task.Delay(3000);
    }

    public override bool IsTransitionDone() {
        throw new System.NotImplementedException();
    }
}
