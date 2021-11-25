using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectToActivate : MonoBehaviour
{
    [SerializeField] int puzzleID;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    abstract public void Activate();
}
