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

    private void Start()
    {
        CheckForPortalsToOpen();
    }

    private void CheckForPortalsToOpen()
    {
        PlayerFragments player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFragments>();


        if (player.DepositFragment("lava"))
        {
            deposits[2].InitializeFragment();
            deposits[1].InitializeFragment();
            deposits[0].InitializeFragment();
            player.transform.position = lavaPos.position;
            player.transform.rotation = lavaPos.rotation;
        }
        else if (player.DepositFragment("wind"))
        {
            deposits[1].InitializeFragment();
            deposits[0].InitializeFragment();
            player.transform.position = windPos.position;
            player.transform.rotation = windPos.rotation;
        }
        else if (player.DepositFragment("earth"))
        {
            deposits[0].InitializeFragment();
            player.transform.position = earthPos.position;
            player.transform.rotation = earthPos.rotation;
        }
        else
        {
            player.transform.position = startPos.position;
            player.transform.rotation = startPos.rotation;
        } 
    }
}
