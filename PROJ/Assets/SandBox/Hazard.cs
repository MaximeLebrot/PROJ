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
    [SerializeField] private bool row;
    [SerializeField] private bool column;
    [SerializeField] private List<bool> customPattern = new List<bool>();

    private bool[,] hazardMatrix = new bool[5, 5]; 

    private void OnEnable()
    {
        EventHandler<ResetHazardEvent>.RegisterListener(OnResetHazard);      
    }
    private void OnDisable()
    {
        // EventHandler<UpdateHazardEvent>.UnregisterListener(OnUpdateHazard);
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
    private void HazardSetup()
    {
        hazardMatrix[0, 1] = true;
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
