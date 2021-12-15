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
            Debug.Log("Fragment");
            FragmentFollow frag = GameObject.FindGameObjectWithTag("Fragment").GetComponent<FragmentFollow>();
            frag.DepositFragment(this);
            portal.Open();
        }
    }

    public void InitializeFragment()
    {

        portal.Open();

        //VARF�R VILL JAG INSTANTIERA DESSA? WTF HAR JAG T�NKT?
        /*
        Debug.Log("Init");
        GameObject instance = Instantiate(fragmentObject, transform);
        instance.transform.position = fragmentPos.position;
        instance.transform.rotation = fragmentPos.rotation;
        instance.GetComponent<Animator>().SetTrigger("activate");
        */
    }

}
