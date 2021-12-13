using UnityEngine;

public class FragmentFollow : MonoBehaviour
{
    public GameObject holder;
    public GameObject player;
    public GameObject fragment;
    public GameObject fancyBall;
    public float offset = 2f;
    private Vector3 pos;

    private void Start()
    {
        fancyBall.transform.parent = transform;
        fancyBall.transform.localPosition = Vector3.zero;
    }
    private void FixedUpdate()
    {
        pos = player.transform.position;
        pos.y += offset;
        holder.transform.position = pos;
        //fancyBall.transform.position = transform.position;
    }

}
