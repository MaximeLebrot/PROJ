using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    void Awake()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.transform.position = startPos.position;
        player.transform.rotation = startPos.rotation;
        player.GetComponent<PlayerController>().ResetCharacterModel();
    }


}
