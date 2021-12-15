using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentDeposit : MonoBehaviour
{
    [SerializeField] private string nameOfThisFragment;
    [SerializeField] private Portal portal;
    //[SerializeField] private GameObject fragmentObject;
    //[SerializeField] private Transform fragmentPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerFragments>().DepositFragment(nameOfThisFragment))
        {
            FragmentFollow frag = other.GetComponent<FragmentFollow>();
            frag.DepositFragment();
            //start relevant cutscene?
            portal.Open();
        }
    }

    public void InitializeFragment()
    {

        portal.Open();

        //VARFÖR VILL JAG INSTANTIERA DESSA? WTF HAR JAG TÄNKT?
        /*
        Debug.Log("Init");
        GameObject instance = Instantiate(fragmentObject, transform);
        instance.transform.position = fragmentPos.position;
        instance.transform.rotation = fragmentPos.rotation;
        instance.GetComponent<Animator>().SetTrigger("activate");
        */
    }

}
