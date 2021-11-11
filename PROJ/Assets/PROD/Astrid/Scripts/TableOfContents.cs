using UnityEngine;

public class TableOfContents : Page
{
    //public GameObject leftTab;
    public override Chapter GetChapterParent() { return null; }

    public override void SetPageSelected()
    {
        gameObject.SetActive(true);
    }
}
