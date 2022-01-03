using UnityEngine;

public abstract class Page : MonoBehaviour
{
    public abstract string GetPageType();

    public void Activate() => gameObject.SetActive(true);

    public void Inactivate() => gameObject.SetActive(false);
}
