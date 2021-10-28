using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlindGoalReached : MonoBehaviour
{
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip normalSound;
    [SerializeField] private AudioSource source;
    [SerializeField] private float victoryVol = 0.1f;

    void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
        source.clip = normalSound;
        source.loop = true;
        source.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("positive womp");

            source.Stop();
            source.PlayOneShot(winSound);
            source.loop = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        source.Stop();
        source.volume = victoryVol;
        source.PlayOneShot(winSound);
        source.loop = false;
    }
}
