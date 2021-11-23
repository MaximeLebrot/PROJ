using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private GameObject hazardObj;
    [SerializeField] private PuzzleGrid grid; 

    [SerializeField] private float baseTimer;
    [SerializeField] private float timerOffsetPerObject;
    [SerializeField] private int startingState;
    [SerializeField] private int stateOffsetPerObject;
    [SerializeField] private List<HazardObject> hazardObjects = new List<HazardObject>();

    //Hazard Setup
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private List<bool> customPattern = new List<bool>();
    [SerializeField] private bool movingHazard;
    [SerializeField] private bool moveX;

    private bool[,] hazardMatrix = new bool[5, 5]; 

    private void OnEnable()
    {
        
        foreach(bool h in customPattern)
            Debug.Log(h);
        
        EventHandler<UpdateHazardEvent>.RegisterListener(OnUpdateHazard);
        EventHandler<ResetHazardEvent>.RegisterListener(OnResetHazard);      
    }
    private void OnDisable()
    {
        EventHandler<UpdateHazardEvent>.UnregisterListener(OnUpdateHazard);
        EventHandler<ResetHazardEvent>.UnregisterListener(OnResetHazard);
    }
    private void Start()
    {
        HazardSetup();
        InitializeHazardObjects();
    }
    private void OnResetHazard(ResetHazardEvent eve)
    {
        foreach (HazardObject ho in hazardObjects)
        {
            ho.ResetHazardObject();
        }
        //Reset etc
    }
    private void OnUpdateHazard(UpdateHazardEvent eve)
    {
        //Maybe also cache start position to reset it in OnResetHazard
        //Or, instead, we could just delete it and instantiate the hazards from next puzzleInstance

        //Something here to decide if the offset relates to y or x-axis, or both
        Vector3 offsetVector;
        if (moveX)
            offsetVector = Vector3.right;
        else
            offsetVector = -Vector3.forward;
        if (movingHazard)
        {
            transform.position += grid.nodeOffset * offsetVector;
            return;
        }
        Debug.Log("OnUpdateHazard event recieved");
        foreach (HazardObject ho in hazardObjects)
        {
            ho.NextState();
        }
    }
    private void HazardSetup()
    {
        /*hazardMatrix[0, 1] = true;
          hazardMatrix[0, 2] = true;
          hazardMatrix[0, 3] = true;
          hazardMatrix[0, 4] = true;

          for (int x = 0; x < 5; x++)
          {
              for (int y = 0; y < 5; y++)
              {
                  if (hazardMatrix[x, y] == true)
                  {
                      GameObject hazardObject = Instantiate(hazardObj, transform);
                      hazardObjects.Add(hazardObject.GetComponentInChildren<HazardObject>());
                      //GetCorresponding node position
                      hazardObject.transform.position = grid.allNodes[x, y].transform.position;
                      Debug.Log(x + ", " + y + " node is at position: "+  grid.allNodes[x, y].transform.position);

                  }

              }
          }*/
        foreach (HazardObject ho in GetComponentsInChildren<HazardObject>())
        {
            hazardObjects.Add(ho);
        }
    }
    private void InitializeHazardObjects()
    {
        int hazardObjectCounter = 0; 
        foreach (HazardObject ho in hazardObjects)
        {
            ho.startingState = startingState + (hazardObjectCounter * stateOffsetPerObject);
            ho.timeToNextState = baseTimer + (hazardObjectCounter * timerOffsetPerObject);
            hazardObjectCounter++;
        }
    }


}
