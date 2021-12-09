using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputController : StateMachineBehaviour {
    public delegate void OnSuspendInput(bool suspend);

    [SerializeField] private bool onStateEnter;
    [SerializeField] private bool onStateExit;
    
    [SerializeField] private bool suspendInputOnEnter;
    [SerializeField] private bool suspendInputOnExit;

    public static event OnSuspendInput SuspendInputEvent;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(onStateEnter)
            SuspendInputEvent?.Invoke(suspendInputOnEnter);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(onStateExit)
            SuspendInputEvent?.Invoke(suspendInputOnExit);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(InputController))]
public class InputControllerEditor : Editor {

    private SerializedProperty onStateEnterProperty;
    private SerializedProperty onStateExitProperty;
    private SerializedProperty suspendInputOnEnterProperty;
    private SerializedProperty suspendInputOnExitProperty;
    
    
    private void OnEnable() {

        onStateEnterProperty = serializedObject.FindProperty("onStateEnter");
        onStateExitProperty = serializedObject.FindProperty("onStateExit");
        suspendInputOnEnterProperty = serializedObject.FindProperty("suspendInputOnEnter");
        suspendInputOnExitProperty = serializedObject.FindProperty("suspendInputOnExit");
    }
    public override void OnInspectorGUI() {

        serializedObject.Update();
        
        EditorGUILayout.PropertyField(onStateEnterProperty);

        if (onStateEnterProperty.boolValue) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(suspendInputOnEnterProperty);
            EditorGUI.indentLevel--;
        }
        
        EditorGUILayout.PropertyField(onStateExitProperty);

        if (onStateExitProperty.boolValue) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(suspendInputOnExitProperty);
            EditorGUI.indentLevel--;
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}


#endif