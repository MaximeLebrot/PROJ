using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class OptionsEditor : EditorWindow {
    
    private static readonly string Title = "Option Generator";
    private GUIStyle titleStyle;
    private GUIStyle optionStyle;

    private static readonly Vector2 WindowSize = new Vector2(500, 600);
    
    private string[] typeContents;
    private string[] valueContents;
    
    private string inputText;

    private int typeSelected;
    private int valueSelected;
    
    private static readonly string TopFolder = "Assets";
    private static readonly string ScriptFolderName = "UI Component Types";
    private static readonly string combinedPath = $"{TopFolder}/{ScriptFolderName}";
    
    private List<Type> subClasses = new List<Type>();

    private Assembly assembly;

    private const float StartingElementHeight = 40f;
    private const float ElementOffset = 20f;

    private Rect leftPane;
    private Rect rightPane;

    [MenuItem("Tools/Option Generator _%T")]
    public static void Open() {
        var window = GetWindow<OptionsEditor>();

        window.minSize = window.maxSize = WindowSize;

    }
    
    private void OnEnable() {

        leftPane = new Rect(new Vector2(0, StartingElementHeight), new Vector2(WindowSize.x * .7f, WindowSize.y));
        rightPane = new Rect(new Vector2(WindowSize.x / 2, StartingElementHeight), new Vector2(WindowSize.x * .3f, WindowSize.y));
        
        
        assembly = typeof(UIMenuItem).Assembly;
        
        IEnumerable<Type> subclassTypes = typeof(UIMenuItem).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(UIMenuItem)) && type.IsGenericType == false && type.IsAbstract == false);
        
        subClasses.AddRange(subclassTypes);

        titleStyle = new GUIStyle() {normal = {textColor = Color.white}, fontSize = 27, alignment = TextAnchor.UpperCenter};

        IEnumerable<Type> mainComponents = typeof(UIMenuItem).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(UIMenuItem)) && type.IsAbstract);
        
        typeContents = new string[mainComponents.Count()];
        
        int counter = 0;
        
        foreach (Type type in mainComponents) 
            typeContents[counter++] = type.ToString().Replace("`1[T]", ""); //Generics why u make me do this
        
        valueContents = new[] {"int", "bool", "float", "string", "Resolution"};
        
        EditorGUIUtility.labelWidth = 80; 
    }

    private void OnGUI() {
        
        EditorGUILayout.LabelField(Title, titleStyle);
        
        DrawLeftPane();

        EditorGUI.DrawRect(leftPane, Color.yellow);
        EditorGUI.DrawRect(rightPane, Color.black);
        
        GUI.BeginScrollView(rightPane, Vector2.zero, rightPane);
        
        GUI.EndScrollView();
    }

    private void DrawLeftPane() {
        
        EditorGUILayout.Space(10);
        
        int elementCounter = 0;
        
        inputText = EditorGUI.TextField(new Rect(leftPane.position + Vector2.up * CalculateOffset(elementCounter++), new Vector2(300, 20)), "New Option", inputText);
        typeSelected = EditorGUI.Popup(new Rect(leftPane.position + Vector2.up * CalculateOffset(elementCounter++),new Vector2(300, 20)), "Type", typeSelected, typeContents);
        valueSelected = EditorGUI.Popup(new Rect(leftPane.position + Vector2.up * CalculateOffset(elementCounter++),new Vector2(300, 20)), "Value", valueSelected, valueContents);

        if (inputText is null || inputText.Length < 1)
            GUI.enabled = false;
        else if (char.IsUpper(inputText[0]) == false)
            inputText = inputText.First().ToString().ToUpper() + inputText.Substring(1);


        
        if (GUI.Button(new Rect(leftPane.position + Vector2.up * CalculateOffset(elementCounter++),new Vector2(300, 20)),"Generate option")) 
            GenerateOption(new ClassDefinition(inputText, typeContents[typeSelected], valueContents[valueSelected]));

        GUI.enabled = true;
    }

    private float CalculateOffset(int index) {
        return StartingElementHeight + ElementOffset * index;
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

        fileContent.AppendLine("//=======AUTO GENERATED CODE=========//");
        fileContent.AppendLine("//=======Tool Author: Jonathan Haag=========//");

        fileContent.AppendLine($"public class {classDefinition.className} : {classDefinition.baseClass}<{classDefinition.baseClassGenericValue}> {{}}");
    
        Regex x = new Regex("a-z");
        
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
