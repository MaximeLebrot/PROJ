using UnityEngine;

public class FragmentFollow : MonoBehaviour
{
    public Transform fragmentHolder;
    public Transform fragmentOrb;
    public Transform fragmentDeposit;
    public float speed = 5f;
    [SerializeField, Range(0.1f, 1f)] private float scale = 0.5f;
    [SerializeField, Range(0.1f, 2f)] private float fragmentSize = 1f;

    private Vector3 downScale;
    private bool follow;
    private bool deposit;

    private void Start()
    {
        scale /= 10;
        downScale = new Vector3(scale, scale, scale);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            follow = true;
    }

    private void Update()
    {
        if (follow)
            FragmentFollowPlayer();
        if (deposit)
            FragmentDeposit();
    }

    private void FragmentFollowPlayer()
    {
        if (transform.localScale.x > fragmentSize)
            transform.localScale -= downScale;
        if (Vector3.Distance(fragmentOrb.position, fragmentHolder.position) > 0.1f)
            fragmentOrb.position = Vector3.Lerp(fragmentOrb.position, fragmentHolder.position, Time.deltaTime * speed);
    }

    private void FragmentDeposit()
    {
        if (follow)
            follow = false;
        fragmentOrb.position = Vector3.Lerp(fragmentOrb.position, fragmentDeposit.position, Time.deltaTime * speed);
        if (Vector3.Distance(fragmentOrb.position, fragmentDeposit.position) < 0.1f)
        {
            deposit = false;
            Debug.Log("Fragment deposited");
        }    
    }

    public void DepositFragment() => deposit = true;
}
