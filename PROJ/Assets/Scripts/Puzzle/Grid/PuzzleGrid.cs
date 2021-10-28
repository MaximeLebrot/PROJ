using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PuzzleGrid : MonoBehaviour {

    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private GameObject linePrefab;

    private int width;
    private int height;
    
    private List<Node> closestNodes = new List<Node>();
    private Stack<LineObject> lineRenderers = new Stack<LineObject>();
    private Node currentNode;
    private Node startNode;

    [SerializeField]private string solution;
    private List<Node> allNodes = new List<Node>();


    private PuzzleLine currentLine;
    private GameObject currentLineObject;

    public Transform Player { get; set; }

   

    public string GetSolution() 
    { 
        return solution[0] == '-' ? PuzzleHelper.SkipFirstChar(solution) : solution;
    }
    
    
    private void Awake() {

        StartGrid();
        
    }

    private void Update()
    {
        if(currentLine != null)
        {
            currentLine.Play();
            currentLine.transform.position = currentNode.transform.position;
            currentLine.SetPosition(new Vector3(Player.position.x,currentLine.transform.position.y ,Player.position.z) - currentLine.transform.position);
        }
    }

    private char SnapDirection(float angle)
    {
        float distance;
        char c = '0';


        throw new NotImplementedException();
    }

    //Setup puzzle from Awake()
    private void StartGrid()
    {
        allNodes.AddRange(transform.GetComponentsInChildren<Node>());

        startNode = currentNode = FindStartNode(ref allNodes);

        width = CalculateWidth(ref allNodes);
        height = allNodes.Count / width;

        foreach (Node node in allNodes)
            node.gameObject.SetActive(false);

        startNode.gameObject.SetActive(true);
        //AddSelectedNode(startNode);
    }
    private Node FindStartNode(ref List<Node> nodes) 
    {
        foreach(Node node in nodes)
            if (node.startNode)
            {
                //node.OnNodeSelected += AddSelectedNode;
                return node;
            }
      
        Debug.LogError("No start node selected");
        return null;
    }

    public void StartPuzzle()
    {
        ActivateNode(startNode);
        InstantiateFirstLine();
    }
    private void InstantiateFirstLine()
    {
        //instansiera linje
        //rita linje från startnod till spelare
        currentLineObject = Instantiate(linePrefab, transform.parent);
        currentLine = currentLineObject.GetComponent<PuzzleLine>();      
    }

    private void AddSelectedNode(Node node) 
    {
        if (node == currentNode)
            return;

        LineObject newLine = new LineObject(node);
        
        //THIS IS WEIRD

        //Om vi har en linje...
        if (lineRenderers.Count > 0 && lineRenderers.Peek().CompareLastLine(newLine))
        {
            //Checks if this was the last line that was drawn, if so delete that line (eraser)
            LineObject oldLine = lineRenderers.Pop();
            foreach (Node n in currentNode.enabledNodes)
            {
                n.gameObject.SetActive(false);
            }

            currentNode.enabledNodes.Clear();

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

        //Annars..
        else
        {
            //Har vi VERKLIGEN INTE EN LINJE? 
            if (lineRenderers.Count > 1)
            {
                //Checks if there exists a line between these nodes already, if so it destroys the line that was created
                if (node.HasLineToNode(currentNode))
                {
                    Debug.Log("This line already exists");
                    return;
                }
            }

            //Line Instantiation
            GameObject newLineRenderer = Instantiate(linePrefab, transform);
            newLineRenderer.transform.position = currentNode.transform.position;
            newLineRenderer.GetComponent<PuzzleLine>().SetPosition(PuzzleHelper.TranslateNumToDirection(PuzzleHelper.TranslateInput(node, currentNode)) * 3);
            LineObject line = new LineObject(currentNode, newLineRenderer);

            //ADD LINE
            node.AddLineToNode(currentNode);
            currentNode.AddLineToNode(node);
            lineRenderers.Push(line);

        }

        //THIS SHOULD BE DONE IN GETSOLUTION()
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

    //Show and activate neighbours
    private void ActivateNode(Node node) 
    {
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
                node.enabledNodes.Add(neighbour);

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

        currentLine.Stop();
        Destroy(currentLine, 2);
        currentLine = null;
        //SEND finalNodes and lineRenderers to some Persistance that can store the completed puzzles

    }

    public void ResetGrid()
    {
        solution = "";
        foreach (LineObject line in lineRenderers)
        {
            line.line.GetComponent<PuzzleLine>().Stop();
            Destroy(line.line, 2);
        }

        lineRenderers.Clear();

        foreach(Node n in allNodes)
        {
            n.ResetNeighbours();
            n.gameObject.SetActive(false);
        }

        //sätt currentLine position
        currentNode = FindStartNode(ref allNodes);
        currentLine.Stop();
        Destroy(currentLineObject, 2);
        currentLine = null;
        currentNode.gameObject.SetActive(true);
    }

}

public class LineObject
{
    //Object that can compare lines between nodes, stored in a stack in the grid
    //THIS LINE OBJECT SHOULD HOLD THE NODES THAT WERE ENABLED
    public Node originNode;
    public GameObject line;

    public LineObject(Node a, GameObject lineRen)
    {
        originNode = a;
        line = lineRen;
    }
    public LineObject(Node a)
    {
        originNode = a;
    }
    public LineObject(GameObject lineRen)
    {
        line = lineRen;
    }
    public bool CompareLastLine(LineObject other)
    {
        return originNode == other.originNode;
    }
    

}