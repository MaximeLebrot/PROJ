using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSSpinState")]
public class OSSpinState : PlayerState
{
    private GameObject armlessCamera;
    private float rotateSpeed = 50f;

    public override void Initialize() => base.Initialize();

    public override void EnterState()
    {
        base.EnterState();
        armlessCamera = player.GetComponent<VoiceInputController>().armlessCamera;
        armlessCamera.SetActive(true);
        //player.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        armlessCamera.SetActive(false);
    }

    private void Rotate() => player.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
}