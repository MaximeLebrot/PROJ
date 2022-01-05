using UnityEditor;
using UnityEngine;

public class LogbookUpdater : MonoBehaviour
{
    [SerializeField] private bool rightPageOnly;
    [SerializeField] private Page page;
    private static Logbook book;

    private void Awake()
    {
        if (book == null)
            book = GameObject.FindGameObjectWithTag("Logbook").GetComponent<Logbook>();
    }

    public void UpdateLogbook()
    {
        if (rightPageOnly)
            book.AddRightSide(page);
        else
            book.AddNextPage();        
    }
}
