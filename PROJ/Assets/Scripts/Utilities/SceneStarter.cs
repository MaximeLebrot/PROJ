using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;
        player.GetComponent<PlayerController>().ResetCharacterModel();
        
        
        
        
        //Restarts the cloth. It Breaks for some reason
        GameObject solver = GameObject.FindGameObjectWithTag("Solver");
        solver.SetActive(false);
        solver.SetActive(true);
        Debug.Log("SCENE STARTER");
    }


}
