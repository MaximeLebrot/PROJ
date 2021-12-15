using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSPuzzle : MonoBehaviour
{ 
    [Header("References"), SerializeField] private Puzzle puzzle;
    [SerializeField] private MetaPlayerController player;
    [SerializeField] private PuzzlePlayerController puzzleController;
    [SerializeField] private GameObject UINodeParent;

    private List<OSPuzzleNode> UINodes = new List<OSPuzzleNode>();

    [Header("Variables"), SerializeField, Range(0.1f, 0.9f)] private float speed = 0.5f;
    [SerializeField, Range(0.01f, 0.3f)] private float holdingButtonLimit = 0.2f;
    [SerializeField] private float puzzleWalkDuration = 1f;

    private float time = 1f, timer;
    private int iterator = 2;
    private float frameCounter;
    private bool pressingButton = false;
    private bool giveLostTime = true;
    private bool movingPlayer = false;
    private Vector3 currentWalkingDirection = new Vector3();

    private InputMaster inputMaster;

    public void StartOSPuzzle(StartPuzzleEvent eve)
    {
        //player.velocity = Vector3.zero
        player.ChangeStateToOSPuzzle(eve);
        UINodeParent.SetActive(true);
        time = 1f - speed;
        timer = time;
    }

    public void ExitOSPuzzle(ExitPuzzleEvent eve)
    {
        UINodeParent.SetActive(false);
        player.ChangeStateToOSWalk(eve);
    }

    private void Update()
    {
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
                    foreach (OSPuzzleNode node in UINodes)
                        node.DeselectPuzzleNode();
                    UINodes[iterator].SelectPuzzleNode();
                    iterator++;
                    timer = 0;
                }
                else
                    timer += Time.deltaTime;
            }
        }
        else
        {
            if (pressingButton)
            {
                if (frameCounter < holdingButtonLimit && !movingPlayer)
                {
                    Debug.Log("Triggered");
                    MovePlayerTo(GetActiveButton());
                }
                frameCounter = 0;
                pressingButton = false;
                giveLostTime = true;
            }
        }
        if (movingPlayer)
        {
            //player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + currentWalkingDirection, Time.deltaTime * puzzleWalkSpeed);
            puzzleController.SetInput(currentWalkingDirection);
        }
    }

    IEnumerator WalkToNode()
    {
        movingPlayer = true;
        yield return new WaitForSeconds(puzzleWalkDuration);
        movingPlayer = false;
    }

    private int GetActiveButton()
    {
        OSPuzzleNode node = null;
        foreach (OSPuzzleNode n in UINodes)
        {
            if (n.GetSelected())
                node = n;
        }
        return node.number;
    }

    private void MovePlayerTo(int numPDirection)
    {
        currentWalkingDirection = ConvertNodeNumberToVecDir(numPDirection);
        StartCoroutine(WalkToNode());
        //Debug.Log(numPDirection + " = " + numPDirection + ", Dir: " + ConvertNodeNumberToDirection(numPDirection));
    }

    private Vector3 ConvertNodeNumberToDirection(int number)
    {
        switch (number)
        {
            case 1:
                return transform.forward + -transform.right;
            case 2:
                return transform.forward;
            case 3:
                return transform.forward + transform.right;
            case 4:
                return transform.right;
            case 5:
                return -transform.forward + transform.right;
            case 6:
                return -transform.forward;
            case 7:
                return -transform.forward + -transform.right;
            case 8:
                return -transform.right;
        }
        return Vector3.zero;
    }

    private Vector2 ConvertNodeNumberToVecDir(int number)
    {
        switch (number)
        {
            case 1:
                return Vector2.up + Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.up + Vector2.right;
            case 4:
                return Vector2.right;
            case 5:
                return Vector2.down + Vector2.right;
            case 6:
                return Vector2.down;
            case 7:
                return Vector2.down + Vector2.left;
            case 8:
                return Vector2.left;
        }
        return Vector2.zero;
    }

    #region UES
    private void Awake()
    {
<<<<<<< Updated upstream
        if (UINodeParent == null)
            Debug.LogError("Give reference to UI");
        if (UINodes.Count == 0)
=======
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<MetaPlayerController>();
        FindPuzzleGridAndNodes();
    }

    private void FindPuzzleGridAndNodes()
    {
        UINodeParent = GameObject.FindGameObjectWithTag("OneSwitchCanvas");
        UINodes.AddRange(UINodeParent.GetComponentsInChildren<OSPuzzleNode>());
        for (int i = 0; i < UINodes.Count; i++)
>>>>>>> Stashed changes
        {
            UINodes[i].number = i + 1;
        }
        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<MetaPlayerController>();
        if (puzzleController == null)
            puzzleController = player.GetComponent<PuzzlePlayerController>();
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

<<<<<<< Updated upstream
    private void OnEnable() 
    {
=======
    private void Start()
    {
        UINodeParent.SetActive(false);
        puzzleGrid = GetComponent<Puzzle>().grid;
    }

    private void OnEnable()
    {
        puzzleGrid = GetComponent<Puzzle>().grid;
        if (UINodeParent == null)
            FindPuzzleGridAndNodes();
        EventHandler<StartPuzzleEvent>.RegisterListener(StartOSPuzzle);
>>>>>>> Stashed changes
        EventHandler<ExitPuzzleEvent>.RegisterListener(ExitOSPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(StartOSPuzzle);
    }

    private void OnDisable()
    {
        inputMaster.Disable();
        EventHandler<ExitPuzzleEvent>.UnregisterListener(ExitOSPuzzle);
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartOSPuzzle);
    }
    #endregion
}