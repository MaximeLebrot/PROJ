using UnityEngine;

public class Breadcrumb : MonoBehaviour
{
    public Breadloaf parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parent.UpdateCrumbStep(this);
        }
    }
}
