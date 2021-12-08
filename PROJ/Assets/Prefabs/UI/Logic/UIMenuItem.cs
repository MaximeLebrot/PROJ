using UnityEngine;

public abstract class UIMenuItem : MonoBehaviour {
    public int ID { get; private set; }

    private void Awake() {
        ID = name.GetHashCode();
        Initialize();
    }

    protected virtual void Initialize() {}

    public abstract dynamic GetValue();
    public abstract void SetValue(dynamic value);
}
