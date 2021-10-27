using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logbook : MonoBehaviour
{
    // For the time being it just flips between tabs

    public GameObject page1; //Base
    public GameObject page2; //Minimalist
    public GameObject page3; //Titles
    public GameObject page4; //Thicker
    public GameObject page5; //Refined

    public GameObject[] tabs;

    public void OpenFirstTab()
    {
        for(int i = 1; i < tabs.Length; i++)
            MoveBackTab(i);
        MoveTab(0);
        CloseEverything();
        if (page1.activeInHierarchy != true)
            page1.SetActive(true);
    }

    public void OpenSecondTab()
    {
        for (int i = 2; i < tabs.Length; i++)
            MoveBackTab(i);
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i <= 1)
                MoveTab(i);
        }
        CloseEverything();
        if (page2.activeInHierarchy != true)
            page2.SetActive(true);
    }

    public void OpenThirdTab()
    {
        for (int i = 3; i < tabs.Length; i++)
            MoveBackTab(i);
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i <= 2)
                MoveTab(i);
        }
        MoveTab(2);
        CloseEverything();
        if (page3.activeInHierarchy != true)
            page3.SetActive(true);
    }

    public void OpenForthTab()
    {
        for (int i = 4; i < tabs.Length; i++)
            MoveBackTab(i);
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i <= 3)
                MoveTab(i);
        }
        CloseEverything();
        if (page4.activeInHierarchy != true)
            page4.SetActive(true);
    }

    public void OpenFifthTab()
    {
        for (int i = 0; i < tabs.Length; i++)
                MoveTab(i);
        CloseEverything();
        if (page5.activeInHierarchy != true)
            page5.SetActive(true);
    }

    public void CloseEverything()
    {
        if (page1.activeInHierarchy != false)
            page1.SetActive(false);
        if (page2.activeInHierarchy != false)
            page2.SetActive(false);
        if (page3.activeInHierarchy != false)
            page3.SetActive(false);
        if (page4.activeInHierarchy != false)
            page4.SetActive(false);
        if (page5.activeInHierarchy != false)
            page5.SetActive(false);
    }

    public void MoveTab(int i)
    {
        Transform tabsTransform = tabs[i].GetComponent<Transform>();
        if (tabsTransform.transform.position.x == 1830f)
        {
            tabsTransform.transform.position = new Vector3(tabsTransform.transform.position.x - 1740, tabsTransform.transform.position.y, 0);
        }
    }

    public void MoveBackTab(int i)
    {
        Transform tabsTransform = tabs[i].GetComponent<Transform>();
        if (tabsTransform.transform.position.x == 90f)
        {
            tabsTransform.transform.position = new Vector3(tabsTransform.transform.position.x + 1740, tabsTransform.transform.position.y, 0);
        }
    }
}
