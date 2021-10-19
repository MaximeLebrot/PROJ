using UnityEditor;
using UnityEngine;
using EGL = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(DynamicCamera.DynamicCamera))]
public class DynamicCameraEditor : Editor {
    
    private SerializedObject serializedCameraBehaviour;
    private SerializedObject serializedCameraData;
    
    private SerializedProperty collisionLayerMaskProp;
    private SerializedProperty cameraData;
    private SerializedProperty targetProp;
    private SerializedProperty offsetProp;
    private SerializedProperty movementSpeedProp;
    private SerializedProperty rotationSpeedProp;
    private SerializedProperty mouseSensitivityProp;

    private void OnEnable() => UpdateInspectorValues();

    public override void OnInspectorGUI() {
        
        serializedCameraBehaviour.Update();
        bool success = UpdateInspectorValues();


        if (success == false) {
            DrawDefaultInspector();
            return;
        }

        serializedCameraData.Update();
        EGL.PropertyField(cameraData);


        GUILayout.Space(15);
        
        EGL.PropertyField(targetProp);
        EGL.PropertyField(collisionLayerMaskProp);
        
        GUILayout.Space(15);
        
        EGL.PropertyField(offsetProp);
        EGL.PropertyField(movementSpeedProp);
        EGL.PropertyField(rotationSpeedProp);
        EGL.PropertyField(mouseSensitivityProp);
        
        
        serializedCameraData.ApplyModifiedProperties();
        serializedCameraBehaviour.ApplyModifiedProperties();
    }

    private bool UpdateInspectorValues() {
        serializedCameraBehaviour = new SerializedObject(target);

        cameraData = serializedCameraBehaviour.FindProperty("cameraBehaviour");
        targetProp = serializedCameraBehaviour.FindProperty("target");
        collisionLayerMaskProp = serializedCameraBehaviour.FindProperty("layerMask");

        if (cameraData.objectReferenceValue == null)
            return false;
        
        SerializedObject cameraBehaviourObject = new SerializedObject(cameraData.objectReferenceValue); //Camera Data
        
        SerializedProperty cameraDataProperty = cameraBehaviourObject.FindProperty("cameraData");

        serializedCameraData = new SerializedObject(cameraDataProperty.objectReferenceValue);
        
        offsetProp = serializedCameraData.FindProperty("offset");
        movementSpeedProp = serializedCameraData.FindProperty("movementSpeed");
        rotationSpeedProp = serializedCameraData.FindProperty("rotationSpeed");
        mouseSensitivityProp = serializedCameraData.FindProperty("mouseSensitivity");

        serializedCameraBehaviour.ApplyModifiedProperties();

        return true;
    }
}
