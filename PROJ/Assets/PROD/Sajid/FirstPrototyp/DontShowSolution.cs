using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontShowSolution : MonoBehaviour
{
    [SerializeField] private ShowSolution1 showSolution;
    [SerializeField] private ShowSolution3 showSolution2;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject go in showSolution.objects) 
            {
                go.SetActive(false);
            }

            foreach (GameObject go in showSolution2.objects)
            {
                go.SetActive(false);
            }

            showSolution2.i = 0;
            showSolution.i = 0;
        }
    }

   
}
