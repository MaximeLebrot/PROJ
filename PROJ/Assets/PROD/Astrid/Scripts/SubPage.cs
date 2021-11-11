using UnityEngine;

public class SubPage : Page
{
    public Chapter chapterParent;
    public override Chapter GetChapterParent() { return chapterParent; }

    public override void SetPageSelected()
    {
        chapterParent.chapterParent.SetActive(false);
        gameObject.SetActive(true);
    }
}
