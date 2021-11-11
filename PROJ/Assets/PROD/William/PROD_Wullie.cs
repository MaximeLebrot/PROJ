using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROD_Wullie : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    private AudioSource source;
    private InputMaster master;

    private void Awake()
    {
        master = new InputMaster();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
    }

    private void OnEnable()
    {
        master.Enable();
    }

    private void OnDisable()
    {
        master.Disable();
    }

    private void CheckInput()
    {
        if (master.SymbolAudio.PlayOne.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[0]);
        }

        if (master.SymbolAudio.PlayTwo.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[1]);
        }

        if (master.SymbolAudio.PlayThree.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[2]);
        }

        if (master.SymbolAudio.PlayFour.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[3]);
        }

        if (master.SymbolAudio.PlayFive.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[4]);
        }

        if (master.SymbolAudio.PlaySix.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[5]);
        }

        if (master.SymbolAudio.PlaySeven.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[6]);
        }

        if (master.SymbolAudio.PlayEight.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[7]);
        }

        if (master.SymbolAudio.PlayNine.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[8]);
        }

        if (master.SymbolAudio.PlayTen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[9]);
        }

        if (master.SymbolAudio.PlayEleven.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[10]);
        }

        if (master.SymbolAudio.PlayTwelve.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[11]);
        }

        if (master.SymbolAudio.PlayThirteen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[12]);
        }

        if (master.SymbolAudio.PlayFourteen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[13]);
        }

        if (master.SymbolAudio.PlayFifteen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[14]);
        }

        if (master.SymbolAudio.PlaySixteen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[15]);
        }

        if (master.SymbolAudio.PlaySeventeen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[16]);
        }

        if (master.SymbolAudio.PlayEighteen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[17]);
        }

        if (master.SymbolAudio.PlayNineteen.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[18]);
        }

        if (master.SymbolAudio.PlayTwenty.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[19]);
        }

        if (master.SymbolAudio.PlayTwentyOne.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[20]);
        }

        if (master.SymbolAudio.PlayTwentyTwo.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[21]);
        }

        if (master.SymbolAudio.PlayTwentyThree.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[22]);
        }

        if (master.SymbolAudio.PlayTwentyFour.triggered && !source.isPlaying)
        {
            source.PlayOneShot(clips[23]);
        }
    }
}
