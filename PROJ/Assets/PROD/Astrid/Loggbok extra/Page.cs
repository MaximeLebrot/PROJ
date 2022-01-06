using UnityEngine;

public abstract class Page : MonoBehaviour
{
    public abstract string GetPageType();

    [SerializeField] private bool twoSided;
    [SerializeField] private GameObject rightPage;

    private void Start()
    {
        InitiatePage();
    }

    public void InitiatePage()
    {
        if (twoSided)
        {
            if (rightPage != null)
                rightPage.SetActive(false);
        }
    }

    public void AddRightSide()
    {
        rightPage.SetActive(true);
    }

    public void Activate() => gameObject.SetActive(true);

    public void Inactivate() => gameObject.SetActive(false);
}
