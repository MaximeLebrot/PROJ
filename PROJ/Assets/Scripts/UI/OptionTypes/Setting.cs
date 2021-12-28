using System;
using UnityEngine;


[Serializable]
public class Setting<T> {
    
    [SerializeField] private T value;
    [SerializeField] private string name;
    
    public T Value {
        get => value;
        set => this.value = value;
    }

    public string Name {
        get => name;
        set => name = value;
    }

    public Setting(T value, string name) {
        this.value = value;
        this.name = name;
    }
}
