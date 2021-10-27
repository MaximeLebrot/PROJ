using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour {

    [SerializeField] private LineRenderer lineRendererPrefab;

    private int width;
    private int height;
    
    private List<Node> closestNodes = new List<Node>();
    private Stack<LineObject> lineRenderers = new Stack<LineObject>();
    private Node currentNode;

    [SerializeField]private string solution;
    private List<Node> allNodes = new List<Node>();

    private LineRenderer currentLine;

    public string GetSolution() 
    { 
        return solution[0] == '-' ? PuzzleHelper.SkipFirstChar(solution) : solution;
    }
    
    
    private void Awake() {

        StartGrid();
        //currentLine = Instantiate(lineRendererPrefab, transform);
        //currentLine.SetPosition(0, currentNode.transform.position);
    }

    private void Update()
    {
        //currentLine.SetPosition(1, )
    }


    private void StartGrid()
    {

        allNodes.AddRange(transform.GetComponentsInChildren<Node>());


        Node startNode = currentNode = FindStartNode(ref allNodes);

        width = CalculateWidth(ref allNodes);
        height = allNodes.Count / width;

        foreach (Node node in allNodes)
            node.gameObject.SetActive(false);

        startNode.gameObject.SetActive(true);


        //AddSelectedNode(startNode);
    }

    private Node FindStartNode(ref List<Node> nodes) {
        foreach(Node node in nodes)
            if (node.startNode)
            {
                node.OnNodeSelected += AddSelectedNode;
                return node;
            }
                

        
        Debug.LogError("No start node selected");
        return null;
    }
    
    private void AddSelectedNode(Node node) 
    {
        LineObject newLine = new LineObject(node);
        
        //THIS IS WEIRD
        if (lineRenderers.Count > 0 && lineRenderers.Peek().CompareLastLine(newLine))
        {
            //Checks if this was the last line that was drawn, if so delete that line (eraser)
            LineObject oldLine = lineRenderers.Pop();
            foreach (Node n in currentNode.EnabledNodes)
            {
                n.gameObject.SetActive(false);
            }

            currentNode.EnabledNodes.Clear();

            //REMOVE LAST CHAR IN SOLUTION OR CALCULATE EVERYTHING AFTERWARDS
            Debug.Log(solution);
            solution = PuzzleHelper.RemoveLastChar(solution);
            Debug.Log(solution);

            node.RemoveLineToNode(currentNode);
            currentNode.RemoveLineToNode(node);
            currentNode = node;
            ActivateNode(node);
            Destroy(oldLine.line);
            return;
        }

        else
        {
            if (lineRenderers.Count > 1)
            {
                //Checks if there exists a line between these nodes already, if so it destroys the line that was created
                if (node.HasLineToNode(currentNode))
                {
                    Debug.Log("This line already exists");
                    return;
                }
            }

            LineRenderer newLineRenderer = Instantiate(lineRendererPrefab, transform);
            newLineRenderer.SetPosition(0, currentNode.transform.position);
            newLineRenderer.SetPosition(1, node.transform.position);
            LineObject line = new LineObject(currentNode, newLineRenderer);

            //ADD LINE
            node.AddLineToNode(currentNode);
            currentNode.AddLineToNode(node);
            lineRenderers.Push(line);
        }

        //THIS SHOULD BE DONE IN GETSOLUTION()
        if (lineRenderers.Count > 0) 
            solution += PuzzleHelper.TranslateInput(node, currentNode); 

        currentNode = node;
        ActivateNode(node);
    }

    private int CalculateWidth(ref List<Node> nodes) {
        
        float currentHeight = nodes[0].transform.position.y;
        
        int width = 0;
        int index = 0;

        while (nodes[index++].transform.position.y <= currentHeight)
            width++;

        return width;
    }

    private void ActivateNode(Node node) {
        node.gameObject.SetActive(true);

        if (closestNodes.Count > 0) {
            foreach(Node closestNode in closestNodes)
                closestNode.ClearSelectable();
        }
        
        foreach(Node n in node.neighbours.Keys)
        {
            closestNodes.Add(n);
        }
        
        foreach (Node neighbour in node.neighbours.Keys) {

            if (neighbour.gameObject.activeSelf == false)
                node.EnabledNodes.Add(neighbour);

            neighbour.gameObject.SetActive(true);
            neighbour.OnNodeSelected += AddSelectedNode;
        } 
    }

    public void CompleteGrid()
    {
        List<Node> finalNodes = new List<Node>();
        Debug.Log("Save grid");
        foreach(Node n in allNodes)
        {
            if (n.gameObject.activeSelf)
                finalNodes.Add(n);

            n.TurnOffCollider();
        }

        //SEND finalNodes and lineRenderers to some Persistance that can store the completed puzzles

    }

    public void ResetGrid()
    {
        solution = "";
        foreach (LineObject line in lineRenderers)
        {
            Destroy(line.line);
        }

        lineRenderers.Clear();

        foreach(Node n in allNodes)
        {
            n.ResetNeighbours();
            n.gameObject.SetActive(false);
        }

        currentNode = FindStartNode(ref allNodes);
        
        currentNode.gameObject.SetActive(true);
    }

}

public class LineObject
{
    //Object that can compare lines between nodes, stored in a stack in the grid
    //THIS LINE OBJECT SHOULD HOLD THE NODES THAT WERE ENABLED
    public Node originNode;
    public LineRenderer line;

    public LineObject(Node a, LineRenderer lineRen)
    {
        originNode = a;
        line = lineRen;
    }
    public LineObject(Node a)
    {
        originNode = a;
    }
    public LineObject(LineRenderer lineRen)
    {
        line = lineRen;
    }
    public bool CompareLastLine(LineObject other)
    {
        return originNode == other.originNode;
    }
    

}