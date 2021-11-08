using UnityEngine;

public abstract class EventData : ScriptableObject {

    [SerializeField] private float transitionsTime;
    [SerializeField] private float delayWhenDone;
   
    public float TransitionTime => transitionsTime;
    public float DelayWhenDone => delayWhenDone;

}
