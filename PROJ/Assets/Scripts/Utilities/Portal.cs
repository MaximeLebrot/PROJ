using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string sceneToLoad;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Open()
    {
        anim.SetTrigger("open");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
            EventHandler<UnLoadSceneEvent>.FireEvent(new UnLoadSceneEvent(sceneToLoad));
        }
    }
}
