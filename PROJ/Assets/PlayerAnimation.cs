using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //references
    ControllerInputReference inputReference;
    Animator anim; 
    //
    public Vector2 input;
    private void Awake()
    {
        anim = GetComponent<Animator>(); 
    }

    private void Update()
    {
        inputReference.InputMaster.Movement.ReadValue<Vector2>();
    }
}
