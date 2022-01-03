using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObiClothFix : MonoBehaviour
{
    private InputMaster inputMaster;
    private bool heavy;
    void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void Update()
    {
        if (inputMaster.Player.Cloth.triggered)
        {
            heavy = !heavy;
            if(heavy)
            {
                GetComponent<Obi.ObiSolver>().parameters.gravity = new Vector3(0, -50f, 0);
            } else
            {
                GetComponent<Obi.ObiSolver>().parameters.gravity = new Vector3(0, -9.82f, 0);
            }
           GetComponent<Obi.ObiSolver>().UpdateBackend();

        }

        if (inputMaster.Player.DestroyCloth.triggered)
        {
            GetComponent<Obi.ObiSolver>().enabled = !GetComponent<Obi.ObiSolver>().enabled;
        }
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }
  
}
