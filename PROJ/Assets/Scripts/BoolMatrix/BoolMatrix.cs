using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoolMatrix : MonoBehaviour {
    [SerializeField] private int row, columns;

    [SerializeField] private List<bool> bools = new List<bool>();

}

[CustomEditor(typeof(BoolMatrix))]
public class BoolMatrixEditor : Editor {
    
    private SerializedProperty columnsProp, rowProp, boolsProp;

    private Vector2 position;
    private Vector2 size;
    private Vector2 offset;

    private bool[,] bools;
    
    private void OnEnable() {
        columnsProp = serializedObject.FindProperty("row");
        rowProp = serializedObject.FindProperty("columns");
        boolsProp = serializedObject.FindProperty("bools");
        
    }

    public override void OnInspectorGUI() {
        
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(columnsProp);
        EditorGUILayout.PropertyField(rowProp);

        position = EditorGUILayout.Vector2Field("Position", position);
        size = EditorGUILayout.Vector2Field("Size", size);
        offset = EditorGUILayout.Vector2Field("Offset", offset);

        for (int i = 0; i < columnsProp.intValue; i++) {
            
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < rowProp.intValue; j++) {
                Vector2 newPosition = new Vector2(position.x + (offset.x * i), position.y + (offset.y * j));
                
                EditorGUI.Toggle(new Rect(newPosition, size), true);
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space(rowProp.intValue * columnsProp.intValue * 10);

        serializedObject.ApplyModifiedProperties();
    }
}

