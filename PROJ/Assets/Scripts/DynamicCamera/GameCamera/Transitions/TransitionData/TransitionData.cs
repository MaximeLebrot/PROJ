using UnityEngine;

public abstract class TransitionData : ScriptableObject {

    [SerializeField] private float transitionsTime;
    [SerializeField] private float delayWhenDone;
   
    public float TransitionTime => transitionsTime;
    public float DelayWhenDone => delayWhenDone;

}
