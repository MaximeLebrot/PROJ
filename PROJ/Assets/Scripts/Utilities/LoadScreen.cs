using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    private string sceneToLoad;
    private Animator anim;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        anim = GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        EventHandler<UnLoadSceneEvent>.RegisterListener(StartLoading);
    }

    private void OnDisable()
    {
        EventHandler<UnLoadSceneEvent>.UnregisterListener(StartLoading);
    }

    private void StartLoading(UnLoadSceneEvent eve)
    {
        Debug.Log("Start Loading, unload scene event read");
        sceneToLoad = eve.sceneToLoad;
        anim.SetTrigger("load");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EventHandler<LoadSceneEvent>.FireEvent(new LoadSceneEvent());
        Debug.Log("On Scene Loaded");
        anim.SetTrigger("stopLoad");
    }
    //Triggered from animation event
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
