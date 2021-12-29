using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEditor;
using UnityEngine;

//Need to generate separate cs.file so they can be used as components. 

public class OptionGenerator : EditorWindow {
    
    private static readonly string Title = "Option Generator";
    private GUIStyle titleStyle;
    private GUIStyle optionStyle;

    private string[] typeContents;
    private string[] valueContents;
    
    private string inputText;

    private int typeSelected;
    private int valueSelected;
    
    private static readonly string TopFolder = "Assets";
    private static readonly string ScriptFolderName = "UI Component Types";
    private static readonly string jsonPath = $"{TopFolder}/{ScriptFolderName}";
    private static readonly string combinedPath = $"{TopFolder}/{ScriptFolderName}";

    
    private List<Type> subClasses = new List<Type>();

    private Assembly assembly;
    
    [MenuItem("Tools/Option Generator _%T")]
    public static void Open() => GetWindow<OptionGenerator>();

    private void OnEnable() {
        
        //FileInfo[] files = new DirectoryInfo(combinedPath).GetFiles();

        
       // foreach(FileInfo file in files)
        //    Debug.Log(file);

        assembly = typeof(UIMenuItem).Assembly;
        
        
        IEnumerable<Type> subclassTypes = typeof(UIMenuItem).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(UIMenuItem)) && type.IsGenericType == false && type.IsAbstract == false);
        
        subClasses.AddRange(subclassTypes);

        titleStyle = new GUIStyle() {normal = {textColor = Color.white}, fontSize = 18, alignment = TextAnchor.UpperCenter};

        IEnumerable<Type> mainComponents = typeof(UIMenuItem).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(UIMenuItem)) && type.IsAbstract);
        
        typeContents = new string[mainComponents.Count()];
        
        int counter = 0;
        
        foreach (Type type in mainComponents) 
            typeContents[counter++] = type.ToString().Replace("`1[T]", ""); //Generics why u make me do this
        
        valueContents = new[] {"int", "bool", "float", "string", "Resolution"};
    }

    private void OnGUI() {
        
        EditorGUILayout.LabelField(Title, titleStyle);

        EditorGUILayout.Space(10);

        EditorGUIUtility.labelWidth = 80; 
        
        inputText = EditorGUILayout.TextField("New Option", inputText);
        
        typeSelected = EditorGUILayout.Popup("Type", typeSelected, typeContents);
        valueSelected = EditorGUILayout.Popup("Value", valueSelected, valueContents);
        
        if (inputText is null || inputText.Length < 1)
            GUI.enabled = false;


        if (GUILayout.Button("Generate option"))
            GenerateOption(new ClassDefinition(inputText, typeContents[typeSelected], valueContents[valueSelected]));
        
        GUI.enabled = true;

    }

    private void GenerateOption(ClassDefinition classDefinition) {
        
        if (TypeExist(classDefinition.className)) {
            Debug.LogError($"Error: {classDefinition.className} already exist");
            return;
        }
        
        GenerateCSharpFile(classDefinition);
        
        inputText = String.Empty;
        GUIUtility.keyboardControl = 0; //Used to clear the input box, tf Unity.
        AssetDatabase.Refresh();
        
    }
    
    private void GenerateCSharpFile(ClassDefinition classDefinition) {

        if (AssetDatabase.IsValidFolder(combinedPath) == false)
            AssetDatabase.CreateFolder(TopFolder, ScriptFolderName);

        string content = GenerateFileContent(classDefinition);

        FileHandler.CreateFile(classDefinition.className, combinedPath, "cs", content);
    }

    private bool TypeExist(string className) => Type.GetType($"{className}, {assembly}") != null;


    private string GenerateFileContent(ClassDefinition classDefinition) {
        StringBuilder fileContent = new StringBuilder();

        fileContent.AppendLine("//======AUTO GENERATED CODE=========//");

        fileContent.AppendLine($"public class {classDefinition.className} : {classDefinition.baseClass}<{classDefinition.baseClassGenericValue}> {{}}");
        
        return fileContent.ToString();
    }
    

    private struct ClassDefinition {
        public string className;
        public string baseClass;
        public string baseClassGenericValue;

        public ClassDefinition(string className, string baseClass, string baseClassGenericValue) {
            this.className = className;
            this.baseClass = baseClass;
            this.baseClassGenericValue = baseClassGenericValue;
        }
        
    }
}
////DÖD KOD
/*
private void GenerateOptionType(string className, string type, string value) {
        
string path = string.Concat(TopFolder, Path.DirectorySeparatorChar, ScriptFolderName);
string filePath = string.Concat(TopFolder, Path.DirectorySeparatorChar, ScriptFolderName, Path.DirectorySeparatorChar, "OptionTypes.cs");
        
    if (AssetDatabase.IsValidFolder(path) == false)
AssetDatabase.CreateFolder(TopFolder, ScriptFolderName);
        
if(File.Exists(filePath))
File.Delete(filePath);
        
CustomType customType = new CustomType(className, type, value);
// types.Add(customType);
        
FileHandler.Write(filePath,  GenerateFileContent());
        
FileHandler.Save(types.ToArray(), jsonPath);
        
AssetDatabase.Refresh();
}
*/