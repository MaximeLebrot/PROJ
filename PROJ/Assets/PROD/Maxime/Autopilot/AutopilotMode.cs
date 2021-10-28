using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopilotMode : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Node> correctNodeLine = new List<Node>();

    [SerializeField] private bool fullAutopilot;
    [SerializeField] private int step;
    [SerializeField] float speed = 5;
    [SerializeField, Range(0, 1)] float distanceFromNode = 0.6f;

    float maxDistanceDelta;
    bool moveToNode;
    bool finishedPuzzle;

    private InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    void Update()
    {
        maxDistanceDelta = speed * Time.deltaTime;
        if (moveToNode)
        {
            if (step >= correctNodeLine.Count)
            {
                step = 0;
                finishedPuzzle = true;
                moveToNode = false;
                return;
            }
            if (CheckDistance(correctNodeLine[step]))
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, GetOffsetVector(correctNodeLine[step]), maxDistanceDelta); //yoffset på player vs node | y + playerY-nodeY
            }
            else
            {
                if (!fullAutopilot)
                    moveToNode = false;
                step++;
                return;
            }
        }
        if (inputMaster.PuzzleDEBUGGER.AutoPilotPuzzle.triggered)
        {
            if (!finishedPuzzle)
                moveToNode = !moveToNode;
        }
    }

    private bool CheckDistance(Node node)
    {
        return Vector3.Distance(player.transform.position, GetOffsetVector(node)) > distanceFromNode;
    }

    private Vector3 GetOffsetVector(Node node)
    {
        Vector3 pos = correctNodeLine[step].transform.position;
        float yOffset = player.transform.position.y - correctNodeLine[step].transform.position.y;
        pos.y += yOffset;
        return pos;
    }
}
