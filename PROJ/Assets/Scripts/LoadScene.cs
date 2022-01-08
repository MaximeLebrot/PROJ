using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string nextScene;

    public void LoadNextScene()
    {
        EventHandler<UnLoadSceneEvent>.FireEvent(new UnLoadSceneEvent(nextScene));
    }
    

}
