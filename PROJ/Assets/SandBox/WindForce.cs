using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    private float windForce = 105;
    private Vector3 windDirection = Vector3.back;
   
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");
        PlayerPhysicsSplit physics = other.gameObject.GetComponent<PlayerPhysicsSplit>();
        if (physics)
            Debug.Log("physics assigned");
        physics?.AddForce(windForce * windDirection);
    }
}
