using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviour Callbacks")]
public class BehaviourCallback : ScriptableObject {

    [SerializeField] private List<CallbackPair> pairs = new List<CallbackPair>();

    private Dictionary<Type, CameraBehaviour> callbacks;
    
    private void OnEnable() {
        callbacks = new Dictionary<Type, CameraBehaviour>();

        foreach (CallbackPair pair in pairs)
            callbacks[pair.playerState.GetType()] = pair.cameraBehaviour;
    }
    public CameraBehaviour GetCameraBehaviourCallback(PlayerState state) => callbacks.ContainsKey(state.GetType()) ? callbacks[state.GetType()] : null;

    [Serializable]
    private struct CallbackPair {
        public PlayerState playerState;
        public CameraBehaviour cameraBehaviour;
    }
    
}

