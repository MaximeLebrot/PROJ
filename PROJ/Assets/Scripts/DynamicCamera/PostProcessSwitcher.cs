using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PostProcessSwitcher : MonoBehaviour
{
    [SerializeField] private List<SceneAndVolumePair> volumesByScene = new List<SceneAndVolumePair>();

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => ChangeVolume();


    private void ChangeVolume()
    {
        foreach(SceneAndVolumePair pair in volumesByScene)
        {
            if (pair.sceneName == SceneManager.GetActiveScene().name)
                pair.volume.SetActive(true);
            else
                pair.volume.SetActive(false);
        }
    }

}

[Serializable]
public class SceneAndVolumePair
{
    public string sceneName;
    public GameObject volume;
}

