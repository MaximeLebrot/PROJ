using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;



public class OptionsEditor : EditorWindow  {
    
    private static readonly string Title = "Option Generator";
    private GUIStyle titleStyle;
    private GUIStyle optionStyle;

    private static readonly Vector2 WindowSize = new Vector2(600, 600);
    
    private string[] typeContents;
    private string[] valueContents;
    
    private string inputText;

    private int typeSelected;
    private int valueSelected;

    private static readonly string TopFolder = "Assets";
    private static readonly string ScriptFolderName = "UI Component Types";
    private static readonly string CombinedPath = $"{TopFolder}/{ScriptFolderName}";
    
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

        leftPane = new Rect(new Vector2(0, StartingElementHeight), new Vector2(WindowSize.x * .45f, 100));
        rightPane = new Rect(new Vector2(WindowSize.x / 2 - 20, StartingElementHeight), new Vector2(WindowSize.x * .5f, WindowSize.y * .9f));
        
        assembly = typeof(UIMenuItemBase).Assembly;
        
        IEnumerable<Type> subclassTypes = typeof(UIMenuItemBase).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(UIMenuItemBase)) && type.IsGenericType == false && type.IsAbstract == false);
        
        subClasses.AddRange(subclassTypes);
        
        titleStyle = new GUIStyle {normal = {textColor = Color.white}, fontSize = 27, alignment = TextAnchor.UpperCenter};

        IEnumerable<Type> mainComponents = typeof(UIMenuItemBase).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(UIMenuItemBase)) && type.IsAbstract);
        
        typeContents = new string[mainComponents.Count()];
        
        int counter = 0;

        foreach (Type type in mainComponents)
            typeContents[counter++] = TrimClassName(type.Name); //Generics why u make me do this
        
        valueContents = new[] {"int", "bool", "float", "string", "Resolution"};
        
        EditorGUIUtility.labelWidth = 80; 
    }

    private string TrimClassName(string text) => text.Replace("`1", "");

    private void OnGUI() {
        
        EditorGUILayout.LabelField(Title, titleStyle);

        BeginArea(leftPane, DrawLeftPane);
        BeginArea(rightPane, DrawRightPane);
    }

    private void DrawRightPane() {
        foreach(Type type in subClasses)
            EditorGUILayout.LabelField($"{type.Name} ({TrimClassName(type.BaseType.Name)})");
    }

    private void DrawLeftPane() {
        
        int elementCounter = 0;
        
        inputText = EditorGUILayout.TextField("New Option", inputText);
        typeSelected = EditorGUILayout.Popup("Type", typeSelected, typeContents);
        //valueSelected = EditorGUILayout.Popup("Value", valueSelected, valueContents);

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
    
    private void BeginArea(Rect rect, Action callback) {
        GUILayout.BeginArea(rect, GUI.skin.box);
        callback.Invoke();
        GUILayout.EndArea();
    }
    
    private void GenerateCSharpFile(ClassDefinition classDefinition) {

        if (AssetDatabase.IsValidFolder(CombinedPath) == false)
            AssetDatabase.CreateFolder(TopFolder, ScriptFolderName);

        string content = GenerateFileContent(classDefinition);

        FileHandler.CreateFile(classDefinition.className, CombinedPath, "cs", content);
    }

    private bool TypeExist(string className) => Type.GetType($"{className}, {assembly}") != null;
    
    private string GenerateFileContent(ClassDefinition classDefinition) {
        StringBuilder fileContent = new StringBuilder();

        fileContent.AppendLine("//=======AUTO GENERATED CODE=========//");
        fileContent.AppendLine("//=======Tool Author: Jonathan Haag=========//");

        //fileContent.AppendLine($"public class {classDefinition.className} : {classDefinition.baseClass}<{classDefinition.baseClassGenericValue}> {{}}"); // Old line when subclasses were generic
        fileContent.AppendLine($"public class {classDefinition.className} : {classDefinition.baseClass} {{}}");
        
        return fileContent.ToString();
    }
    
    private readonly struct ClassDefinition {
        public readonly string className;
        public readonly string baseClass;
        public readonly string baseClassGenericValue;

        public ClassDefinition(string className, string baseClass, string baseClassGenericValue) {
            this.className = className;
            this.baseClass = baseClass;
            this.baseClassGenericValue = baseClassGenericValue;
        }
        
    }
    
}
