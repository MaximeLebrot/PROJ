using UnityEditor;
using UnityEngine;

namespace ScriptTemplateEditor {
    
    public class ScriptTemplateEditor : EditorWindow {

        [SerializeField] private string text;
        private TextAsset _currentTextAsset;

        private SerializedObject _serializedObject;
        private SerializedProperty _textProperty;

        private string _loadedTemplatePath;

        public static readonly string SCRIPT_TEMPLATE_FOLDER_PATH = "Assets/ScriptTemplates";
    
        private bool _textChanged;
        private Vector2 _scroll;
        private GUIStyle _headerStyle;
    
        [MenuItem("Window/Script Editor &f")]
        private static void OpenWindow() => GetWindow<ScriptTemplateEditor>();
    
        private void OnEnable() {
        
            _loadedTemplatePath = null;
        
            if (ScriptTemplateFolderExists() == false)
                AssetDatabase.CreateFolder("Assets", "ScriptTemplates");
        
            _serializedObject = new SerializedObject(this);
            _textProperty = _serializedObject.FindProperty("text");

            _headerStyle = new GUIStyle {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 25,
                fontStyle = FontStyle.BoldAndItalic,
                padding = new RectOffset(20, 20, 20, 20),
                normal = {textColor = new Color(100, 30, 200, .9f)}
            };
        }

        private void OnGUI() {
        
            _serializedObject.Update();
        
            DrawHeader();
            DrawToolBar();
        
            EditorGUI.BeginChangeCheck();
        
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            _currentTextAsset = EditorGUILayout.ObjectField("Current Template", _currentTextAsset, typeof(TextAsset), false) as TextAsset;
            GUILayout.EndHorizontal();

            if (_currentTextAsset == null) {
                _loadedTemplatePath = null;
                return;
            }
            
        
            if (EditorGUI.EndChangeCheck()) 
                LoadNewFile(_currentTextAsset);
        
            _scroll = EditorGUILayout.BeginScrollView(_scroll);
        
            EditorGUI.BeginChangeCheck(); //Check if text has been edited
        
            _textProperty.stringValue = EditorGUILayout.TextArea(_textProperty.stringValue, GUILayout.ExpandHeight(true));
        
            if (EditorGUI.EndChangeCheck())
                _textChanged = true;
        
            EditorGUILayout.EndScrollView();


            using (new EditorGUILayout.HorizontalScope(GUI.skin.textField)) {

                GUI.enabled = _textChanged;

                if (GUILayout.Button("Save")) {
                    this.WriteToTextAsset(AssetDatabase.GetAssetPath(_currentTextAsset), _textProperty.stringValue);
                    AssetDatabase.Refresh();
                    _textChanged = false;
                }
            
                GUI.enabled = true;

                if (GUILayout.Button("Cancel"))
                    GetWindow<ScriptTemplateEditor>().Close();
            
            }

            _serializedObject.ApplyModifiedProperties();

        }
    
        private void LoadNewFile(TextAsset newTextAsset) {
            _textProperty.stringValue = newTextAsset.text;
            _currentTextAsset = newTextAsset;
            _loadedTemplatePath = AssetDatabase.GetAssetPath(_currentTextAsset);
        }


        private void DrawHeader() {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Script Template Editor", _headerStyle, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
        }

        private void DrawToolBar() {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Button("New Template", GUILayout.Width(110)))
                ScriptWizard.OpenWindow(position, LoadNewFile);

            GUI.enabled = _loadedTemplatePath != null;
            if (GUILayout.Button("Delete Template", GUILayout.Width(110)))
                DeleteLoadedTemplate();
            GUI.enabled = true;
        
            EditorGUILayout.EndHorizontal();
        }

        private bool ScriptTemplateFolderExists() {
            return AssetDatabase.IsValidFolder(SCRIPT_TEMPLATE_FOLDER_PATH);
        }
    
        private void DeleteLoadedTemplate() {
            bool deleteConfirmed = EditorUtility.DisplayDialog("Delete Template", "Are you sure you want to delete " + _currentTextAsset.name + "?", "Yes", "No");

            if (deleteConfirmed) {
                AssetDatabase.DeleteAsset(_loadedTemplatePath);
                _currentTextAsset = null;
                _loadedTemplatePath = null;
            }
        
        }
    }
}
