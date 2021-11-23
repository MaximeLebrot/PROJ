using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraTransition<T> where T : TransitionData {

    protected readonly T transitionData;

    protected CameraTransition(T transitionData) => this.transitionData = transitionData;

    public abstract Task RunTransition(Transform transform);

}
