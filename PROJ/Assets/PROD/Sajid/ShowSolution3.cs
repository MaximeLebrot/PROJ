using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSolution3 : MonoBehaviour
{
    public GameObject[] objects;
    public int i;
    private float time, timer;

    public void Start()
    {
        time = 0;
    }
    public void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (timer <= time)
            {
                if (i >= objects.Length)
                {
                    Debug.Log("Finished");
                    return;
                }
                else
                {
                    objects[i].SetActive(true);
                    i++;
                    timer = 1;
                }

            }
            else
            {
                timer -= Time.deltaTime;
                Debug.Log(timer);
            }
        }
    }


}
