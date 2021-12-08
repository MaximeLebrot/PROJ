using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This goes in MainTutorialHubScene

public class HubFragmentChecker : MonoBehaviour
{

    [SerializeField] private Transform earthPos;
    [SerializeField] private Transform windPos;
    [SerializeField] private Transform lavaPos;
    [SerializeField] private Transform startPos;

    [SerializeField] private List<FragmentDeposit> deposits = new List<FragmentDeposit>();

    private void Awake()
    {
        CheckForPortalsToOpen();
    }



    private void CheckForPortalsToOpen()
    {
        PlayerFragments fragment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFragments>();


        if (fragment.DepositFragment("lava"))
        {
            deposits[2].InitializeFragment();
            deposits[1].InitializeFragment();
            deposits[0].InitializeFragment();
            fragment.transform.position = earthPos.position;
            fragment.transform.rotation = earthPos.rotation;
        }

        else if (fragment.DepositFragment("wind"))
        {
            deposits[1].InitializeFragment();
            deposits[0].InitializeFragment();
            fragment.transform.position = windPos.position;
            fragment.transform.rotation = windPos.rotation;
        }

        else if (fragment.DepositFragment("earth"))
        {
            deposits[0].InitializeFragment();
            fragment.transform.position = earthPos.position;
            fragment.transform.rotation = earthPos.rotation;
        }

        else
        {
            fragment.transform.position = startPos.position;
            fragment.transform.rotation = startPos.rotation;
        }
            

    }


}
