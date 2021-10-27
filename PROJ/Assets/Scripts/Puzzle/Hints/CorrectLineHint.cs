using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CorrectLineHint : MonoBehaviour
{
    [SerializeField] Puzzle puzzle;

    private int step;
    private char[] solution;
    private Dictionary<Node, int> nodeSteps;
    public Node startNode;
    private Node currentNode; //senaste korrekta nod
    private Node correctNextNode;

    private List<Node> allNodes = new List<Node>();

    private void Start()
    {
        if (puzzle == null)
            puzzle = GetComponent<Puzzle>();

        allNodes.AddRange(transform.GetComponentsInChildren<Node>());
        nodeSteps = new Dictionary<Node, int>();
        step = 0;
        solution = puzzle.GetSolution().ToCharArray();
    }

    public void ShowHint(Node node)
    {
        UpdateCorrectNextNode(node);
        if (node.startNode)
        {
            if (nodeSteps.ContainsKey(node))
            {
                UpdateCurrentNode(nodeSteps.Keys.Last());
                return;
            }
            startNode = node;
            currentNode = startNode;
            UpdateCurrentNode(startNode);
            nodeSteps.Add(node, step);
            return;
        }

        //currentNode.MarkCurrentNode();

        if (step == solution.Length)
        {
            //won
            //kanske ha autowin? måste man konfimera är frågan?

            return;
        }
      
        if (nodeSteps.ContainsKey(node))
        {
            UpdateCurrentNode(node);
            step = nodeSteps[node];
        }
        else if (SteppedCorrectly(currentNode, node, solution[step]))
        {
            UpdateCurrentNode(node);
            nodeSteps.Add(node, step++);
        }
        else
        {
            nodeSteps.Clear();
            step = 0;
            UpdateCurrentNode(startNode);
        }
        Debug.Log("STEP: " + step);
    }

    public void Hint(Node node)
    {
        if (step == solution.Length)
            return; //wincon

        if (node.startNode)
        {
            if (!nodeSteps.ContainsKey(node))
            {
                startNode = node;
                currentNode = node;
                correctNextNode = FindCorrectNextNode();
                Debug.Log(correctNextNode);
                nodeSteps.Add(node, step);
                step++;
                UpdateDebugInfo();
            }
            return;
        }

        if (!nodeSteps.ContainsKey(node))
        {

        }

        //startNode = node;
        //currentNode = node;
        //correctNextNode = node;
        //nodeSteps.Add(node, step);
        //step++;

    }

    private void UpdateDebugInfo()
    {
        foreach (Node node in allNodes)
        {
            node.UnmarkCurrentNode();
            node.UnHintCorrectNextNode();
        }
        currentNode.MarkCurrentNode();
        correctNextNode.HintCorrectNextNode();
    }

    private Node FindCorrectNextNode()
    {
        Debug.Log(solution[step]);
        Debug.Log(PuzzleHelper.TranslateNumToDirection(solution[step]));
        return currentNode.FindSpecificNeighbour(PuzzleHelper.TranslateNumToDirection(solution[step]));
    }

    private bool SteppedCorrectly(Node currentNode, Node node, char numpadStep)
    {
        if (node == currentNode.FindSpecificNeighbour(PuzzleHelper.TranslateNumToDirection(numpadStep)))
        {
            UpdateCurrentNode(node);
            return true;
        }
        else
            return false;
    }

    private void UpdateCorrectNextNode(Node node)
    {
        if (node.startNode)
        {
            currentNode = node;
            correctNextNode = node;
        }
        else
            correctNextNode.UnHintCorrectNextNode();

        correctNextNode = currentNode.FindSpecificNeighbour(PuzzleHelper.TranslateNumToDirection(solution[step]));
        correctNextNode.HintCorrectNextNode();
    }

    private void UpdateCurrentNode(Node node)
    {
        currentNode.UnmarkCurrentNode();
        currentNode = node;
        currentNode.MarkCurrentNode();
    }
}
