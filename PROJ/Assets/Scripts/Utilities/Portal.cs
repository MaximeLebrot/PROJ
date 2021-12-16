using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string sceneToLoad;

    private void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    public void Open()
    {
        //send LookAtEvent
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        EventHandler<UnLoadSceneEvent>.FireEvent(new UnLoadSceneEvent(sceneToLoad));
    }
}
