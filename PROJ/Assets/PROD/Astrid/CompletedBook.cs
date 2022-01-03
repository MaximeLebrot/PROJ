using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedBook : MonoBehaviour
{
    public int pagesAquired = -1;
    [SerializeField] private List<Page> allPages = new List<Page>();

    public SymbolPage symbolsChapter;
    public OperandPage OperandChapter;
    public MetaOperandPage MetaOperandChapter;

    public Page GetNextPage()
    {
        if (pagesAquired > allPages.Count - 2)
            return null;

        pagesAquired++;
        return allPages[pagesAquired];
    }
}
