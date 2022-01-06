using UnityEditor;
using UnityEngine;

public class LogbookUpdater : MonoBehaviour
{
    [SerializeField] private int puzzleID;
    [SerializeField] private int pagesAdded;
    [SerializeField] private Logbook log;

    [Header("Options for adding right page"), SerializeField] private bool rightPageOnly;
    [SerializeField] private Page[] rightSidePages;
    private static Logbook book;


    private void Awake()
    {
        if (log != null)
            book = log;
        //FindLogbook();
    }

    private void FindLogbook()
    {
        //if (book == null)
            //book = GameObject.FindGameObjectWithTag("Logbook").GetComponent<Logbook>();
    }

    private void OnEnable()
    {
        EventHandler<ActivatorEvent>.RegisterListener(UpdateLogbook);
    }

    private void OnDisable()
    {
        EventHandler<ActivatorEvent>.UnregisterListener(UpdateLogbook);
    }

    public void UpdateLogbook(ActivatorEvent eve)
    {
        if (eve.info.ID == puzzleID)
        {
            FindLogbook();
            if (rightPageOnly)
            {
                for (int i = 0; i < rightSidePages.Length; i++)
                    book.AddRightSide(rightSidePages[i]);
            }
            else
            {
                for (int i = 0; i < pagesAdded; i++)
                    book.AddNextPage();
            }
        }
    }
}
