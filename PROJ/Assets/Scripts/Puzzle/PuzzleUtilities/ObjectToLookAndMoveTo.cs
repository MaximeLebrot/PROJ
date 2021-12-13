using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLookAndMoveTo : ObjectToActivate
{
    [SerializeField] Vector3 endPosition;
    [SerializeField] Quaternion endRotation; 

    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 offsetToTarget;
    [SerializeField] float rotationSpeed;
    public override void Activate(ActivatorEvent eve)
    {
        if (eve.info.ID == puzzleID)
        {
            EventHandler<CameraLookAndMoveToEvent>.FireEvent(
                new CameraLookAndMoveToEvent(endPosition, endRotation, moveSpeed, offsetToTarget, rotationSpeed));
        }
    }
}
