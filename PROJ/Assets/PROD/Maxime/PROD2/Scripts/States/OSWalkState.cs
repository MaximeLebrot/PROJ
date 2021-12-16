using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSWalkState")]
public class OSWalkState : PlayerState
{
    private GameObject armlessCamera;
    private Vector2 forward = new Vector2(0f, 1f);

    public override void Initialize() => base.Initialize();

    public override void EnterState()
    {
        base.EnterState();
        armlessCamera = player.GetComponent<VoiceInputController>().armlessCamera;
        armlessCamera.SetActive(true);
        //player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void RunUpdate()
    {
        WalkForward();

        if (player.inputReference.OneSwitchInputMaster.OnlyButton.triggered)
            stateMachine.ChangeState<OSSpinState>();
    }

    public override void ExitState()
    {
        base.ExitState();
        armlessCamera.SetActive(false);
    }

    private void WalkForward() => player.playerController3D.InputWalk(forward);
}
