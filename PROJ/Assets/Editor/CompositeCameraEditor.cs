using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(CompositeCameraBehaviour))]
public class CompositeCameraEditor : Editor {
    
    private SerializedProperty behaviourProperty;
    private SerializedProperty callbackTypesProperty;
    private SerializedProperty hasCallbackTypeProperty;
    private SerializedProperty addCameraBehaviourTypeProperty;
    private SerializedProperty typeNameDictionaryProperty;

    private TypeNameDictionary typeNameDictionary;
    
    private Type currentType;
    
    private GUIStyle wrongTypeStyle;
    private GUIStyle correctTypeStyle;
    
    private ReorderableList typeCallbackList;
    
    private void OnEnable() {
        behaviourProperty = serializedObject.FindProperty("cameraBehaviour");
        callbackTypesProperty = serializedObject.FindProperty("callbackTypeNames");
        hasCallbackTypeProperty = serializedObject.FindProperty("hasCallbackType");
        addCameraBehaviourTypeProperty = serializedObject.FindProperty("addCameraBehaviourTypeAsKey");
        typeNameDictionaryProperty = serializedObject.FindProperty("typeNameDictionary");
        
        typeNameDictionary = typeNameDictionaryProperty.objectReferenceValue as TypeNameDictionary;
        
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

        EditorGUILayout.PropertyField(typeNameDictionaryProperty);
        
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawTypeElement(Rect rect, int index, bool isActive, bool isFocused) {
        SerializedProperty element = typeCallbackList.serializedProperty.GetArrayElementAtIndex(index); 
        
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        
        if (GUI.Button((new Rect(rect.x + 230, rect.y, 200, EditorGUIUtility.singleLineHeight)), "Check legit")) {
            
            currentType = GenerateType(index);
            
            Debug.Log(currentType);

            if (currentType != null)
                typeNameDictionary.AddType(target.GetInstanceID(), currentType.Name);

        }
        
        if(currentType == null)
            DrawLabelField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),  "Input is not a type", wrongTypeStyle);
        else
            DrawLabelField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),  currentType.Name, correctTypeStyle);
    }
    private void DrawLabelField(Rect rect, string text, GUIStyle style) {
        EditorGUI.LabelField(new Rect(rect.x + 450, rect.y, 200, EditorGUIUtility.singleLineHeight),text, style);
    }
    
    private void DrawHeader(Rect rect) => EditorGUI.LabelField(rect, "Callback Type Names");

    private Type GenerateType(int index) =>  (target as CompositeCameraBehaviour)?.CreateCallbackType(index);

    private bool IsTypePresentInList(string newType, int ID, bool excludeIDFromSearch) {

        HashSet<string> types = typeNameDictionary.GetTypeList(target.GetInstanceID());

        for (int i = 0; i < types.Count; i++) {
            if (String.Equals(newType, typeCallbackList.serializedProperty.GetArrayElementAtIndex(i).stringValue)) {
                Debug.Log("Type present in list");
                return true;
            }
                
        }

        return false;
    }


}
