using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject menu;
     private Animator anim;


    private void Update()
    {
        PressAnyKey();
    }

    public void PressAnyKey()
    {
        if (Input.anyKeyDown)

        {
            anim.SetTrigger("KeyPress");
            //Application.Load
            menu.SetActive(true);
        }


    }
   void Start()
    {
        anim = GetComponent<Animator>();
    }

}
