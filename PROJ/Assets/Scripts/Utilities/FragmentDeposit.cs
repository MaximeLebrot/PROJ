using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentDeposit : MonoBehaviour
{
    [SerializeField] private string nameOfThisFragment;



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerFragments>().DepositFragment(nameOfThisFragment))
        {
            //start relevant cutscene?
        }
    }

}
