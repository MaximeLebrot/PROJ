using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using EGL = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(WindPuzzle))]
public class WindPuzzleEditor : Editor
{
    WindPuzzle serObj;
    SerializedProperty puzzleObjects;
    string[] strings;
    int[] index;
    Object[] modifiers;
    ReorderableList listRE;

    private void OnEnable()
    {
        //Get the list of puzzleobjects
        puzzleObjects = serializedObject.FindProperty("puzzleObjects");
        listRE = new ReorderableList(serializedObject, puzzleObjects, true, true, true, true);


        listRE.drawElementCallback = DrawListItems;
        listRE.drawHeaderCallback = DrawHeader;


        /*
        modifiers = AssetDatabase.LoadAllAssetsAtPath("Assets/ScriptableObjects");
        serObj = target as WindPuzzle;
        index = new int[4];
        string[] newString = { "NONE", "Rotate", "Mirror", "Ignore" };
        strings = newString;

        Debug.Log(serObj.puzzleObjects.Count);
        */
    }

    // Draws the elements on the list
    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {

        SerializedProperty element = listRE.serializedProperty.GetArrayElementAtIndex(index); // The element in the list

        //Create a property field and label field for each property. 

        //The 'quantity' property
        //The label field for quantity (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Modifier");

        //The 'mobs' property. Since the enum is self-evident, I am not making a label field for it. 
        //The property field for mobs (width 100, height of a single line)
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("modVariant"),
            GUIContent.none);


       
    }


        //Draws the header
        void DrawHeader(Rect rect)
    {
        string name = "Puzzle Objects";
        EditorGUI.LabelField(rect, name);
    }


    //This is the function that makes the custom editor work
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        /*
        base.OnInspectorGUI();
        serializedObject.Update(); // Update the array property's representation in the inspector

        listRE.DoLayoutList(); // Have the ReorderableList do its work

        // We need to call this so that changes on the Inspector are saved by Unity.
        serializedObject.ApplyModifiedProperties();
        */

    }



}
