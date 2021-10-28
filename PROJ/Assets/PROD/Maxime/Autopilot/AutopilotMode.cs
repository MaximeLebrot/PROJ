using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopilotMode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private GameObject pilotText;
    [SerializeField] private List<Node> correctNodeLine = new List<Node>();

    [Header("Autopilot")]
    [SerializeField] private bool fullAutopilot;
    [SerializeField] float speed = 5;
    [SerializeField, Range(0, 1)] float distanceFromNode = 0.1f;

    [SerializeField] private bool inPuzzleZone;

    int step;
    float maxDistanceDelta;
    bool moveToNode;
    bool finishedPuzzle;

    private InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPuzzleZone = true;
            pilotText.SetActive(true);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPuzzleZone = false;
            pilotText.SetActive(false);
        }
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
        if (inputMaster.PuzzleDEBUGGER.ToggleFullAutopilot.triggered)
            ToggleFullAutopilot();

        if (inPuzzleZone)
        {
            maxDistanceDelta = speed * Time.deltaTime;
            if (inputMaster.PuzzleDEBUGGER.AutoPilotPuzzle.triggered)
            {
                if (!finishedPuzzle)
                {
                    if (fullAutopilot)
                        moveToNode = true;
                    else
                        moveToNode = !moveToNode;
                }
            }
            if (!moveToNode)
            {
                if (fullAutopilot)
                    step = 0;
                return;
            }
            else
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
                    player.transform.position = Vector3.MoveTowards(player.transform.position, GetOffsetVector(correctNodeLine[step]), maxDistanceDelta);
                }
                else
                {
                    if (!fullAutopilot)
                        moveToNode = false;
                    step++;
                    return;
                }
            }
        }
    }

    private void ToggleFullAutopilot()
    {
        checkMark.SetActive(!checkMark.activeInHierarchy);
        fullAutopilot = !fullAutopilot;
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
