using System.Collections;using UnityEngine;


[CreateAssetMenu(menuName = "PlayerStates/MenuState")]
public class MenuState : PlayerState {

    private Transform cameraTransform;
    
    public override void Initialize()
    {
        base.Initialize();
        cameraTransform = Camera.main.transform;
        
    }

    public override void EnterState() => player.physics.enabled = false;

    public override void RunUpdate() {

        Quaternion rotation = Quaternion.LookRotation(cameraTransform.position - player.transform.position);
        Vector3 eulerRotation = rotation.eulerAngles;

        eulerRotation.x = 0;
        eulerRotation.z = 0;

        player.transform.eulerAngles = Vector3.Lerp(player.transform.eulerAngles, eulerRotation, Time.deltaTime * 5f);
    }

    public override void ExitState() => player.physics.enabled = true;
}
