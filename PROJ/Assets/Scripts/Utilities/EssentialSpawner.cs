using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject cam;


    private void OnEnable()
    {
        if(GameObject.FindGameObjectWithTag("Player") == false)
        {
            Instantiate(player);
        }
        if (GameObject.FindGameObjectWithTag("MainCamera") == false)
        {
            Instantiate(cam);
        }
        if (GameObject.FindGameObjectWithTag("GameMenu") == false)
        {
            Instantiate(menu);
        }
    }
}
