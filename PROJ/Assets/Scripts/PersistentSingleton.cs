using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component {
    
    public static T Instance { get; private set; }

    private void Awake() {
        
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else {
            Instance = this as T;
            DontDestroyOnLoad(this);
            InitializeSingleton();
        }
            

    }
    
    protected virtual void InitializeSingleton() {}

}
