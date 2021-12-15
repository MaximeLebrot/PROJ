using System.Collections.Generic;
using UnityEngine;

public class OSPuzzle : MonoBehaviour
{
    [SerializeField] private MetaPlayerController player;
    [SerializeField] private GameObject UINodeParent;

    public List<OSPuzzleNode> UINodes = new List<OSPuzzleNode>();

    public void StartOSPuzzle(StartPuzzleEvent eve)
    {
        //player.velocity = Vector3.zero
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
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<MetaPlayerController>();

        FindPuzzleGridAndNodes();
    }

    private void FindPuzzleGridAndNodes()
    {
        UINodeParent = GameObject.FindGameObjectWithTag("OneSwitchCanvas");
        UINodes.AddRange(UINodeParent.GetComponentsInChildren<OSPuzzleNode>());
        for (int i = 0; i < UINodes.Count; i++)
            UINodes[i].Initialize(i);
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MetaPlayerController>();
    }

    private void Start()
    {
        UINodeParent.SetActive(false);
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