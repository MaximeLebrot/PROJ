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
        Transform cameraFollowTarget = GameObject.FindGameObjectWithTag("CameraFollowTarget").transform;
        //cameraFollowTarget.localRotation = Quaternion.Euler(0, 0, 0);

    }


}
