using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSceneLoader : MonoBehaviour
{

    public string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        EventHandler<UnLoadSceneEvent>.FireEvent(new UnLoadSceneEvent(sceneToLoad));
    }
}
