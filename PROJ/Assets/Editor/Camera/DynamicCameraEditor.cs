using UnityEditor;
using UnityEngine;
using EGL = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(DynamicCamera.DynamicCamera))]
public class DynamicCameraEditor : Editor {
    
    private SerializedObject serializedCameraBehaviour;
    private SerializedObject serializedCameraData;
    private SerializedProperty cameraData;
    private SerializedProperty targetProp;
    private SerializedProperty offsetProp;
    private SerializedProperty movementSpeedProp;
    private SerializedProperty rotationSpeedProp;
    private SerializedProperty mouseSensitivityProp;

    private void OnEnable() => UpdateInspectorValues();

    public override void OnInspectorGUI() {
        
        serializedCameraBehaviour.Update();
        serializedCameraData.Update();

        if (cameraData.objectReferenceValue == null) {
            DrawDefaultInspector();
            return;
        }
        
        EditorGUI.BeginChangeCheck();

        EGL.PropertyField(cameraData);

        if (EditorGUI.EndChangeCheck())
            UpdateInspectorValues();

        GUILayout.Space(15);
        
        EGL.PropertyField(targetProp);
        EGL.PropertyField(offsetProp);
        EGL.PropertyField(movementSpeedProp);
        EGL.PropertyField(rotationSpeedProp);
        EGL.PropertyField(mouseSensitivityProp);
        
        serializedCameraBehaviour.ApplyModifiedProperties();
        serializedCameraData.ApplyModifiedProperties();
    }

    private void UpdateInspectorValues() {
        serializedCameraBehaviour = new SerializedObject(target);

        cameraData = serializedObject.FindProperty("cameraBehaviour");
        targetProp = serializedObject.FindProperty("target");

        if (cameraData.objectReferenceValue == null)
            return;
        
        SerializedObject cameraBehaviourObject = new SerializedObject(cameraData.objectReferenceValue); //Camera Data

        SerializedProperty cameraDataProperty = cameraBehaviourObject.FindProperty("cameraData");

        serializedCameraData = new SerializedObject(cameraDataProperty.objectReferenceValue);
        
        offsetProp = serializedCameraData.FindProperty("offset");
        movementSpeedProp = serializedCameraData.FindProperty("movementSpeed");
        rotationSpeedProp = serializedCameraData.FindProperty("rotationSpeed");
        mouseSensitivityProp = serializedCameraData.FindProperty("mouseSensitivity");
    }
}
