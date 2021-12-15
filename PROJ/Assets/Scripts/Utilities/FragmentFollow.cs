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
    public bool follow;
    private bool deposit;

    [SerializeField] private Animator anim;

    private void Start()
    {
        scale /= 10;
        downScale = new Vector3(scale, scale, scale);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fragmentHolder = other.transform;
            follow = true;
            GetComponent<Collider>().isTrigger = false;
        }
            
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
        Debug.Log("FOLOOW");
        if (transform.localScale.x > fragmentSize)
            transform.localScale -= downScale;
        if (Vector3.Distance(fragmentOrb.position, fragmentHolder.position + Vector3.up * 1.5f) > 0.1f)
            fragmentOrb.position = Vector3.Lerp(fragmentOrb.position, fragmentHolder.position + Vector3.up * 1.5f, Time.deltaTime * speed);
    }

    private void FragmentDeposit()
    {
        follow = false;
        fragmentOrb.position = Vector3.Lerp(fragmentOrb.position, fragmentDeposit.position, Time.deltaTime * speed);
        if (Vector3.Distance(fragmentOrb.position, fragmentDeposit.position) < 0.1f)
        {
            deposit = false;
            Destroy(this, 5);
        }    
    }

    public void DepositFragment(FragmentDeposit frag) 
    {
        fragmentDeposit = frag.transform;
        deposit = true;

    
    }
}
