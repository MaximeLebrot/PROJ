using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSolution : MonoBehaviour
{
    private InputMaster inputMaster;
    private Animator anim;
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
    // Update is called once per frame
    void Update()
    {
        if (inputMaster.PuzzleDEBUGGER.ShowSolution.triggered)
        {
            anim.SetBool("Solved", true);
        }
    }
}
