using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string sceneToLoad;
    private FMOD.Studio.EventInstance PortalEnter;

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
        PortalEnter = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/Soft Magic/Portal/PortalEnter");
        PortalEnter.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PortalEnter.start();
        PortalEnter.release();
    }
}
