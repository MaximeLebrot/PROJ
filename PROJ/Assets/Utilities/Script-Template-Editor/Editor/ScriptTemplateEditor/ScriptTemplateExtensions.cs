using System.IO;
using UnityEditor;
using UnityEngine;

namespace ScriptTemplateEditor {
    public static class ScriptTemplateExtensions {

        public enum DockingPosition {
            TopRight,
            TopLeft,
            BottomRight,
            BottomLeft
        }
    
        public static void WriteToTextAsset(this EditorWindow window, string path, string text) {
            StreamWriter file = new StreamWriter(path, false);
        
            file.WriteLine(text);
        
            file.Close();
        
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        
        }

        public static void LooselyDockToWindowCorner(this EditorWindow window, Rect otherWindowRect, DockingPosition dockingPosition) {

            Vector2 cornerPosition = Vector2.zero;
        
            switch (dockingPosition) {
                case DockingPosition.TopRight:
                    cornerPosition = new Vector2(otherWindowRect.xMax, otherWindowRect.yMin);
                    break;
                case DockingPosition.TopLeft:
                    cornerPosition = new Vector2(otherWindowRect.xMin, otherWindowRect.yMin);
                    break;
                case DockingPosition.BottomLeft:
                    cornerPosition = new Vector2(otherWindowRect.xMin, otherWindowRect.yMax);
                    break;
                case DockingPosition.BottomRight:
                    cornerPosition = new Vector2(otherWindowRect.xMax, otherWindowRect.yMax);
                    break;
            
            }
        
            window.position = new Rect(cornerPosition, window.maxSize);
        
        }
    }
}
