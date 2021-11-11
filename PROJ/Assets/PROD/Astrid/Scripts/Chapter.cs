using UnityEngine;

public class Chapter : Page
{
    public int chapterNumber;
    public GameObject leftTab;
    public GameObject rightTab;
    public SubPage[] pages;
    public GameObject chapterParent;

    private void Awake()
    {
        if (pages.Length == 0)
        {
            pages = GetComponentsInChildren<SubPage>();
        }
        foreach (SubPage page in pages)
        {
            page.gameObject.SetActive(false);
            page.chapterParent = this;
        }
    }

    public void SwapToPage(int pageNumber)
    {

    }

    public void HaveFlippedPastChapter(bool yes)
    {
        leftTab.SetActive(yes);
        rightTab.SetActive(!yes);
    }

    public void HandleChapterSelection(Chapter chapter)
    {
        if (chapterNumber == chapter.chapterNumber)
            chapterParent.SetActive(true);
        if (chapterNumber <= chapter.chapterNumber)
            HaveFlippedPastChapter(true);
        else
            HaveFlippedPastChapter(false);
        if (chapterNumber != chapter.chapterNumber)
            chapterParent.SetActive(false);
    }

    public void CloseChapter()
    {
        leftTab.SetActive(false);
        rightTab.SetActive(true);
        chapterParent.SetActive(false);
    }

    public void SetPageValues(int chapNumber, int pageNumber)
    {
        this.chapterNumber = chapNumber;
        this.pageNumber = pageNumber;
        for (int i = 0; i < pages.Length; i++)
            pages[i].pageNumber = pageNumber + i+1;
        Debug.Log("Chapter: " + this.name + " chapter number = " + chapNumber + ", page number = " + pageNumber);
        foreach (Page page in pages)
            Debug.Log("Page: " + page.name + " page number = " + page.pageNumber);
    }

    public override Chapter GetChapterParent() { return this; }

    public override void SetPageSelected()
    {
        gameObject.SetActive(true);
        foreach (SubPage page in pages)
            page.gameObject.SetActive(false);
        chapterParent.SetActive(true);
    }
}
