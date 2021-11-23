using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private GameObject hazardObj;
    [SerializeField] private PuzzleGrid grid;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private bool movingHazard;
    [SerializeField] private bool diagonal;

    [SerializeField] private List<HazardObject> hazardObjects = new List<HazardObject>();

    //Hazard Setup

    [SerializeField] private List<bool> customPattern = new List<bool>();

    private int hazardBoundsMIN;
    private int hazardBoundsMAX;
    private bool[,] hazardMatrix = new bool[5, 5];
    private float hazardOffset;

    private void OnEnable()
    {
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
        if (movingHazard)
        {
            if (eve.reverse == false)
            {
                foreach (HazardObject ho in hazardObjects)
                {
                    ho.CheckHazardBounds(hazardBoundsMAX, moveDirection, hazardOffset);
                    ho.UpdateHazard(hazardOffset, moveDirection);
                }
            }
            else
            {
                foreach (HazardObject ho in hazardObjects)
                {
                    ho.CheckHazardBounds(hazardBoundsMAX, moveDirection, hazardOffset);
                    ho.ReverseHazard(hazardOffset, moveDirection);
                }
            }
            
        }
        
    }

    

    private void HazardSetup()
    {

        hazardBoundsMAX = (grid.Size / 2) * grid.NodeOffset;

        //Debug.Log(hazardBoundsMAX + " :: " + hazardBoundsMIN);
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
        if (diagonal == false)
            hazardOffset = grid.NodeOffset;
        else
            hazardOffset = grid.NodeOffset * Mathf.Sqrt(2);

        int hazardObjectCounter = 0; 
        foreach (HazardObject ho in hazardObjects)
        {
            ho.StartPos = ho.transform.position;
            hazardObjectCounter++;
        }
    }


}
