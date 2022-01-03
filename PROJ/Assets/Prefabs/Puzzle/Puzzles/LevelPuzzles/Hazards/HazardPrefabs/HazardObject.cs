using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObject : MonoBehaviour
{
   //private Animator animator;
    public Vector3 StartPos { get; set; }
    public int PuzzleID { get; internal set; }
    public bool movingBackwards;
    public float moveTime = 1.2f;
    private void Awake()
    {
        //animator = GetComponent<Animator>();    
    }

    public void ResetHazardObject()
    {
        StopAllCoroutines();
        movingBackwards = false;
        transform.parent.localPosition = StartPos;
    }

    public void UpdateHazard(float hazardOffset, Vector3 moveDirection)
    {
        if (movingBackwards == false)
            MoveHazard(transform.parent.position + hazardOffset * moveDirection);
        else
            MoveHazard(transform.parent.position - hazardOffset * moveDirection);
    }

    public void ReverseHazard(float hazardOffset, Vector3 moveDirection)
    {
        if (movingBackwards == true)
            MoveHazard(transform.parent.position + hazardOffset * moveDirection);
        else
            MoveHazard(transform.parent.position - hazardOffset * moveDirection);
    }
    private void MoveHazard(Vector3 targetPosition)
    {
        StopAllCoroutines();
        StartCoroutine(ExecuteMove(targetPosition));
    }

    private IEnumerator ExecuteMove(Vector3 targetPosition)
    {
        float time = 0f;
        while (time < moveTime)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, targetPosition, time * (1 / moveTime));
            time += Time.deltaTime;
            yield return null;
        }
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
    private void OnTriggerEnter(Collider other)
    {
        EventHandler<ResetPuzzleEvent>.FireEvent(new ResetPuzzleEvent(new PuzzleInfo(PuzzleID)));
    }
    private void TurnAround()
    {
        //Debug.Log("TURN AROUND");
        movingBackwards = !movingBackwards;
    }

}
