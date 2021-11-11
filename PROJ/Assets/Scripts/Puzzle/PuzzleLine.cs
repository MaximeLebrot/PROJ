using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PuzzleLine : MonoBehaviour
{
    [SerializeField] private VisualEffect lineParticle;
    [SerializeField] private Animator anim;


    public void SetPosition(Vector3 pos, Quaternion puzzleRot)
    {
        pos = puzzleRot * pos;
        lineParticle.SetVector3("EndPos", pos);
    }
    public void SetPosition(Vector3 pos)
    {
        lineParticle.SetVector3("EndPos", pos);
    }

    public void TurnOffLine()
    {
        //animate something that calls on Stop
        anim.SetTrigger("off");
        //Invoke("Stop", 1.5f);
    }

    public void Stop()
    {
        lineParticle.Stop();
    }

    public void Play()
    {
        lineParticle.Play();
    }
}
