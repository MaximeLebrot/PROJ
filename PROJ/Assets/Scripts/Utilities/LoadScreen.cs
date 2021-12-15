using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    private string sceneToLoad;
    Animator anim;

    private void Start()
    {
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
        sceneToLoad = eve.sceneToLoad;
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
