using System;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Composite Camera Behaviour", fileName = "New Composite Camera Behaviour")]
public class CompositeCameraBehaviour : ScriptableObject {

    [SerializeField] public BaseCameraBehaviour cameraBehaviour;
    
    [Tooltip("if hasCallbackType is true, any hashmap will use the specified type as key. If false, the camera behaviour type will be used.")]
    [SerializeField] private bool hasCallbackType;
    [Tooltip("Set as false if the camera behaviour should not be set as a key and value in the same element ")]
    [SerializeField] private bool addCameraBehaviourTypeAsKey;
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
    
    public Type[] GetTypeList() {

        List<Type> allTypes = new List<Type>();

        
        if (hasCallbackType == false) {
            //Does not have callback, return only camera behaviour type.
            allTypes.Add(cameraBehaviour.GetType());
            return allTypes.ToArray();
        }
        
        for (int i = 0; i < callbackTypeNames.Count; i++)
            allTypes.Add(CreateCallbackType(i));

        if (addCameraBehaviourTypeAsKey)
            allTypes.Add(cameraBehaviour.GetType());

        return allTypes.ToArray();
    }

    public bool HasMultipleCallbackTypes() {
        return callbackTypeNames.Count > 1;
    }
}


