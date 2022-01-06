using UnityEditor;
using UnityEngine;

public class LogbookUpdater : MonoBehaviour
{
    [SerializeField] private int puzzleID;
    [SerializeField] private Logbook book;
    [SerializeField] private int pagesAdded;

    [Header("Options for adding right page"), SerializeField] private bool rightPageOnly;
    [SerializeField] private string[] rightSidePagesName;


    private void Start()
    {
        if (log != null)
        {
            book = FindObjectOfType(typeof(Logbook)) as Logbook;
        }
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
            //FindLogbook();
            if (rightPageOnly)
            {
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
