using System;
using UnityEditor;


[CustomEditor(typeof(DynamicCamera.DynamicCamera))]
public class DynamicCameraEditor : Editor {

   /* private SerializedObject serializedObject;
    private SerializedProperty targetProp;
    private SerializedProperty radiusProp;
    private SerializedProperty heightProp;
    private Transform targetTransform;
    
    private void OnEnable() {
        
        serializedObject = new SerializedObject(target);
        targetProp = serializedObject.FindProperty("followTarget");

        targetTransform = targetProp.objectReferenceValue as Transform;
    }

    public void OnSceneGUI() {
        if (targetProp.objectReferenceValue == null)
            return;

        serializedObject.Update();
        Handles.DrawWireDisc(targetTransform.position + targetTransform.up * heightProp.floatValue, Vector3.up, radiusProp.floatValue);

        serializedObject.ApplyModifiedProperties();
    }
    */
    
}
