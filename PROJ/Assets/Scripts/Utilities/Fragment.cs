using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    [SerializeField] private string nameOfFragment;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerFragments>().AddFragment(nameOfFragment);
        //Start a cutscene or whatevs?
    }

}
