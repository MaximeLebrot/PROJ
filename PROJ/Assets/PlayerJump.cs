using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Animator JumpAnimator;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            JumpAnimator.SetTrigger("First");
            Debug.Log("Its jumping");
        }
    }
}

