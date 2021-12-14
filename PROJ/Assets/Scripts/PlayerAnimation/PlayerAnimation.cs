using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //references
    [SerializeField] private ControllerInputReference inputReference;
    private Animator anim;
    private MetaPlayerController mpc;
    
    private Vector2 input;
    private int x, y;
    private void Awake()
    {

        x = Animator.StringToHash("speed");
        y = Animator.StringToHash("direction");
        anim = GetComponent<Animator>();
        mpc = GetComponent<MetaPlayerController>();
    }

    float rotationThreshold = -0.7f;
    private void Update()
    {
        input = mpc.inputReference.InputMaster.Movement.ReadValue<Vector2>();
        float dot = Vector3.Dot(mpc.playerController3D.characterModel.transform.forward, mpc.physics.GetXZMovement());
        anim.SetFloat(x, input.x);
        anim.SetFloat(y, dot);
    }
}
