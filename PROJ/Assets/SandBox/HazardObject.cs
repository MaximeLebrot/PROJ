using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObject : MonoBehaviour
{




    //private Animator animator;
    public bool movingBackwards;

    public Vector3 StartPos { get; set; }

    

    private void Awake()
    {

        //animator = GetComponent<Animator>();    
    }
    
    public void ResetHazardObject()
    {
        movingBackwards = false;
        transform.position = StartPos;
    }

    public void UpdateHazard(float hazardOffset, Vector3 moveDirection)
    {
        if(movingBackwards == false)
            transform.parent.position += hazardOffset * moveDirection;
        else
            transform.parent.position -= hazardOffset * moveDirection;
    }

    public void ReverseHazard(float hazardOffset, Vector3 moveDirection)
    {
        if (movingBackwards == true)
            transform.parent.position += hazardOffset * moveDirection;
        else
            transform.parent.position -= hazardOffset * moveDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player has Stepped on Hazard

        //va h�nder om spelaren st�r kvar p� hazard? 
        //vi kan inte reset varje g�ng. Vi m�ste typ kolla om pusslet �r aktivt eller om det h�ller p� att starta om typ?

        //EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(310), false));
    }

    public void TurnAround()
    {
        //Debug.Log("TURN AROUND");
        movingBackwards = !movingBackwards;
    }

    public void CheckHazardBounds(int boundsMax, Vector3 moveDirection, float hazardOffset)
    {
        //Check if this reached the bounds. if so: movingBackwards = true

        Vector3 vec;
        if (movingBackwards)
            vec = transform.parent.localPosition + (-moveDirection * hazardOffset);
        else
            vec = transform.parent.localPosition + (moveDirection * hazardOffset);



        if (Mathf.Round(vec.x) > boundsMax || Mathf.Round(vec.z) > boundsMax)
        {
            TurnAround();
            return;
        }


        if (Mathf.Round(vec.x) < -boundsMax || Mathf.Round(vec.z) < -boundsMax)
        {
            TurnAround();
        }

    }

}
