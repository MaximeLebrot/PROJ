using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TypeNameDictionary))]
public class TypeNameDictionaryEditor : Editor {

    public override void OnInspectorGUI() {
    
        DrawDefaultInspector();
        
        if (GUILayout.Button("Debug what I contain", GUILayout.Width(200))) {
                
            TypeNameDictionary typeNameDictionary = target as TypeNameDictionary;

            HashSet<string> allNames = typeNameDictionary.GetAllCombinedTypeLists(false, -1);

            if (allNames == null || allNames.Count < 1) {
                Debug.Log("Empty");
                return;
            }
                
            
            foreach(string name in allNames)
                Debug.Log(name);

        }
        
    }

}