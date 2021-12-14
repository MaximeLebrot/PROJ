using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSPuzzle : MonoBehaviour
{ 
    [Header("References"), SerializeField] private Puzzle puzzle;
    [SerializeField] private MetaPlayerController player;
    [SerializeField] private GameObject UINodeParent;

    private List<Button> UINodes;

    [Header("Variables"), SerializeField, Range(0.1f, 0.9f)] private float speed;
    [SerializeField, Range(0.01f, 0.3f)] private float holdingButtonLimit = 0.2f;

    private float time = 1f, timer;
    private int iterator = 0;
    private float frameCounter;
    private bool pressingButton = false;
    private bool giveLostTime = true;

    private InputMaster inputMaster;

    public void StartOSPuzzle(StartPuzzleEvent eve)
    {
        //player.velocity = Vector3.zero
        UINodeParent.SetActive(true);
        timer = time;
    }

    public void ExitOSPuzzle(ExitPuzzleEvent eve)
    {
        UINodeParent.SetActive(false);
    }

    private void Update()
    {
        time = 1f - speed;
        if (inputMaster.OneSwitch.PuzzleTest.ReadValue<float>() != 0)
        {
            pressingButton = true;
            frameCounter += Time.deltaTime;
            if (frameCounter >= holdingButtonLimit)
            {
                if (giveLostTime)
                    timer += frameCounter;
                giveLostTime = false;
                if (timer >= time)
                {
                    if (iterator >= UINodes.Count)
                        iterator = 0;
                    foreach (Button node in UINodes)
                        node.image.color = Color.white;
                    UINodes[iterator].image.color = Color.red;
                    iterator++;
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
        else
        {
            if (pressingButton)
            {
                if (frameCounter < holdingButtonLimit)
                {
                    Debug.Log("Triggered");
                }
                frameCounter = 0;
                pressingButton = false;
                giveLostTime = true;
            }

        }
    }


    private void OnEventCalled(StartPuzzleEvent eve)
    {
        StartOSPuzzle(eve);
        player.ChangeStateToOSPuzzle(eve);
    }

    private void OnEventCalled(ExitPuzzleEvent eve)
    {
        ExitOSPuzzle(eve);
        player.ChangeStateToOSWalk(eve);
    }

    #region UES
    private void Start()
    {
        if (UINodeParent == null)
            return;
        if (UINodes.Count == 0)
            UINodes.AddRange(UINodeParent.GetComponentsInChildren<Button>());
        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MetaPlayerController>();
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    private void OnEnable() 
    {
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitOSPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartOSPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(player.ChangeStateToOSPuzzle);
        EventHandler<ExitPuzzleEvent>.RegisterListener(player.ChangeStateToOSWalk);
    }

    private void OnDisable()
    {
        inputMaster.Disable();
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitOSPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartOSPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(player.ChangeStateToOSPuzzle);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(player.ChangeStateToOSWalk);
    }
    #endregion
}