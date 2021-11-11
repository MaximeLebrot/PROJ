using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logbook : MonoBehaviour
{
    [SerializeField] private GameObject[] pages; // Will prob wanna categorize them later.
    [SerializeField] private GameObject[] tabsLeft; //Don't include Tab_0
    [SerializeField] private GameObject[] tabsRight; //Don't include Tab_0
    private AudioSource audioSource;
    private int iterator;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        iterator = 0;

        for (int i = 0; i < tabsLeft.Length; i++)
            CloseTab(i);
    }
    public void ChangePage(bool forward)
    {
        for (int i = 0; i < tabsRight.Length; i++)
            CloseTab(i);
        if (forward)
        {
            if (iterator < pages.Length)
            {
                pages[iterator].SetActive(false);
                iterator++;
                pages[iterator].SetActive(true);
            }
        }
        else
        {
            if (iterator > 0)
            {
                pages[iterator].SetActive(false);
                iterator--;
                pages[iterator].SetActive(true);
            }
        }
        for (int i = 0; i < tabsRight.Length; i++)
        {
            if (i <= iterator)
                OpenTab(i);
        }
    }

    //Add so it takes input from keys.

    public void OpenWelcomeTab()
    {
        for (int i = 0; i < tabsRight.Length; i++)
            CloseTab(i);
        CloseEverything();
        if (pages[0].activeInHierarchy != true)
            pages[0].SetActive(true);
        audioSource.Play();
    }

    public void OpenFirstTab()
    {
        for (int i = 1; i < tabsRight.Length; i++)
            CloseTab(i);
        OpenTab(0);
        CloseEverything();
        if (pages[1].activeInHierarchy != true)
            pages[1].SetActive(true);
        audioSource.Play();
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
        CloseEverything();
        if (pages[2].activeInHierarchy != true)
            pages[2].SetActive(true);
        audioSource.Play();
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
        CloseEverything();
        if (pages[3].activeInHierarchy != true)
            pages[3].SetActive(true);
        audioSource.Play();
    }

    public void OpenForthTab()
    {
        for (int i = 4; i < tabsRight.Length; i++)
            CloseTab(i);
        for (int i = 0; i < tabsRight.Length; i++)
        {
            if (i <= 3)
                OpenTab(i);
        }
        CloseEverything();
        if (pages[4].activeInHierarchy != true)
            pages[4].SetActive(true);
        audioSource.Play();
    }

    public void OpenFifthTab()
    {
        for (int i = 0; i < tabsRight.Length; i++)
            OpenTab(i);
        CloseEverything();
        if (pages[5].activeInHierarchy != true)
            pages[5].SetActive(true);
        audioSource.Play();
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
        iterator = i;
        Debug.Log(iterator);
    }

    // Needs to deactivate the tab on the right and active the tab on the left side.
    private void CloseTab(int i)
    {
        if (tabsLeft[i].activeInHierarchy != false)
            tabsLeft[i].SetActive(false);
        if (tabsRight[i].activeInHierarchy != true)
            tabsRight[i].SetActive(true);
    }
}
