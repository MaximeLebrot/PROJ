using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightAnimationScript : MonoBehaviour
{
    private Animator jumpAnimator;
    private MetaPlayerController mpc;
    private void Awake()
    {
        jumpAnimator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            jumpAnimator.SetTrigger("First");
            mpc = col.GetComponent<MetaPlayerController>();
            mpc.physics.enabled = false;
            mpc.transform.parent = this.transform;
            mpc.enabled = false;
        }
    }
    public void ActivateScripts()
    {
        GetComponent<SphereCollider>().enabled = false;
        mpc.transform.parent = null;
        mpc.physics.enabled = true;
        mpc.enabled = true;
    }

    //Maybe we also want to destroy this game object when we're done with it, OR make it part of some type of object pooling. Just leaving it in the scene serves no purpose.

}

