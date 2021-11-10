using UnityEngine;

public abstract class EventDataCameraTransition<T> : CameraTransition {
    
    protected readonly T eventData;

    protected EventDataCameraTransition(ref Transform transform, T eventData) : base(ref transform) {
        this.eventData = eventData;
    }
}