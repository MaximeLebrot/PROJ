using System;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Transition Container")]
public class Transitioner : ScriptableObject {

    [SerializeField] private List<BaseCameraBehaviour> from;
    [SerializeField] private List<BehaviourPair> to;

    private Dictionary<BaseCameraBehaviour, Dictionary<BaseCameraBehaviour, List<TransitionData>>> transitionDictionary;

    public void Initialize() {
        transitionDictionary = new Dictionary<BaseCameraBehaviour, Dictionary<BaseCameraBehaviour, List<TransitionData>>>();

        for (int i = 0; i < from.Count; i++) {

            transitionDictionary.Add(from[i], new Dictionary<BaseCameraBehaviour, List<TransitionData>>());

            foreach (BehaviourPair pair in to) {
                
                foreach(BaseCameraBehaviour cameraBehaviour in pair.to)
                    transitionDictionary[from[i]].Add(cameraBehaviour, pair.transitionData);
            }
        }
    }

    public List<TransitionData> GetTransitionData<T>() where T : BaseCameraBehaviour {


    //    transitionDictionary[typeof(BaseCameraBehaviour)].Keys;
      

        return null;

    }
    
    
}

[Serializable]
public struct BehaviourPair {
    public List<BaseCameraBehaviour> to;
    public List<TransitionData> transitionData;
} 

