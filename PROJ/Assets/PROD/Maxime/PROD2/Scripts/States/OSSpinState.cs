using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSSpinState")]
public class OSSpinState : PlayerState
{
    private float rotateSpeed = 50f;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void EnterState()
    {
        Debug.Log("Entered Spin State");
        base.EnterState();
        player.physics.SetGlide(false);
    }

    public override void RunUpdate()
    {
        Rotate();

        if (player.inputReference.OneSwitchInputMaster.OnlyButton.triggered)
            stateMachine.ChangeState<OSWalkState>();

        //if (!player.playerController3D.IsGrounded())
            //stateMachine.ChangeState<OSAirborneState>();

        //if (player.physics.velocity.magnitude > player.physics.SurfThreshold + 1 && player.playerController3D.groundHitAngle < player.playerController3D.GlideMinAngle)
            //stateMachine.ChangeState<OSGlideState>();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void Rotate()
    {
        player.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}