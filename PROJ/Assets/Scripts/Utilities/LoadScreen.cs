using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    private string sceneToLoad;
    Animator anim;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventHandler<UnLoadSceneEvent>.RegisterListener(StartLoading);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventHandler<UnLoadSceneEvent>.UnregisterListener(StartLoading);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        anim.SetTrigger("stopLoad");
    }
    private void StartLoading(UnLoadSceneEvent eve)
    {
        sceneToLoad = eve.sceneToLoad;
        Debug.Log(sceneToLoad.ToString());
        anim.SetTrigger("load");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SendLoadEvent()
    {
        EventHandler<LoadSceneEvent>.FireEvent(new LoadSceneEvent());
    }


}
