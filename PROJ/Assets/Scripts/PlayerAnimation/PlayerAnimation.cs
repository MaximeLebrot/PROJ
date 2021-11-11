using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //references
    [SerializeField] private ControllerInputReference inputReference;
    private Animator anim; 
    
    private Vector2 input;
    private int x, y;
    private void Awake()
    {
        x = Animator.StringToHash("speed");
        y = Animator.StringToHash("direction");
        anim = GetComponent<Animator>(); 
    }
    
    private void Update()
    {
        input = inputReference.InputMaster.Movement.ReadValue<Vector2>();
        anim.SetFloat(x, input.x);
        anim.SetFloat(y, input.y);
    }
}
