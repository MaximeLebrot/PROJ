
using System;
using UnityEditor;
using UnityEngine;


public enum DockingPosition {
    TopRight,
    TopLeft,
    BottomRight,
    BottomLeft
}

public class ExistingOptionsWindow : EditorWindow {

    private static ExistingOptionsWindow window;
    private EditorWindow optionsWindow;
    private DockingPosition dockingPosition;
    
    private static readonly Vector2 MaxSize = new Vector2(300, 500);
    
    public static ExistingOptionsWindow OpenWindow() {
        
        if(window != null)
            window.Close();
        
        window = GetWindow<ExistingOptionsWindow>();
        
        window.maxSize = MaxSize;

        return window;
    }


    public void Initialize(EditorWindow parentWindow, DockingPosition dockingPosition) {
        optionsWindow = parentWindow;
        this.dockingPosition = dockingPosition;
    }


    public void MoveWithWindow() {
        LooselyDockToWindowCorner(optionsWindow.position, dockingPosition);
    }
    
    
    
    private void LooselyDockToWindowCorner(Rect otherWindow, DockingPosition dockingPosition) {

        Vector2 cornerPosition = Vector2.zero;
        
        switch (dockingPosition) {
            case DockingPosition.TopRight:
                cornerPosition = new Vector2(otherWindow.xMax, otherWindow.yMin);
                break;
            case DockingPosition.TopLeft:
                cornerPosition = new Vector2(otherWindow.xMin, otherWindow.yMin);
                break;
            case DockingPosition.BottomLeft:
                cornerPosition = new Vector2(otherWindow.xMin, otherWindow.yMax);
                break;
            case DockingPosition.BottomRight:
                cornerPosition = new Vector2(otherWindow.xMax, otherWindow.yMax);
                break;
            
        }
        
        window.position = new Rect(cornerPosition, window.maxSize);
        
    }
}
