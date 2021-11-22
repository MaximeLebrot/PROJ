using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hazard))]
[CanEditMultipleObjects]
public class HazardEditor : Editor {
    
    private SerializedProperty columnsProp, rowProp, hazardMatrix;

    private Vector2 position;
    private Vector2 size;
    private bool drawDefaultInspector;
    
    private void OnEnable() {
        columnsProp = serializedObject.FindProperty("column");
        rowProp = serializedObject.FindProperty("row");
        hazardMatrix = serializedObject.FindProperty("customPattern");
        
        
        hazardMatrix.arraySize = columnsProp.intValue * rowProp.intValue;
        
        for (int i = 0; i < columnsProp.intValue * rowProp.intValue; i++) {

            Debug.Log(hazardMatrix.arraySize);

        }
        
        drawDefaultInspector = true;
    }
    
    public override void OnInspectorGUI() {
        drawDefaultInspector = EditorGUILayout.Toggle("Draw default inspector", drawDefaultInspector);

        if (drawDefaultInspector) {
            DrawDefaultInspector();
            return;
        }
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(columnsProp);
        EditorGUILayout.PropertyField(rowProp);
        
        position = EditorGUILayout.Vector2Field("Position", position);
        DrawMatrix();
        
        EditorGUILayout.Space(rowProp.intValue * columnsProp.intValue * 10);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawMatrix() {

        float windowWidth = CalculateInspectorWidth();
        
        Debug.Log(hazardMatrix.arraySize);
        
        for (int i = 0; i < columnsProp.intValue; i++) {
            
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < rowProp.intValue; j++) {
                
                Vector2 newPosition = new Vector2(windowWidth + (20 * i), position.y + (20 * j));
                
                
                
                //EditorGUI.PropertyField(new Rect(newPosition, size), hazardMatrix.GetArrayElementAtIndex(j + (i * rowProp.intValue)));
                
               // EditorGUI.Toggle(new Rect(newPosition, size), true);
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }

    private float CalculateInspectorWidth() {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        return GUILayoutUtility.GetLastRect().size.x * .5f;
    }
}
