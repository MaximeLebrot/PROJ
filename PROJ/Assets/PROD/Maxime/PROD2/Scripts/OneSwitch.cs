using UnityEngine;

public class OneSwitch : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float rotateSpeed = 50;
    private InputMaster inputMaster;
    private bool walking;

    #region UnityEngine Stuff
    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Update()
    {
        if (inputMaster.OneSwitch.OnlyButton.triggered)
        {
            SwitchState();
        }
        if (walking)
            WalkForward();
        else
            RotateInPlace();
    }
    #endregion
    
    private void SwitchState()
    {
        walking = !walking;
    }

    private void WalkForward()
    {
        if (rb.freezeRotation == false)
            rb.freezeRotation = true;
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward);
        } 
    }

    private void RotateInPlace()
    {
        if (rb.velocity != Vector3.zero)
            rb.velocity = Vector3.zero;
        if (rb.freezeRotation == true)
            rb.freezeRotation = false;
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
