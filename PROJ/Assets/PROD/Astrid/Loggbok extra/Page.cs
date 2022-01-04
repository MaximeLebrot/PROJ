using UnityEngine;

public abstract class Page : MonoBehaviour
{
    public abstract string GetPageType();

    [SerializeField] private bool twoSided;
    [SerializeField] public bool complete;
    [SerializeField] private GameObject rightPage;

    private void Awake()
    {
        if (twoSided)
        {
            if (rightPage != null)
                rightPage.SetActive(false);
        }
        else
            complete = true;
    }

    public void AddRightSide()
    {
        complete = true;
        rightPage.SetActive(true);
    }
    public void Activate() => gameObject.SetActive(true);

    public void Inactivate() => gameObject.SetActive(false);
}
