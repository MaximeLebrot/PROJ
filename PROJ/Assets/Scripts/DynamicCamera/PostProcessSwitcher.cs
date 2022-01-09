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
        (GameMenuController.Instance.RequestOption<Use_HighContrastMode>() as Use_HighContrastMode).AddListener(EnableContrastMode);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if(GameMenuController.Instance != null)
            (GameMenuController.Instance.RequestOption<Use_HighContrastMode>() as Use_HighContrastMode).RemoveListener(EnableContrastMode);
    }
    private void EnableContrastMode(bool active)
    {
        contrastModeEnabled = active;
        ContrastModeSwitch(contrastModeEnabled);
    }
    /*private void CheckForContrastMode(SaveSettingsEvent obj)
    {
        contrastModeEnabled = obj.settingsData.highContrastMode;
        //Debug.Log(contrastModeEnabled);
        ContrastModeSwitch(contrastModeEnabled);
    }*/

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
            if(pair.volume == null)
                continue;
            
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

