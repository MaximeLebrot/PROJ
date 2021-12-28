#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Text;
using FMODUnity;
using UnityEngine;
using UnityEditor;


public class DefaultSettingsEditor : EditorWindow {
    
    private static readonly Vector2 WindowSize = new Vector2(300, 600);

    private string jsonContent;
    
    [MenuItem("Tools/Default settings _&T")]
    public static void OpenWindow() {
        DefaultSettingsEditor window = GetWindow<DefaultSettingsEditor>();

        window.minSize = WindowSize;
        window.maxSize = WindowSize;
        
    }

    private void OnGUI() {
        if (GUILayout.Button("Save"))
            Save();
    }

    private void Save() {
        
        
    }
    
}

#endif //UNITY_EDITOR