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

        physics = GetComponent<PlayerPhysicsSplit>();
        pc = GetComponent<PlayerController>(); 
    }
    PlayerController pc;
    PlayerPhysicsSplit physics;

    float rotationThreshold = -0.7f;
    private void Update()
    {
        input = inputReference.InputMaster.Movement.ReadValue<Vector2>();
        float cameraDot = Vector3.Dot(pc.cameraTransform.forward, transform.forward) < rotationThreshold ? -1f : 1f;
        float dot = Vector3.Dot(transform.forward, physics.GetXZMovement()) < rotationThreshold ? -1f : 1f;
        anim.SetFloat(x, input.x * cameraDot);

        float forwardAnimation = dot > 0 ? Mathf.Abs(input.y) : input.y;
        //Obviously not exactly what we want to do, but in principle this sort of calculation should go into feeding the animator controller.
        anim.SetFloat(y, forwardAnimation);
    }
}
