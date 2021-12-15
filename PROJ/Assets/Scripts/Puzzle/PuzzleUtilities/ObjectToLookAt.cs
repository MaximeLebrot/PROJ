using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLookAt : ObjectToActivate
{
    [SerializeField] float transitionTime;
    [SerializeField] float delayWhenDone;
    [SerializeField] float rotationSpeed;
    public override void Activate(ActivatorEvent eve)
    {
       if (eve.info.ID == puzzleID)
        {
            EventHandler<CameraLookAtEvent>.FireEvent(
                new CameraLookAtEvent(this.transform, transitionTime, delayWhenDone, rotationSpeed));
        }
    }
}
