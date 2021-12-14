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
        Vector3 charRotationMovement = mpc.transform.rotation * mpc.physics.GetXZMovement();
        input = mpc.inputReference.InputMaster.Movement.ReadValue<Vector2>();
        float cameraDot = Vector3.Dot(mpc.playerController3D.cameraTransform.forward, transform.forward) < rotationThreshold ? -1f : 1f;
        float dot = Vector3.Dot(mpc.playerController3D.characterModel.transform.forward, mpc.physics.GetXZMovement()) < rotationThreshold ? -1f : 1f;
        float forwardAnimation = dot > 0 ? Mathf.Abs(input.y) : input.y;
        anim.SetFloat(x, input.x );
        anim.SetFloat(y, forwardAnimation);

    }
}
