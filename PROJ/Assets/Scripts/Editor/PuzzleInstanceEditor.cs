using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using EGL = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(PuzzleInstance))]
public class PuzzleInstanceEditor : Editor {

    private ReorderableList puzzleList;
    private PuzzleInstance currentTarget;
    private bool drawDefault;
    
    private new SerializedObject serializedObject;
    private SerializedProperty puzzleListProperty; //PuzzleObject-list
    private SerializedProperty symbolPlacementObjectProp; //PuzzleObject-list
    
    private void OnEnable() {
        drawDefault = true;
        serializedObject = new SerializedObject(target);
        puzzleListProperty = serializedObject.FindProperty("puzzleObjects");
        symbolPlacementObjectProp = serializedObject.FindProperty("symbolPlacementObject");
        
        puzzleList = new ReorderableList(serializedObject, puzzleListProperty, true, true, true, true);
        
        SortHierarchy();
        
        if (puzzleList.count > 0) {
            puzzleList.drawElementCallback = DrawListItems;
            puzzleList.onAddCallback = Add;
        }
        
    }
    
    private void SortHierarchy() {
        Transform parent = symbolPlacementObjectProp.objectReferenceValue as Transform;
        
        RemoveNonPuzzleObjects(parent);
        List<int> indicesToSkip = FindAndReplacePrefabReferences(parent);

        SortObjectHierarchy(parent, indicesToSkip);

    }

    private void Add(ReorderableList list) {

        int index = list.index == -1 ? list.serializedProperty.arraySize : list.index;

        list.serializedProperty.InsertArrayElementAtIndex(index);
        list.serializedProperty.GetArrayElementAtIndex(index - 1).objectReferenceValue = null;

        Transform parent = symbolPlacementObjectProp.objectReferenceValue as Transform;

        CreateInstance(parent, index);

    } 

    private void SortObjectHierarchy(Transform parent, List<int> indicesToSkip) {

        List<int> hierarchyIDs = GetHierarchyChildren(parent);
        
        for (int i = 0; i < puzzleList.count; i++) {
            
            if (indicesToSkip.Contains(i))
                continue;

            int myID = puzzleList.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue.GetInstanceID();

            int index = SearchForID(hierarchyIDs, i, myID);

            if (index != -1)
                Swap(parent, i, index);
            
        }
    }

    private List<int> FindAndReplacePrefabReferences(Transform parent) {

        List<int> indices = new List<int>();
        
        for (int i = 0; i < puzzleList.count; i++) {
            bool isClone = puzzleList.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue.name.Contains("Clone");

            if (isClone)
                continue;

            CreateInstance(parent, i);

            indices.Add(i);
        }

        return indices;

    }

    private void RemoveNonPuzzleObjects(Transform parent) {

        List<GameObject> objectsToDestroy = new List<GameObject>();
        
        for (int i = 0; i < parent.childCount; i++) {
            if (parent.GetChild(i).GetComponent<PuzzleObject>())
                continue;
            
            objectsToDestroy.Add(parent.GetChild(i).gameObject);
        }
        
        foreach (var t in objectsToDestroy)
            DestroyImmediate(t);
    }
    
    
    private void Swap(Transform parent, int insertIndex, int valueAtIndex) {
        parent.GetChild(insertIndex).SetSiblingIndex(valueAtIndex);
    }
    
    private List<int> GetHierarchyChildren(Transform parent) {
        
        List<int> children = new List<int>(parent.childCount);

        foreach (Transform child in parent) 
            children.Add( child.GetComponent<PuzzleObject>().GetInstanceID());
        
        return children;
    }

    private int SearchForID(List<int> IDList, int startingIndex, int wantedID) {
        
        for (int index = startingIndex; index < IDList.Count; index++) {
            
            if (Mathf.Abs(IDList[index]) - Mathf.Abs(wantedID) == 0)
                return index;

        }

        return -1;
    }
    
    private void CreateInstance(Transform parent, int insertIndex) {
        
        SerializedProperty property = puzzleList.serializedProperty.GetArrayElementAtIndex(insertIndex);
            
        PuzzleObject prop = property.objectReferenceValue as PuzzleObject;
        
        if (prop == null) return;
        
        PuzzleObject newPuzzleReference = Instantiate(prop, parent);
            
        property.objectReferenceValue = newPuzzleReference;

        puzzleList.serializedProperty.GetArrayElementAtIndex(insertIndex).objectReferenceValue = property.objectReferenceValue;

        serializedObject.ApplyModifiedProperties();
        //newPuzzleReference.gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
    
    public override void OnInspectorGUI() {
        
        if (drawDefault)
            DrawDefaultInspector();
        else {
            
            serializedObject.Update();

            EGL.PropertyField(symbolPlacementObjectProp);
            
            puzzleList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            
        }
            
        
    }
    
    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused) {

        SerializedProperty element = puzzleList.serializedProperty.GetArrayElementAtIndex(index);
        
        EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),  element, GUIContent.none);

        if (element.objectReferenceValue == null)
            return;

        SerializedObject enumValue = new SerializedObject(element.objectReferenceValue);

        EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y, 100, EditorGUIUtility.singleLineHeight), enumValue.FindProperty("modVariant"), GUIContent.none);

        enumValue.ApplyModifiedProperties();

        
    }




}
