using System.Collections.Generic;
using UnityEngine;

public class OSPuzzle : MonoBehaviour
{
    [SerializeField] private MetaPlayerController player;
    [SerializeField] private GameObject UINodeParent;
    [SerializeField] private bool oneSwitch;

    public static List<OSPuzzleNode> UINodes = new List<OSPuzzleNode>();

    public void StartOSPuzzle(StartPuzzleEvent eve)
    {
        if (player == null)
            player = GetComponent<MetaPlayerController>();
            //player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<MetaPlayerController>();
        //player.velocity = Vector3.zero
        player.ChangeStateToOSPuzzle(eve);
        SetUINodesActive(true);
    }

    public void ExitOSPuzzle(ExitPuzzleEvent eve)
    {
        SetUINodesActive(false);
        player.ChangeStateToOSWalk(eve);
    }

    private void Awake()
    {
        FindPuzzleUINodes();
        SetUINodesActive(false);
        if (player == null)
            player = GetComponent<MetaPlayerController>();
        //player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<MetaPlayerController>();
    }

    private void Start()
    {
        //if (player == null)
            //player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<MetaPlayerController>();
    }

    private void FindPuzzleUINodes()
    {
        if (UINodeParent == null)
            UINodeParent = GameObject.FindGameObjectWithTag("OneSwitchCanvas");
        //UINodeParent.SetActive(true);
        if (UINodes.Count == 0)
        {
            UINodes.AddRange(UINodeParent.GetComponentsInChildren<OSPuzzleNode>());
            for (int i = 0; i < UINodes.Count; i++)
                UINodes[i].Initialize(i);
        }
    }

    private void SetUINodesActive(bool active)
    {
        foreach (OSPuzzleNode node in UINodes)
            node.gameObject.SetActive(active);
    }

    private void OnEnable()
    {
        EventHandler<SaveSettingsEvent>.RegisterListener(HandleOSSetting);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartOSPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitOSPuzzle);
    }

    private void HandleOSSetting(SaveSettingsEvent eve)
    {
        //this.enabled = eve.settingsData.oneSwitchMode;
        /*if (this.enabled == false)
            return;*/
        Debug.Log("Kör: " + eve.settingsData.oneSwitchMode);

    }

    private void OnDisable()
    {
        EventHandler<SaveSettingsEvent>.UnregisterListener(HandleOSSetting);
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartOSPuzzle);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitOSPuzzle);
    }
}