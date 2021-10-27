using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSolution : MonoBehaviour
{
    private InputMaster inputMaster;
    private Animator anim;
    public GameObject[] objects;
    private int i;
    void Awake()
    {
       anim = GetComponent<Animator>();
       inputMaster = new InputMaster();
    }
    
    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }
    
    private void PlayAnimation()
    {
        anim.SetBool("Solved", true);

    }
    
    // Update is called once per frame
    void Update()
    {
        if (inputMaster.PuzzleDEBUGGER.ShowSolution.triggered)
        {
            if (i < objects.Length)
            {
                Debug.Log(i);

                objects[i].SetActive(true);
                i = i + 1;
                

            } else if (i==objects.Length)
            {
               foreach(GameObject obj in objects) {
                    obj.SetActive(false);
                }
                i = 0;
            }
        }
    }

   
}
