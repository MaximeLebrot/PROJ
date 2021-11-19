using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObject : MonoBehaviour
{
    [SerializeField] public int startingState = 0;
    [SerializeField] public float timeToNextState = 2f;


    private float timer;
    private Animator animator;
    private int counter;
    private void Awake()
    {
        timer = timeToNextState;
        animator = GetComponent<Animator>();
        counter = startingState;
        animator.SetInteger("stateCounter", counter);      
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            NextState();
            timer = timeToNextState;
        }
    }
    public void ResetHazardObject()
    {
        timer = timeToNextState;
        counter = startingState;
        animator.SetInteger("stateCounter", counter);
        animator.SetTrigger("resetTrigger");
    }

    public void OnUpdateHazard()
    {
        //NextState();
    }
    private void NextState()
    {
        counter++;
        if (counter > 2)
            counter = 0;

        animator.SetInteger("stateCounter", counter);
    }    
}
