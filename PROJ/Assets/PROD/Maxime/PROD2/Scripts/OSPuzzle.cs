using System.Collections.Generic;
using UnityEngine;

public class OSPuzzle : MonoBehaviour
{ 
    [SerializeField] private MetaPlayerController player;
    [SerializeField] private GameObject UINodeParent;
    [SerializeField] public PuzzleGrid puzzleGrid;


    public List<OSPuzzleNode> UINodes = new List<OSPuzzleNode>();

    public void StartOSPuzzle(StartPuzzleEvent eve)
    {
        player.ChangeStateToOSPuzzle(eve);
        UINodeParent.SetActive(true);
    }

    public void ExitOSPuzzle(ExitPuzzleEvent eve)
    {
        UINodeParent.SetActive(false);
        player.ChangeStateToOSWalk(eve);
    }

    private void Awake()
    {
        puzzleGrid = GetComponent<Puzzle>().grid;
        if (UINodeParent == null)
            Debug.LogError("Give reference to UI");
        if (UINodes.Count == 0)
        {
            UINodes.AddRange(UINodeParent.GetComponentsInChildren<OSPuzzleNode>());
            for (int i = 0; i < UINodes.Count; i++)
            {
                UINodes[i].number = i + 1;
                if (i == 1)
                    UINodes[i].SelectPuzzleNode();
            }
        }
    }
    
    private void OnEnable()
    {
        EventHandler<StartPuzzleEvent>.RegisterListener(StartOSPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitOSPuzzle);
    }

    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartOSPuzzle);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitOSPuzzle);
    }
}