using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIMenuItem : MonoBehaviour {
    public int ID { get; private set; }

    public bool hasHashedID = false;
    
    public void GenerateID() {
        ID = name.GetHashCode();
        hasHashedID = true;
        Initialize();
    }

    protected virtual void Initialize() {}

    public abstract dynamic GetValue();
    public abstract void SetValue(dynamic value);

    
    
    /// <summary>
    /// Allows for adding listeners to the UIItem. Works with lambdas.
    /// </summary>
    /// <param name="action"></param>
    public abstract void OnValueChanged(Action action);
}
