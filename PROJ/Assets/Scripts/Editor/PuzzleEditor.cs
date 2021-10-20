using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using EGL = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(Puzzle))]
public class PuzzleEditor : Editor
{
    Puzzle serObj;
    SerializedProperty puzzleObjects;
    SerializedProperty solution;
    string[] strings;
    int[] index;
    Object[] modifiers;
    ReorderableList listRE;

    private void OnEnable()
    {
        //Get the list of puzzleobjects
        puzzleObjects = serializedObject.FindProperty("puzzleObjects");
        solution = serializedObject.FindProperty("solution");

        listRE = new ReorderableList(serializedObject, puzzleObjects, true, true, true, true);


        if (listRE.count > 0)
        {
            listRE.drawElementCallback = DrawListItems;
            listRE.drawHeaderCallback = DrawHeader;
        }


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
        SerializedObject obj = new SerializedObject(element.objectReferenceValue);

        //The property field for level. Since we do not need so much space in an int, width is set to 20, height of a single line.
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, 175, EditorGUIUtility.singleLineHeight),
            element,
            GUIContent.none
        );


        //The label field for Modifier (width 100, height of a single line)
        EditorGUI.LabelField(new Rect(rect.x + 180, rect.y, 100, EditorGUIUtility.singleLineHeight), "Modifier: ");

        //The 'mobs' property. Since the enum is self-evident, I am not making a label field for it. 
        //The property field for mobs (width 100, height of a single line)
        EditorGUI.PropertyField(
            new Rect(rect.x + 240, rect.y, 100, EditorGUIUtility.singleLineHeight),
            obj.FindProperty("modVariant"),
            GUIContent.none);



        obj.ApplyModifiedProperties();
       
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

        
        if(listRE.count < 1)
        {
            base.DrawDefaultInspector();
        }
        else
        {
            serializedObject.Update(); // Update the array property's representation in the inspector

            listRE.DoLayoutList(); // Have the ReorderableList do its work

            EGL.PropertyField(solution);

            // We need to call this so that changes on the Inspector are saved by Unity.
            serializedObject.ApplyModifiedProperties();
        }
        
        
 
        
    }



}
