using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : ObjectToActivate
{
    [SerializeField] private GameObject obj;
    public override void Activate(ActivatorEvent eve)
    {
        if (eve.info.ID == puzzleID)
        {
            obj.SetActive(true);
        }
    }
}
