using UnityEditor;
using UnityEngine;

public class ObjectHelper {

    
    /// <summary>
    /// Only works for objects with no parents
    /// </summary>
    [MenuItem("Tools/Add In Game Menu")]
    public static void AddInGameMenu() {
        GameObject[] roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject sceneObject in roots) {
            bool settingsMenuExists = sceneObject.GetComponent<InGameMenu>();

            if (settingsMenuExists) {
                Debug.Log("In game settings exists");
                return;
            }
        }
        
        string[] assets = AssetDatabase.FindAssets("InGameMenu", new [] {"Assets/Prefabs"});

        if (assets.Length < 1) {
            Debug.LogError("Could not find prefab");
            return;
        }
        string pathToPrefab = AssetDatabase.GUIDToAssetPath(assets[0]);

        Object prefab = AssetDatabase.LoadAssetAtPath<Object>(pathToPrefab);

        PrefabUtility.InstantiatePrefab(prefab);
        Debug.Log("Adding in game menu");
    }
    
    
}
