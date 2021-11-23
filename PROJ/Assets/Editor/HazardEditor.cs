using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hazard))]
public class HazardEditor : Editor {
    
    private SerializedProperty hazardMatrix;
    private SerializedProperty hazardObjectProp;
    private SerializedProperty gridObjectTypeProp;
    private SerializedProperty baseTimerProp;
    private SerializedProperty timerOffsetPerObjectProp;
    private SerializedProperty startingStateProp;
    private SerializedProperty stateOffsetPerObjectProp;
    private SerializedProperty hazardObjectsProp;
    private SerializedProperty movingHazardProp;
    private SerializedProperty moveXProp;
    
    private bool drawDefaultInspector;

    private int gridSize;
    
    private void OnEnable() {
        hazardMatrix = serializedObject.FindProperty("customPattern");
        hazardObjectProp = serializedObject.FindProperty("hazardObj");
        gridObjectTypeProp = serializedObject.FindProperty("grid");
        baseTimerProp = serializedObject.FindProperty("baseTimer");
        timerOffsetPerObjectProp = serializedObject.FindProperty("timerOffsetPerObject");
        startingStateProp = serializedObject.FindProperty("startingState");
        stateOffsetPerObjectProp = serializedObject.FindProperty("stateOffsetPerObject");
        hazardObjectsProp = serializedObject.FindProperty("hazardObjects");
        movingHazardProp = serializedObject.FindProperty("movingHazard");
        moveXProp = serializedObject.FindProperty("moveX");
        
        CalculateGridSize();
        
        serializedObject.ApplyModifiedProperties();
        
        drawDefaultInspector = true;
    }
    
    public override void OnInspectorGUI() {
        serializedObject.Update();
        
        drawDefaultInspector = EditorGUILayout.Toggle("Draw default inspector", drawDefaultInspector);
        
        if (drawDefaultInspector) {
            DrawDefaultInspector();
            return;
        }
        
        EditorGUILayout.PropertyField(hazardObjectProp);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(gridObjectTypeProp);

        if (EditorGUI.EndChangeCheck()) {
            hazardMatrix.ClearArray();
            CalculateGridSize();
        }
        
        EditorGUILayout.PropertyField(baseTimerProp);
        EditorGUILayout.PropertyField(timerOffsetPerObjectProp);
        EditorGUILayout.PropertyField(startingStateProp);
        EditorGUILayout.PropertyField(stateOffsetPerObjectProp);
        EditorGUILayout.PropertyField(movingHazardProp);
        EditorGUILayout.PropertyField(moveXProp);
        
        DrawMatrix();
        EditorGUILayout.Space(200);
        
        EditorGUILayout.PropertyField(hazardObjectsProp);
        serializedObject.ApplyModifiedProperties();
        
        
    }

    private void DrawMatrix() {
        
        float windowCenter = CalculateInspectorWidth();

        EditorGUI.LabelField(new Rect(new Vector2(windowCenter + gridSize ,190), new Vector2(100, 20)), "Hazard Matrix");
        
        for (int i = 0; i < gridSize; i++) {
            
            for (int j = 0; j < gridSize; j++) {

                SerializedProperty prop = hazardMatrix.GetArrayElementAtIndex(j + i * gridSize);
                
                Vector2 newPosition = new Vector2(windowCenter + (20 * j),  220 + (20 * i));
                
                EditorGUI.PropertyField(new Rect(newPosition, new Vector2(20, 20)), prop, GUIContent.none);
                
            }
            
        }
    }

    private float CalculateInspectorWidth() {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        return GUILayoutUtility.GetLastRect().size.x * .5f;
    }
    
    private void CalculateGridSize() {
        gridSize = new SerializedObject(gridObjectTypeProp.objectReferenceValue).FindProperty("size").intValue;
        hazardMatrix.arraySize = gridSize * gridSize;
    }
    
    
}
