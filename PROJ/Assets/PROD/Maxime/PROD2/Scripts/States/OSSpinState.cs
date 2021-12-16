using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSSpinState")]
public class OSSpinState : PlayerState
{
    private float rotateSpeed = 50f;

    public override void Initialize() => base.Initialize();

    public override void EnterState()
    {
        base.EnterState();
        player.physics.velocity = Vector3.zero;
    }

    public override void RunUpdate()
    {
        Rotate();
        if (player.inputReference.OneSwitchInputMaster.OnlyButton.triggered)
            stateMachine.ChangeState<OSWalkState>();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void Rotate() => player.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
}