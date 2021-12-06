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
        EventHandler<LoadSceneEvent>.RegisterListener(StartLoading);
    }

    private void OnDisable()
    {
        EventHandler<LoadSceneEvent>.UnregisterListener(StartLoading);
    }

    private void StartLoading(LoadSceneEvent eve)
    {
        sceneToLoad = eve.sceneToLoad;
        anim.SetTrigger("load");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


}
