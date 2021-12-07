using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTeleport : MonoBehaviour
{
    public Transform TeleportTarget;
    public GameObject Player;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Player = col.gameObject;
            Player.transform.position = TeleportTarget.transform.position;
            //Debug.Log("It fcking works ma dude");
        }
    }
}


