using UnityEditor;
using UnityEngine;

public class LogbookUpdater : MonoBehaviour
{

    [Header("Options for adding right page"), SerializeField] private bool rightPageOnly;
    [SerializeField] private Page page;

    [SerializeField] private int puzzleID;
    private static Logbook book;

    private void Awake()
    {
        if (book == null)
            book = GameObject.FindGameObjectWithTag("Logbook").GetComponent<Logbook>();
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
            if (rightPageOnly)
                book.AddRightSide(page);
            else
                book.AddNextPage();
        } 
    }
}
