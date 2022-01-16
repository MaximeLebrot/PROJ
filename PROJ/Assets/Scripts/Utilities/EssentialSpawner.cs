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
        if (GameObject.FindGameObjectWithTag("GameMenu") == false)
        {
            Debug.Log("DID NOT FIND :: MENU");
            menu.gameObject.SetActive(true);
            menu.transform.SetParent(null);
            DontDestroyOnLoad(menu);
        }
        if (GameObject.FindGameObjectWithTag("MainCamera") == false)
        {
            Debug.Log("DID NOT FIND :: CAMERA");
            cam.gameObject.SetActive(true);
            cam.transform.SetParent(null);
            DontDestroyOnLoad(cam);
        }
        if (GameObject.FindObjectOfType<MetaPlayerController>() == false)
        {
            Debug.Log("DID NOT FIND :: PLAYER");
            player.gameObject.SetActive(true);
            player.transform.SetParent(null);
            DontDestroyOnLoad(player);
        }
        if (GameObject.FindGameObjectWithTag("CanvasLogbook") == false)
        {
            Debug.Log("DID NOT FIND :: LOGBOOK");
            canvasLogbook.gameObject.SetActive(true);
            canvasLogbook.transform.SetParent(null);
            DontDestroyOnLoad(canvasLogbook);
        }
        if (GameObject.FindGameObjectWithTag("AudioManager") == false)
        {
            Debug.Log("DID NOT FIND :: AUDIO");
            audioManager.gameObject.SetActive(true);
            audioManager.transform.SetParent(null);
            DontDestroyOnLoad(audioManager);
        }

        Destroy(gameObject);
    }
}
