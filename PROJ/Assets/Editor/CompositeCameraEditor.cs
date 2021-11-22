using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(CompositeCameraBehaviour))]
public class CompositeCameraEditor : Editor {

    private static Dictionary<int, HashSet<string>> typeNames = new Dictionary<int, HashSet<string>>();
    
    private SerializedProperty behaviourProperty;
    private SerializedProperty callbackTypesProperty;
    private SerializedProperty hasCallbackTypeProperty;
    private SerializedProperty addCameraBehaviourTypeProperty;
    
    private Type currentType;
    
    private GUIStyle wrongTypeStyle;
    private GUIStyle correctTypeStyle;

    private ReorderableList typeCallbackList;

    
    private void OnEnable() {
        typeNames = new Dictionary<int, HashSet<string>>();
        
        if(typeNames.ContainsKey(target.GetInstanceID()) == false)
            typeNames.Add(target.GetInstanceID(), new HashSet<string>());
        
        behaviourProperty = serializedObject.FindProperty("cameraBehaviour");
        callbackTypesProperty = serializedObject.FindProperty("callbackTypeNames");
        hasCallbackTypeProperty = serializedObject.FindProperty("hasCallbackType");
        addCameraBehaviourTypeProperty = serializedObject.FindProperty("addCameraBehaviourTypeAsKey");
        
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
            drawElementCallback = DrawTypeElement,
            drawHeaderCallback = DrawHeader,
        };

    }
    
    public override void OnInspectorGUI() {
        
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(behaviourProperty);
        EditorGUILayout.PropertyField(hasCallbackTypeProperty);
        
        if (hasCallbackTypeProperty.boolValue) {
            EditorGUILayout.PropertyField(addCameraBehaviourTypeProperty);
            typeCallbackList.DoLayoutList();
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawTypeElement(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty element = typeCallbackList.serializedProperty.GetArrayElementAtIndex(index); 
        
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        
        CheckIfTypeExists(index);

        if(IsTypePresentInList(element.stringValue, index))
            EditorGUI.LabelField(new Rect(rect.x + 230, rect.y, 200, EditorGUIUtility.singleLineHeight),"Type already exists in list", wrongTypeStyle);
        else if (currentType != null) {
            if(KeyIsOccupied(target, element.stringValue))
                DrawLabelField(rect, "KEY OCCUPIED", wrongTypeStyle);
            else {
                DrawLabelField(rect, currentType.Name, correctTypeStyle);
                typeNames[target.GetInstanceID()].Add(element.stringValue);
            }
        }
        else 
            DrawLabelField(rect, "This type doesn't exist", wrongTypeStyle);
        
    }
    private void DrawLabelField(Rect rect, string text, GUIStyle style) {
        EditorGUI.LabelField(new Rect(rect.x + 230, rect.y, 200, EditorGUIUtility.singleLineHeight),text, style);
    }
    
    private void DrawHeader(Rect rect) => EditorGUI.LabelField(rect, "Callback Type Names");

    private void CheckIfTypeExists(int index) {
        currentType = (target as CompositeCameraBehaviour)?.CreateCallbackType(index);
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

    private static bool KeyIsOccupied(UnityEngine.Object target, string newKey) {
        foreach (int key in typeNames.Keys) {
            if (key == target.GetInstanceID())
                continue;
            
            if(typeNames[key].Contains(newKey))
                return true;
        }
        return false;
    }
    
}
