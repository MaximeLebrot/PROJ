using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSolution1 : MonoBehaviour
{
    public GameObject[] objects;
    public int i;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (i >= objects.Length)
            {
                Debug.Log("Finished");
                return;
            }

            objects[i].SetActive(true);
            i++;
        }
    }

   
}
