using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject canvasLogbook;
    [SerializeField] private GameObject audioManager;


    private void OnEnable()
    {

        if (GameObject.FindGameObjectWithTag("MainCamera") == false)
        {
            cam.gameObject.SetActive(true);
            cam.transform.parent = null;
            DontDestroyOnLoad(cam);
        }
        if (GameObject.FindGameObjectWithTag("Player") == false)
        {
            player.gameObject.SetActive(true);
            player.transform.parent = null;
            DontDestroyOnLoad(player);
        }
        if (GameObject.FindGameObjectWithTag("GameMenu") == false)
        {
            menu.gameObject.SetActive(true);
            menu.transform.parent = null;
            DontDestroyOnLoad(menu);
        }
        if (GameObject.FindGameObjectWithTag("CanvasLogbook") == false)
        {
            canvasLogbook.gameObject.SetActive(true);
            canvasLogbook.transform.parent = null;
            DontDestroyOnLoad(canvasLogbook);
        }
        if (GameObject.FindGameObjectWithTag("AudioManager") == false)
        {
            audioManager.gameObject.SetActive(true);
            audioManager.transform.parent = null;
            DontDestroyOnLoad(audioManager);
        }

        Destroy(gameObject);
    }
}
