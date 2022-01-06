using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Logbook : MonoBehaviour
{
    [SerializeField] private CompletedBook completeBook;
    [SerializeField] private StartPage startPage;
    [SerializeField] private List<SymbolPage> symbolPages;
    [SerializeField] private List<OperandPage> operandPages;
    [SerializeField] private List<MetaOperandPage> metaOperandPages;
    [SerializeField] private List<Page> allPages;

    [SerializeField] private GameObject[] chapters; // The start page for each chapter
    [SerializeField] private GameObject[] pages; // All pages
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private GameObject[] tabsLeft; //Don't include Tab_0
    [SerializeField] private GameObject[] tabsRight; //Don't include Tab_0
    private AudioSource audioSource;
    [SerializeField] private int pageNr;
    [SerializeField] private GameObject leftTurnButton;
    [SerializeField] private GameObject rightTurnButton;
    [SerializeField] private TextMeshProUGUI pageNrText;

    [SerializeField] private Animator notificationAnim;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        AddChapters();
        leftTurnButton.SetActive(false);
        rightTurnButton.SetActive(true);
        for (int i = 0; i < tabsLeft.Length; i++)
            CloseTab(i);
        CloseEveryPage();
        allPages[pageNr].Activate();
    }

    private void CloseEveryPage()
    {
        foreach (Page page in allPages)
            page.Inactivate();
    }

    public void FlipPage(bool forward)
    {
        if (forward)
        {
            if (pageNr == allPages.Count - 1)
                return;
            pageNr++;
            PageActivation();
        }
        else
        {
            if (pageNr == 0)
                return;
            pageNr--;
            PageActivation();
        }
    }
    
    public void OpenPage(Page page)
    {
        for (int i = 0; i < allPages.Count; i++)
        {
            if (allPages[i] == page)
                pageNr = i;
        }
        PageActivation();
    }

    private void PageActivation()
    {
        CloseEveryPage();
        allPages[pageNr].Activate();
        pageNrText.text = "Page " + (pageNr + 1);
        leftTurnButton.SetActive(true);
        rightTurnButton.SetActive(true);
        if (pageNr == allPages.Count- 1)
            rightTurnButton.SetActive(false);
        else if (pageNr == 0)
            leftTurnButton.SetActive(false);
        ChapCheck();
    }

    #region old
    /*public void awake()
    {
        audioSource = GetComponent<AudioSource>();
        pageNr = 0; // Or should it be 0?
        leftTurnButton.SetActive(false);
        for (int i = 0; i < tabsLeft.Length; i++)
            CloseTab(i);
        CloseEverything();
        if (pages[pageNr].activeInHierarchy == false)
            pages[0].SetActive(true);
    }

    // <-- brain stupid and don't understand left vs right
    public void TurnPageLeft()
    {
        if (pageNr == 0)
            return;
        pageNr--;
        CloseEverything();
        pages[pageNr].SetActive(true);
        if (rightTurnButton.activeInHierarchy == false)
            rightTurnButton.SetActive(true);
        pageNrText.text = "Page " + (pageNr + 1);
        if (pageNr == 0)
        {
            // Deactivate the button
            if (leftTurnButton.activeInHierarchy == true)
                leftTurnButton.SetActive(false);
        }
        ChapterCheck();
    }

    // -->
    public void TurnPageRight()
    {
        if (pageNr == pages.Length - 1)
            return;
        pageNr++;
        CloseEverything();
        pages[pageNr].SetActive(true);
        if (leftTurnButton.activeInHierarchy == false)
            leftTurnButton.SetActive(true);
        pageNrText.text = "Page " + (pageNr + 1);
        if (pageNr == pages.Length - 1)
        {
            // Deactivate the button
            if (rightTurnButton.activeInHierarchy == true)
                rightTurnButton.SetActive(false);
        }
        ChapterCheck();
    }*/

    private void ChapCheck()
    {
        if (pageNr > symbolPages.Count + operandPages.Count)
            OpenThirdTab();
        else if (pageNr > symbolPages.Count)
            OpenSecondTab();
        else if (pageNr > 0)
            OpenFirstTab();
        else
            OpenWelcomeTab();
    }

    // Do something about disssss :'))))))))))))
    private void ChapterCheck()
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            if (chapters[i].activeInHierarchy == true)
            {
                switch (i)
                {
                    case 0:
                        OpenWelcomeTab();
                        break;
                    case 1:
                        OpenFirstTab();
                        break;
                    case 2:
                        OpenSecondTab();
                        break;
                    case 3:
                        OpenThirdTab();
                        break;
                }

            }
        }
    }

    public void OpenSpecificPage(GameObject page)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] == page)
            {
                pageNr = i;
            }
        }
        CloseEverything();
        pages[pageNr].SetActive(true);
        pageNrText.text = "Page " + (pageNr + 1);
        
        // Fix the button stuffs
        if (pageNr == pages.Length - 1)
        {
            // Deactivate the button
            if (rightTurnButton.activeInHierarchy == true)
                rightTurnButton.SetActive(false);
        } else
        {
            if (leftTurnButton.activeInHierarchy == false)
                leftTurnButton.SetActive(true);
        }
        if (pageNr == 0)
        {
            // Deactivate the button
            if (leftTurnButton.activeInHierarchy == true)
                leftTurnButton.SetActive(false);
        } else
        {
            if (rightTurnButton.activeInHierarchy == false)
                rightTurnButton.SetActive(true);
        }
        ChapterCheck();
    }

    public void OpenWelcomeTab()
    {
        for (int i = 0; i < tabsRight.Length; i++)
            CloseTab(i);
        PlaySound();
    }

    public void OpenFirstTab()
    {
        for (int i = 1; i < tabsRight.Length; i++)
            CloseTab(i);
        OpenTab(0);
        PlaySound();
    }

    public void OpenSecondTab()
    {
        for (int i = 2; i < tabsRight.Length; i++)
            CloseTab(i);
        for (int i = 0; i < tabsRight.Length; i++)
        {
            if (i <= 1)
                OpenTab(i);
        }
        PlaySound();
    }

    public void OpenThirdTab()
    {
        for (int i = 3; i < tabsRight.Length; i++)
            CloseTab(i);
        for (int i = 0; i < tabsRight.Length; i++)
        {
            if (i <= 2)
                OpenTab(i);
        }
        OpenTab(2);
        PlaySound();
    }

    private void CloseEverything()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i].activeInHierarchy != false)
                pages[i].SetActive(false);
        }
    }

    // Needs to deactivate the tab on the left and active the tab on the right side.
    private void OpenTab(int i)
    {
        if (tabsRight[i].activeInHierarchy != false)
            tabsRight[i].SetActive(false);
        if (tabsLeft[i].activeInHierarchy != true)
            tabsLeft[i].SetActive(true);
    }

    // Needs to deactivate the tab on the right and active the tab on the left side.
    private void CloseTab(int i)
    {
        if (tabsLeft[i].activeInHierarchy != false)
            tabsLeft[i].SetActive(false);
        if (tabsRight[i].activeInHierarchy != true)
            tabsRight[i].SetActive(true);
    }
    #endregion
    
    private void UpdateAllPages()
    {
        allPages = new List<Page>();
        allPages.Add(startPage);
        allPages.AddRange(symbolPages);
        allPages.AddRange(operandPages);
        allPages.AddRange(metaOperandPages);
    }

    private void AddChapters()
    {
        symbolPages.Add(completeBook.symbolsChapter);
        operandPages.Add(completeBook.OperandChapter);
        metaOperandPages.Add(completeBook.MetaOperandChapter);
        UpdateAllPages();
    }

    public void AddNextPage() => AddPage(completeBook.GetNextPage());

    public void AddSpecificPage(Page page) => AddPage(page);

    private void AddPage(Page newPage)
    {
        if (newPage == null)
            return;
        TriggerNotificationAnimation();
        switch (newPage.GetPageType())
        {
            case "Start":
                startPage = (StartPage)newPage;
                break;
            case "Symbol":
                symbolPages.Add((SymbolPage)newPage);
                break;
            case "Operand":
                operandPages.Add((OperandPage)newPage);
                break;
            case "MetaOperand":
                metaOperandPages.Add((MetaOperandPage)newPage);
                break;
        }

        UpdateAllPages();
        UpdatePageNr(newPage);
        PageActivation();
    }

    private void TriggerNotificationAnimation()
    {
        notificationAnim.SetTrigger("Notify");
    }

    public void AddRightSide(Page page)
    {
        TriggerNotificationAnimation();
        page.AddRightSide();
        OpenPage(page);
    }

    public void AddRightSide(string pageName)
    {
        Page page = FindPageByName(pageName);
        TriggerNotificationAnimation();
        page.AddRightSide();
        OpenPage(page);
    }

    private Page FindPageByName(string name)
    {
        Page p = null;
        foreach (Page page in allPages)
        {
            if (page.name.Equals(name))
                p = page;
        }
        return p;
    }

    private Page GetLatestTwoSidedPage()
    {
        Page page = null;

        foreach (Page p in allPages)
        {
            if (!p.complete)
                page = p;
        }

        return page;
    }

    private void UpdatePageNr(Page page)
    {
        for (int i = 0; i < allPages.Count; i++)
        {
            if (allPages[i] == page)
                pageNr = i;
        }
    }

    private void PlaySound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
