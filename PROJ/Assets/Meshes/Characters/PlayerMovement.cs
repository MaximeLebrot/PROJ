using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator anim;
    public float speed;
    public float direction;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
     /*   float speed = Input.GetAxis("Vertical");
        float direction = Input.GetAxis("Horizontal");

          Debug.Log(speed + "" + direction);
       anim.SetFloat("speed", speed);
        anim.SetFloat("direction", direction);
        */

    }

}

