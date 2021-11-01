using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ScriptTemplateEditor {
    
    public class NewCategoryWindow : EditorWindow {

        private static Action<int, string> OnCreateCategory;

        private int _contextMenuOrderIndex;
        private string _orderIndexString;
        private string[] _orderIndexes;
        private string _textField;

        private static readonly Rect buttonPosition = new Rect(540, 80, 60, 20);

        public static void OpenWindow(Rect parentWindowPosition, Action<int, string> onCreateCategory) {
            NewCategoryWindow window = GetWindow<NewCategoryWindow>("New Category");
        
            window.minSize = new Vector2(600, 100);
            window.maxSize = new Vector2(600, 100);
        
            window.LooselyDockToWindowCorner(parentWindowPosition, ScriptTemplateExtensions.DockingPosition.TopRight);

            OnCreateCategory += onCreateCategory;
        }

        private void OnEnable() {
            int[] intOrderValues = Enumerable.Range(70, 30).ToArray();

            _orderIndexes = new string[intOrderValues.Length];
        
            for (int i = 0; i < intOrderValues.Length; i++)
                _orderIndexes[i] = intOrderValues[i].ToString();
        }
    
        private void OnGUI() {
        
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            _contextMenuOrderIndex = EditorGUILayout.Popup("Menu Sort Order", _contextMenuOrderIndex, _orderIndexes);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            _textField = EditorGUILayout.TextField("Category name", _textField, GUILayout.ExpandWidth(true)); 
            EditorGUILayout.EndHorizontal();
        
            if (GUI.Button(buttonPosition, "Create")) {

                string newCategory = String.IsNullOrEmpty(_textField) ? "" : _orderIndexes[_contextMenuOrderIndex] + "-" + _textField;
            
                if (newCategory.Equals(""))
                    Debug.LogWarning("Category not valid: field was empty");
                else
                    OnCreateCategory?.Invoke(int.Parse(_orderIndexes[_contextMenuOrderIndex]), newCategory);

                OnCreateCategory = null;
                Close();
            
            }

        }

    }
}
