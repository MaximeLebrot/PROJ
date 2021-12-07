using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        //anim.SetTrigger("open");
    }
}
