using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PuzzleLine : MonoBehaviour
{
    [SerializeField] private VisualEffect lineParticle;


    public void SetPosition(Vector3 pos)
    {
        lineParticle.SetVector3("EndPos", pos);
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
