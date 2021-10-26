using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HearCollision : MonoBehaviour
{
    [SerializeField] private AudioSource frontSource;
    [SerializeField] private AudioSource leftSource;
    [SerializeField] private AudioSource rightSource;
    [SerializeField] private AudioSource backSource;

    [SerializeField] private AudioClip alertSound;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
}
