using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCamera {

    [CreateAssetMenu(menuName = "Camera/Camera Behaviour Callbacks")]
    public class BehaviourCallback : ScriptableObject {

        [SerializeField] private List<CallbackPair> pairs = new List<CallbackPair>();

        private Dictionary<Type, BaseCameraBehaviour> callbacks;

        private void OnEnable() {
            callbacks = new Dictionary<Type, BaseCameraBehaviour>();

            foreach (CallbackPair pair in pairs)
                callbacks[pair.playerState.GetType()] = pair.baseCameraBehaviour;
        }

        public BaseCameraBehaviour GetCameraBehaviourCallback(PlayerState state) => callbacks.ContainsKey(state.GetType()) ? callbacks[state.GetType()] : null;

        [Serializable]
        private struct CallbackPair {
            public PlayerState playerState;
            [FormerlySerializedAs("cameraBehaviour")] public BaseCameraBehaviour baseCameraBehaviour;
        }

    }

}