using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    [SerializeField] private string portalThisFragmentOpens;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerFragments>().AddFragment(portalThisFragmentOpens);
        //Start a cutscene or whatevs?
    }

}
