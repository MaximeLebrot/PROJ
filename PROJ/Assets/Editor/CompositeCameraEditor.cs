using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(CompositeCameraBehaviour))]
public class CompositeCameraEditor : Editor {

    private SerializedProperty behaviourProperty;
    private SerializedProperty callbackTypesProperty;
    private SerializedProperty hasCallbackTypeProperty;
    private SerializedProperty transitionListProperty;
    
    private Type currentType;
    
    private GUIStyle wrongTypeStyle;
    private GUIStyle correctTypeStyle;

    private ReorderableList typeCallbackList;
    
    private void OnEnable() {
        
        behaviourProperty = serializedObject.FindProperty("cameraBehaviour");
        callbackTypesProperty = serializedObject.FindProperty("callbackTypeNames");
        transitionListProperty = serializedObject.FindProperty("transitionsToThisBehaviour");
        hasCallbackTypeProperty = serializedObject.FindProperty("hasCallbackType");

        wrongTypeStyle = new GUIStyle {
            normal = {
                textColor = Color.red
            },
            fontSize = 15
            
        };

        correctTypeStyle = new GUIStyle {
            normal = {
                textColor = Color.green
            },

            fontSize = 15
        };
        
        typeCallbackList = new ReorderableList(serializedObject, callbackTypesProperty) {
            drawElementCallback = DrawElement
        };
    }

    public override void OnInspectorGUI() {
        
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(behaviourProperty);

        EditorGUILayout.PropertyField(hasCallbackTypeProperty);

        if (hasCallbackTypeProperty.boolValue) {
            
            typeCallbackList.DoLayoutList();
        }
        
        EditorGUILayout.PropertyField(transitionListProperty);
        
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty element = typeCallbackList.serializedProperty.GetArrayElementAtIndex(index); 
        
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        
        DoesTypeExist(index);

        if(IsTypePresentInList(element.stringValue, index))
            EditorGUI.LabelField(new Rect(rect.x + 230, rect.y, 200, EditorGUIUtility.singleLineHeight),"Type already exists in list", wrongTypeStyle);
        else if(currentType != null)
            EditorGUI.LabelField(new Rect(rect.x + 230, rect.y, 200, EditorGUIUtility.singleLineHeight), currentType.Name, correctTypeStyle);
        else 
            EditorGUI.LabelField(new Rect(rect.x + 230, rect.y, 200, EditorGUIUtility.singleLineHeight),"Type does not exist", wrongTypeStyle);
        
    }
    
    private void DoesTypeExist(int index) {
        currentType = (target as CompositeCameraBehaviour).CreateCallbackType(index);
    }

    private bool IsTypePresentInList(string newType, int indexToSkip) {
        for (int i = 0; i < typeCallbackList.serializedProperty.arraySize; i++) {

            if (i == indexToSkip)
                continue;
            
            if (String.Equals(newType, typeCallbackList.serializedProperty.GetArrayElementAtIndex(i).stringValue)) 
                return true;
        }

        return false;
    }
    
}
