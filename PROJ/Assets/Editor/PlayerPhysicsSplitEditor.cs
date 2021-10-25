using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EGL = UnityEditor.EditorGUILayout;


[CustomEditor(typeof(PlayerPhysicsSplit))]
public class PlayerPhysicsSplitEditor : Editor {
    
    private bool showTarget;

    private SerializedObject serializedObject;
    
    private SerializedProperty maxSpeedProp;
    private SerializedProperty skinWidthProp;
    private SerializedProperty inputThresholdProp;
    private SerializedProperty gravityProp;
    private SerializedProperty gravityWhenFallingProp;
    private SerializedProperty currentGravityProp;

    private List<SerializedProperty> variablesProperties = new List<SerializedProperty>();

    private bool showDefault;
    
    private void OnEnable() {
        serializedObject = new SerializedObject(target);

        maxSpeedProp = serializedObject.FindProperty("maxSpeed");
        variablesProperties.Add(maxSpeedProp);
        skinWidthProp = serializedObject.FindProperty("skinWidth");
        variablesProperties.Add(skinWidthProp);
        inputThresholdProp = serializedObject.FindProperty("inputThreshold");
        variablesProperties.Add(inputThresholdProp);
        gravityProp = serializedObject.FindProperty("gravity");
        variablesProperties.Add(gravityProp);
        gravityWhenFallingProp = serializedObject.FindProperty("gravityWhenFalling");
        variablesProperties.Add(gravityWhenFallingProp);
        currentGravityProp = serializedObject.FindProperty("currentGravity");
        variablesProperties.Add(currentGravityProp);
    }

    public override void OnInspectorGUI() {

        showDefault = EGL.Toggle("Draw default", showDefault);
        
        DrawDefaultInspector();
    }
}
