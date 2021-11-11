using UnityEngine;

public abstract class Page : MonoBehaviour
{
    public int pageNumber;

    public abstract void SetPageSelected();
    public abstract Chapter GetChapterParent();
}
