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
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Open should be called from the fragment holder/whatever, not here. Only for testing.
        //Open();
        Debug.Log("hehehe");
        EventHandler<UnLoadSceneEvent>.FireEvent(new UnLoadSceneEvent(sceneToLoad));
    }
}
