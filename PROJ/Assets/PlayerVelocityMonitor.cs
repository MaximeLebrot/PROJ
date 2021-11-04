using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityMonitor : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 previousPosition;
        
    private void Awake() {
        player ??= GameObject.FindWithTag("Player").transform;

        previousPosition = player.position;
    }

    [ContextMenu("Auto-assign target", false,0)]
    public void AssignTargets() {
        try {
            player = GameObject.FindWithTag("Player").transform;
        } catch (System.NullReferenceException e) {
            Debug.Log("No object in hierarcy tagged with Player");
        }
    }
    
    

  
    
}
