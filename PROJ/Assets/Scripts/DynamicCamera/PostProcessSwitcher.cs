using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PostProcessSwitcher : MonoBehaviour
{
    [SerializeField] private List<SceneAndVolumePair> volumesByScene = new List<SceneAndVolumePair>();
    [SerializeField] private SceneAndVolumePair contrastVolume;

    private bool contrastModeEnabled = false;
    private SceneAndVolumePair lastEnabled;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventHandler<SaveSettingsEvent>.RegisterListener(CheckForContrastMode);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventHandler<SaveSettingsEvent>.UnregisterListener(CheckForContrastMode);
    }

    private void CheckForContrastMode(SaveSettingsEvent obj)
    {
        contrastModeEnabled = obj.settingsData.highContrastMode;
        Debug.Log(contrastModeEnabled);
        ContrastModeSwitch(contrastModeEnabled);
    }

    private void ContrastModeSwitch(bool contrast)
    {
        contrastVolume.volume.SetActive(contrast);
        if(lastEnabled != null)
            lastEnabled.volume.SetActive(!contrast);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => ChangeVolume();


    private void ChangeVolume()
    {
        foreach(SceneAndVolumePair pair in volumesByScene)
        {
            if (pair.sceneName == SceneManager.GetActiveScene().name)
            {
                pair.volume.SetActive(true);
                lastEnabled = pair;
            }   
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

