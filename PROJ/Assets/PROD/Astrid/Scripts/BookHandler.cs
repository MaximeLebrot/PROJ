using System.Collections.Generic;
using UnityEngine;

public class BookHandler : MonoBehaviour
{
    [SerializeField] private Chapter[] chapters;
    [SerializeField] private List<Page> allPages = new List<Page>();
    [SerializeField] private Dictionary<Chapter, Page[]> logbook = new Dictionary<Chapter, Page[]>();
    [SerializeField] private Chapter currentChapter;
    [SerializeField] private int currentPage;

    private TableOfContents tableOfContents;

    private AudioSource audioSource;

    private void Start()
    {
        InitializeValues();
        int chapNumber = 1;
        int pageOffset = 0;
        allPages.Add(tableOfContents);
        foreach (Chapter chapter in chapters)
        {
            allPages.Add(chapter);
            foreach (Page page in chapter.pages)
                allPages.Add(page);
            chapter.SetPageValues(chapNumber, pageOffset + chapNumber);
            chapNumber++;
            logbook.Add(chapter, chapter.pages);
            chapter.gameObject.SetActive(false);
            pageOffset += chapter.pages.Length;
        }
    }

    public void OpenTableOfContents()
    {
        tableOfContents.gameObject.SetActive(true);
        CloseAllChapters();
    }

    private void HideTableOfContents()
    {
        tableOfContents.gameObject.SetActive(false);
    }

    public void ChangeToChapter(Chapter chapter)
    {
        HideTableOfContents();
        currentPage = chapter.pageNumber;
        currentChapter = chapter;
        foreach (Chapter chap in logbook.Keys)
        {
            chap.HandleChapterSelection(chapter);
        }
        allPages[currentPage].SetPageSelected();
    }

    public void ChangeToPage(Page page)
    {
        foreach (Page p in allPages)
            p.gameObject.SetActive(false);
        currentPage = page.pageNumber;
        currentChapter = page.GetChapterParent();
        currentChapter.gameObject.SetActive(true);
        if (allPages.Contains(page))
        {
            allPages[currentPage].SetPageSelected();
        }

    }

    private void CloseAllChapters()
    {
        foreach (Chapter chapter in logbook.Keys)
        {
            chapter.CloseChapter();
        }
    }

    private Chapter GetChapterFromPage(int page)
    {
        Chapter chapter = null;
       
        foreach (Chapter chap in logbook.Keys)
        {
            if (chap.pageNumber <= page)
                chapter = chap;
        }
        return chapter;
    }

    public void FlipPage(bool forward)
    {
        if (forward)
        {
            if (currentPage+1 == allPages.Count)
                return;
            currentPage++;

            foreach (Chapter chap in logbook.Keys)
                chap.HandleChapterSelection(GetChapterFromPage(currentPage));

            foreach (Page page in allPages)
                page.gameObject.SetActive(false);

            if (allPages[currentPage].GetChapterParent() != null)
                currentChapter = allPages[currentPage].GetChapterParent();
            currentChapter.gameObject.SetActive(true);
            currentChapter.chapterParent.SetActive(false);
            //allPages[currentPage].gameObject.SetActive(true);
            allPages[currentPage].SetPageSelected();
        }
        else
        {
            if (currentPage == 0)
                return;
            currentPage--;
            if (currentPage == 0)
            {
                OpenTableOfContents();
                return;
            }

            foreach (Chapter chap in logbook.Keys)
                chap.HandleChapterSelection(GetChapterFromPage(currentPage));
            foreach (Page page in allPages)
                page.gameObject.SetActive(false);
            if (allPages[currentPage].GetChapterParent() != null)
                currentChapter = allPages[currentPage].GetChapterParent();
            currentChapter.gameObject.SetActive(true);
            currentChapter.chapterParent.SetActive(false);
            //allPages[currentPage].gameObject.SetActive(true);
            allPages[currentPage].SetPageSelected();
        }
    }

    private void InitializeValues()
    {
        if (chapters.Length == 0)
            chapters = GetComponentsInChildren<Chapter>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (tableOfContents == null)
            tableOfContents = GetComponentInChildren<TableOfContents>();
    }
}
