using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hazard))]
public class HazardEditor : Editor {
    
    private SerializedProperty hazardMatrix;
    private SerializedProperty hazardObjectProp;
    private SerializedProperty gridObjectTypeProp;
    private SerializedProperty moveDirectionProp;
    private SerializedProperty hazardObjectsProp;
    private SerializedProperty movingHazardProp;

    
    private bool drawDefaultInspector;

    private int gridSize;
    
    private void OnEnable() {
        hazardMatrix = serializedObject.FindProperty("customPattern");
        hazardObjectProp = serializedObject.FindProperty("hazardObj");
        gridObjectTypeProp = serializedObject.FindProperty("grid");
        moveDirectionProp = serializedObject.FindProperty("moveDirection");
        hazardObjectsProp = serializedObject.FindProperty("hazardObjects");
        movingHazardProp = serializedObject.FindProperty("movingHazard");

        
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



        EditorGUILayout.PropertyField(movingHazardProp);
        EditorGUILayout.PropertyField(moveDirectionProp);
        
        DrawMatrix();
        EditorGUILayout.Space(400);
        
        EditorGUILayout.PropertyField(hazardObjectsProp);
        serializedObject.ApplyModifiedProperties();
        
        
    }

    private void DrawMatrix()
    {

        float windowCenter = CalculateInspectorWidth();

        float maxHeight = 220 + 20 * gridSize;

        EditorGUI.LabelField(new Rect(new Vector2(windowCenter + gridSize, 190), new Vector2(100, 20)), "Hazard Matrix");

        for (int i = 0; i < gridSize; i++)
        {

            for (int j = 0; j < gridSize; j++)
            {

                SerializedProperty prop = hazardMatrix.GetArrayElementAtIndex(j + i * gridSize);

                Vector2 newPosition = new Vector2(windowCenter + 20 * j, maxHeight - 20 * i);

                EditorGUI.PropertyField(new Rect(newPosition, new Vector2(30, 30)), prop, GUIContent.none);
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
