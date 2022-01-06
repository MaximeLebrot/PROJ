using UnityEditor;
using UnityEngine;

public class LogbookUpdater : MonoBehaviour
{
    [SerializeField] private int puzzleID;
    [SerializeField] private int pagesAdded;
    [SerializeField] private Logbook log;

    [Header("Options for adding right page"), SerializeField] private bool rightPageOnly;
    [SerializeField] private string[] rightSidePagesName;
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
        if (book == null)
            book = GameObject.FindGameObjectWithTag("CanvasLogbook").GetComponent<LogbookHandler>().Logbook;
        Debug.Log(book);
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
            Debug.Log("Eve info ID: " + eve.info.ID);
            FindLogbook();
            if (rightPageOnly)
            {
                //for (int i = 0; i < rightSidePages.Length; i++)
                    //book.AddRightSide(rightSidePages[i]);
                for (int i = 0; i < rightSidePagesName.Length; i++)
                    book.AddRightSide(rightSidePagesName[i]);
            }
            else
            {
                for (int i = 0; i < pagesAdded; i++)
                    book.AddNextPage();
            }
        }
    }
}
