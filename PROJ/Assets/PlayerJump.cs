using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Animator JumpAnimator;
    MetaPlayerController MPC;
   
    void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("Player"))
        {
            JumpAnimator.SetTrigger("First");
            Debug.Log("Its jumping");
            MPC = col.GetComponent<MetaPlayerController>();
            MPC.playerController3D.enabled = false;
            MPC.physics.enabled = false;
            MPC.transform.parent = this.transform;
        }
    }

    public void ActivateScripts()
    {
        GetComponent<SphereCollider>().enabled = false;
        MPC.transform.parent = null;
        MPC.playerController3D.enabled = true;
        MPC.physics.enabled = true;
        MPC.physics.velocity = Vector3.zero;
        Debug.Log("Rickard");
    }
}

