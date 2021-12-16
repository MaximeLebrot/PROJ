using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTeleport : MonoBehaviour
{
    public Transform TeleportTarget;
    public GameObject Player;
    private FMOD.Studio.EventInstance PortalEnter;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Player = col.gameObject;
            Player.transform.position = TeleportTarget.transform.position;
            //Debug.Log("It fcking works ma dude");
            PortalEnter = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/Soft Magic/Portal/PortalEnter");
            PortalEnter.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PortalEnter.start();
            PortalEnter.release();
        }
    }
}


