using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Logbook : MonoBehaviour
{
    [SerializeField] private GameObject[] chapters; // The start page for each chapter
    [SerializeField] private GameObject[] pages; // All pages
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private GameObject[] tabsLeft; //Don't include Tab_0
    [SerializeField] private GameObject[] tabsRight; //Don't include Tab_0
    private AudioSource audioSource;
    private int pageNr;
    [SerializeField] private GameObject leftTurnButton;
    [SerializeField] private GameObject rightTurnButton;
    [SerializeField] private TextMeshProUGUI pageNrText;

    public void Start()
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

    public void AddPage(GameObject newPage)
    {
        GameObject[] p = new GameObject[pages.Length + 1];
        for (int i = 0; i < pages.Length; i++)
            p[i] = pages[i];
        p[pages.Length] = newPage;
        pages = p;
        rightTurnButton.SetActive(true);
    }

    private void PlaySound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
