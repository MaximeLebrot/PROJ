using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraTransition<T> where T : TransitionData {

    protected readonly T transitionData;
    protected readonly Transform endTarget;
    
    protected CameraTransition(T transitionData, Transform endTarget) {
        this.transitionData = transitionData;
        this.endTarget = endTarget;
    }

    public async Task RunTransition(Transform transform) {
        
        float endTime = Time.time + transitionData.TransitionTime;
        float animationCurveTime = 0;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        
        while (endTime > Time.time) {
            
            Transition(transform, startPosition, startRotation, animationCurveTime);

            Debug.Log(animationCurveTime);
            
            animationCurveTime += Time.deltaTime / transitionData.TransitionTime;
            
            await Task.Yield();
        }
        
        await Task.Delay((int)transitionData.DelayWhenDone * 1000);
        
    }
    
    protected abstract void Transition(Transform transform, Vector3 start, Quaternion startRotation, float animationCurveValue);

}
