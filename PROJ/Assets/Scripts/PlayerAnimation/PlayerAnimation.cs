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
        float dot = Vector3.Dot(pc.cameraTransform.forward, transform.forward) < rotationThreshold ? -1f : 1f;

        anim.SetFloat(x, input.x * dot);
        anim.SetFloat(y, input.y * dot);
    }
}
