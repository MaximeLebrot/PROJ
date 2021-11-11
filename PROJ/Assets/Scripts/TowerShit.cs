using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShit : MonoBehaviour
{
    public int triggerAmount;
    public int puzzleCounter = 0;
    public Animator anim;
    private void OnEnable()
    {
        EventHandler<ExitPuzzleEvent>.RegisterListener(LowerTower);
    }

    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(LowerTower);
    }

    private void LowerTower(ExitPuzzleEvent obj)
    {
        if (obj.success == true)
        {
            puzzleCounter++;
            if(puzzleCounter >= triggerAmount)
            {
                anim.SetTrigger("down");
            }
        }
    }
}
