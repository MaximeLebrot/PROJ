using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSPuzzleState")]
public class OSPuzzleState : PlayerState
{
    private OSPuzzle puzzle;

    [Header("Variables"), SerializeField, Range(0.1f, 0.9f)] private float speed = 0.5f;
    [SerializeField, Range(0.01f, 0.3f)] private float holdingButtonLimit = 0.2f;
    [SerializeField] private float puzzleWalkDuration = 1f;

    private float time = 1f, timer;
    private int iterator = 2;
    private float frameCounter;
    private bool giveLostTime = true;
    private bool pressingButton;
    private bool movingPlayer;
    private bool diagonalMove;
    private bool realignPlayer;

    private Transform puzzleTransform;

    private Vector3 offset = new Vector3(0, 1.5f, 0);
    private Vector3 currentWalkingDirection = new Vector3();
    private Vector2 playerXZ = new Vector2();
    private Vector2 puzzleXZ = new Vector2();

    public override void Initialize() => base.Initialize();

    public override void EnterState() => HandleStateEntry();

    public override void ExitState() => HandleExitState();

    public override void RunUpdate()
    {
        HandleStartingAlignment();
        HandlePuzzleMovement();
    }

    private void HandlePuzzleMovement()
    {
        if (player.inputReference.inputMaster.OneSwitch.OnlyButton.ReadValue<float>() != 0)
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
                    if (iterator >= OSPuzzle.UINodes.Count)
                        iterator = 0;
                    foreach (OSPuzzleNode node in OSPuzzle.UINodes)
                        node.DeselectPuzzleNode();
                    OSPuzzle.UINodes[iterator].SelectPuzzleNode();
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
                    MovePlayerTo(GetActiveButton());
                frameCounter = 0;
                pressingButton = false;
                giveLostTime = true;
            }
        }
        if (movingPlayer)
            player.puzzleController.SetInput(currentWalkingDirection);
    }

    IEnumerator WalkToNode()
    {
        movingPlayer = true;
        float walkDuration = puzzleWalkDuration;
        if (diagonalMove)
            walkDuration = Mathf.Sqrt(Mathf.Pow(puzzleWalkDuration, 2) * 2);
        yield return new WaitForSeconds(walkDuration);
        diagonalMove = false;
        movingPlayer = false;
    }

    private int GetActiveButton()
    {
        OSPuzzleNode node = null;
        foreach (OSPuzzleNode n in OSPuzzle.UINodes)
        {
            if (n.GetSelected())
                node = n;
        }
        return node.number;
    }

    private void MovePlayerTo(int numPDirection)
    {
        currentWalkingDirection = ConvertNodeNumberToVecDir(numPDirection);
        puzzle.StartCoroutine(WalkToNode());
        //Debug.Log(numPDirection + " = " + numPDirection + ", Dir: " + ConvertNodeNumberToDirection(numPDirection));
    }

    private Vector2 ConvertNodeNumberToVecDir(int number)
    {
        switch (number)
        {
            case 1:
                diagonalMove = true;
                return Vector2.up + Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                diagonalMove = true;
                return Vector2.up + Vector2.right;
            case 4:
                return Vector2.right;
            case 5:
                diagonalMove = true;
                return Vector2.down + Vector2.right;
            case 6:
                return Vector2.down;
            case 7:
                diagonalMove = true;
                return Vector2.down + Vector2.left;
            case 8:
                return Vector2.left;
        }
        return Vector2.zero;
    }

    private void HandleStartingAlignment()
    {
        UpdateVector2s();
        if (Vector2.Distance(playerXZ, puzzleXZ) < 0.1f && realignPlayer == true)
        {
            Debug.Log("realigned");
            realignPlayer = false;
            player.physics.velocity = Vector3.zero;
        }
        if (realignPlayer)
            player.transform.position = Vector3.Lerp(player.transform.position, puzzleTransform.position + offset, Time.deltaTime * 5);
    }

    private void UpdateVector2s()
    {
        playerXZ.x = player.transform.position.x;
        playerXZ.y = player.transform.position.z;
    }

    private void HandleExitState()
    {
        base.ExitState();
        Debug.Log("OS Puzzle State Exit");
        player.playerController3D.enabled = true;
        player.puzzleController.enabled = false;
    }

    private void HandleStateEntry()
    {
        base.EnterState();
        Debug.Log("OS Puzzle state");
        time = 1f - speed;
        timer = time;
        realignPlayer = true;
        player.physics.velocity = Vector3.zero;
        player.playerController3D.enabled = false;
        player.puzzleController.enabled = true;
    }

    private void GetPuzzleInfo(StartPuzzleEvent eve)
    {
        puzzleTransform = eve.info.puzzle.transform;
        puzzleXZ.x = puzzleTransform.position.x;
        puzzleXZ.y = puzzleTransform.position.z;
        //puzzle = eve.info.puzzlePos.gameObject.GetComponent<OSPuzzle>();
        puzzle = GameObject.FindGameObjectWithTag("Player").GetComponent<OSPuzzle>();
        //puzzle = player.GetComponent<OSPuzzle>();
    }

    private void OnEnable() => EventHandler<StartPuzzleEvent>.RegisterListener(GetPuzzleInfo);

    private void OnDisable() => EventHandler<StartPuzzleEvent>.UnregisterListener(GetPuzzleInfo);
}