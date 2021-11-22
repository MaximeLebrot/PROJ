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

    private void OnTriggerEnter(Collider other)
    {
        //Player has Stepped on Hazard

        //va h�nder om spelaren st�r kvar p� hazard? 
        //vi kan inte reset varje g�ng. Vi m�ste typ kolla om pusslet �r aktivt eller om det h�ller p� att starta om typ?

        //EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(310), false));
    }
}
