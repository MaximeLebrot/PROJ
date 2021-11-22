using System;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Transition Container")]
public class Transitioner : ScriptableObject {

    [SerializeField] private List<BaseCameraBehaviour> from;
    [SerializeField] private List<BehaviourPair> to;

    private Dictionary<Type, Dictionary<Type, List<TransitionData>>> transitionDictionary;

    public void Initialize() {
        transitionDictionary = new Dictionary<Type, Dictionary<Type, List<TransitionData>>>();

        for (int i = 0; i < from.Count; i++) {

            transitionDictionary.Add(from[i].GetType(), new Dictionary<Type, List<TransitionData>>());

            foreach (BehaviourPair pair in to) {
                
                foreach(BaseCameraBehaviour cameraBehaviour in pair.to)
                    transitionDictionary[from[i].GetType()].Add(cameraBehaviour.GetType(), pair.transitionData);
            }
        }
    }

    public List<TransitionData> GetTransitionData<T1, T2>() where T1 : BaseCameraBehaviour where T2 : BaseCameraBehaviour {
        return transitionDictionary[typeof(T1)][typeof(T2)];
    }
    
    
}

[Serializable]
public struct BehaviourPair {
    public List<BaseCameraBehaviour> to;
    public List<TransitionData> transitionData;
} 

