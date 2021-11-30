using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightAnimationScript : MonoBehaviour
{
    private Animator jumpAnimator;
    private MetaPlayerController mpc;
    private bool moveToCenter;
    [SerializeField] private string triggerValue;

    private void Awake()
    {
        jumpAnimator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            jumpAnimator.SetTrigger(triggerValue);
            mpc = col.GetComponent<MetaPlayerController>();
            mpc.physics.enabled = false;
            mpc.transform.parent = this.transform;
            mpc.enabled = false;
            moveToCenter = true;
        }
    }
    public void ActivateScripts()
    {
        GetComponent<SphereCollider>().enabled = false;
        mpc.transform.parent = null;
        mpc.physics.enabled = true;
        mpc.enabled = true;
    }

    private void Update()
    {
        if(moveToCenter)
        {
            if(Vector3.Distance(mpc.transform.position, this.transform.position) < 0.1f)
                moveToCenter = false;
        
            mpc.transform.position = Vector3.MoveTowards(mpc.transform.position, this.transform.position, Time.deltaTime);
  
        }
    }
    //Maybe we also want to destroy this game object when we're done with it, OR make it part of some type of object pooling. Just leaving it in the scene serves no purpose.

}

