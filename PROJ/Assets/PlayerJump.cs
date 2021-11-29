using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Animator PlayerAnimator;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Glideable"))
        {
            PlayerAnimator.SetTrigger("JumpOne");
            Debug.Log("Its jumping");
        }
    }
}

