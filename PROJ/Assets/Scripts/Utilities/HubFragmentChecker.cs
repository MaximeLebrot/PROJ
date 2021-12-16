using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This goes in MainTutorialHubScene

public class HubFragmentChecker : MonoBehaviour
{

    /*[SerializeField] private */public Transform earthPos;
    /*[SerializeField] private */public Transform windPos;
    /*[SerializeField] private */public Transform lavaPos;
    /*[SerializeField] private */public Transform startPos;

    [SerializeField] private List<FragmentDeposit> deposits = new List<FragmentDeposit>();

    private void Start()
    {
        CheckForPortalsToOpen();
    }

    private void CheckForPortalsToOpen()
    {
        PlayerFragments player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFragments>();

        Debug.Log(player.CheckForFragment("lava") + " yee maddafakka");

        if (player.CheckForFragment("lava"))
        {
            deposits[2].InitializeFragment();
            deposits[1].InitializeFragment();
            deposits[0].InitializeFragment();
            player.transform.position = windPos.position;
            player.transform.rotation = windPos.rotation;
        }
        else if (player.CheckForFragment("wind"))
        {
            deposits[1].InitializeFragment();
            deposits[0].InitializeFragment();
            player.transform.position = earthPos.position;
            player.transform.rotation = earthPos.rotation;
        }
        else if (player.CheckForFragment("earth"))
        {
            deposits[0].InitializeFragment();
            player.transform.position = startPos.position;
            player.transform.rotation = startPos.rotation;
        }
        else
        {
            player.transform.position = startPos.position;
            player.transform.rotation = startPos.rotation;
        }

        GameObject solver = GameObject.FindGameObjectWithTag("Solver");
        solver.SetActive(false);
        solver.SetActive(true);
    }
}
