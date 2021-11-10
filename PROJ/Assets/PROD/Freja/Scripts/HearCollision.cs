using System.Collections.Generic;
using UnityEngine;

public class HearCollision : MonoBehaviour
{
    [SerializeField] private List<Collider> nearbyWalls;
    [SerializeField] private AudioSource[] audioArray;
    private Dictionary<AudioSource, Collider> ETHandle = new Dictionary<AudioSource, Collider>();
    [SerializeField] private new Transform transform;
    [SerializeField] private AudioClip alertSound;

    public GameObject player;

    private float sphereRadius;

    float soundLength = 0f;

    float time = 0f;
    float timer;
    [SerializeField] float cd;

    bool canPlay = true;

    void Start()
    {
        if (transform == null)
            transform = GetComponent<Transform>();
        if (audioArray.Length == 0)
            audioArray = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource audio in audioArray)
            ETHandle.Add(audio, null);
       
        sphereRadius = GetComponent<SphereCollider>().radius;
        soundLength = alertSound.length;
        timer = cd;
    }

    private void Update()
    {
        foreach (AudioSource audio in ETHandle.Keys)
        {
            if (ETHandle[audio] != null)
                UpdateAudioSourcePosition(audio, ETHandle[audio]);
        }
    }

    private float RegulateVolume(float vr)
    {
        float volume = 0;

        volume = vr / sphereRadius; // 0 - 1

        volume -= 1; //0.2 - 0.8

        volume = Mathf.Abs(volume);

        return volume;
    }

    private void UpdateAudioSourcePosition(AudioSource audio, Collider wall)
    {
        audio.transform.position = wall.ClosestPoint(transform.position);
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource audio in ETHandle.Keys)
        {
            if (ETHandle[audio] == null)
                return audio;
        }
        return null;
    }

    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlindWall"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, other.ClosestPoint(transform.position) - transform.position, out hit, sphereRadius))
            {
                if (hit.collider == other)
                {
                    nearbyWalls.Add(other);
                    ETHandle[GetFreeAudioSource()] = other;
                }
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BlindWall"))
        {
            if (nearbyWalls.Contains(other))
            {
                nearbyWalls.Remove(other);
                AudioSource audios = null;
                foreach (AudioSource audio in ETHandle.Keys)
                {
                    if (ETHandle[audio] == other)
                        audios = audio;
                }
                ETHandle[audios] = null;
                audios.gameObject.transform.position = transform.position;
            }
        }
    }
    #endregion

    /* #region oldstuff
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


     //private void OnTriggerExit(Collider other)
     //{
     //    if (other.GetComponent<Collider>().CompareTag("BlindWall"))
     //    {
     //        Debug.Log("Unwomp:(");
     //        audioSource.transform.position = transform.position;
     //        timer = 0;
     //    }
     //}


     private void PlayAlert(int place)
     {
         switch (place)
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
         }
     }
    #endregion
     */

}
