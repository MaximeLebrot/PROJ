using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/PuzzleState")]
public class PuzzleState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void EnterState()
    {
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
        base.EnterState();
    }

    public override void ExitState()
    {
        player.playerController3D.enabled = true;
        player.puzzleController.enabled = false;
        base.ExitState();
    }
}
