using System;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Composite Camera Behaviour", fileName = "New Composite Camera Behaviour")]
public class CompositeCameraBehaviour : ScriptableObject {

    [SerializeField] public BaseCameraBehaviour cameraBehaviour;
    [SerializeField] private bool hasCallbackType;
    [SerializeField] private List<string> callbackTypeNames;
    [SerializeField] private List<CameraTransition> transitionsToThisBehaviour;

    /// <summary>
    /// Will break if type is in a namespace
    /// </summary>
    /// <returns></returns>
    public Type CreateCallbackType(int index) => Type.GetType(callbackTypeNames[index]);

    /// <summary>
    /// Returns the type that will act as a key in the behaviour dictionary, if hasCallbackType is false this will return the type of the cameraBehaviour.
    /// </summary>
    /// <returns></returns>
    public Type GetCallbackType(int index) {
        return hasCallbackType ? CreateCallbackType(index) : cameraBehaviour.GetType();
    }
    
    public Type[] GetCallbackTypes() {
        Type[] types = new Type[callbackTypeNames.Count];

        for (int i = 0; i < callbackTypeNames.Count; i++) {
            types[i] = GetCallbackType(i);
        }

        return types;
    }

    public bool HasMultipleCallbackTypes() {
        return callbackTypeNames.Count > 1;
    }
    
}


