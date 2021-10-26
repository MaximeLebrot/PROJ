using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HearCollision : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip alertSound;

    float soundLength = 0f;

    float time = 0f;
    float timer;
    [SerializeField] float cd;

    bool canPlay = true;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        soundLength = alertSound.length;
        timer = cd;
    }

    /*
     *  max 1 - 20
     * 
     * 
     *  min 0 - 0.5
     */
    private float RegulateVolume(float vr)
    {
        float volume = 0;

        volume = vr / 20; // 0 - 1

        volume -= 1; //0.2 - 0.8

        volume = Mathf.Abs(volume);

        return volume;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("BlindWall"))
        {
            Debug.Log("womp");
            audioSource.transform.position = other.ClosestPoint(transform.position);

            if (canPlay)
            {
                float volumeRegulator = Vector3.Distance(transform.position, audioSource.transform.position);
                //Debug.Log(volumeRegulator);
                audioSource.volume = RegulateVolume(volumeRegulator);
                audioSource.PlayOneShot(alertSound);
            }

            if (timer > time)
            {
                timer -= Time.deltaTime;
                if (canPlay)
                    canPlay = false;
            }
            else
            {
                Debug.Log("Timer < time");
                timer = cd;
                canPlay = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("BlindWall"))
        {
            Debug.Log("Unwomp:(");
            audioSource.transform.position = transform.position;
            timer = 0;
        }
    }


    private void PlayAlert(int place)
    {
        /*switch (place)
        {
            case 1:
                frontSource.PlayOneShot(alertSound);
                break;
            case 2:
                leftSource.PlayOneShot(alertSound);
                break;
            case 3:
                rightSource.PlayOneShot(alertSound);
                break;
            case 4:
                backSource.PlayOneShot(alertSound);
                break;
            default:
                return;
        }*/
    }
}
