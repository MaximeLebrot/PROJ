using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BlindGoalReached : MonoBehaviour
{
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip normalSound;
    [SerializeField] private AudioSource source;

    private SphereCollider collider;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = normalSound;
        source.loop = true;
        source.Play();

        collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        source.Stop();
        source.PlayOneShot(winSound);
        source.loop = false;
    }
}
