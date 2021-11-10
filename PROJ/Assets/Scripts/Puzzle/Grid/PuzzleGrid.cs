using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PuzzleGrid : MonoBehaviour {

    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject startNodePrefab;
    [SerializeField] private int size;

    private int nodeOffset = 3;
    
    private List<Node> closestNodes = new List<Node>();
    private List<Node> lineNodes = new List<Node>();
    private Stack<LineObject> lineRenderers = new Stack<LineObject>();
    private Node currentNode;
    private Node startNode;

    [SerializeField]private string solution;
    //private List<Node> allNodesLIST = new List<Node>();
    private Node[,] allNodes; 


    private List<Node> unrestrictedNodes = new List<Node>();


    private PuzzleLine currentLine;
    private GameObject currentLineObject;

    public Transform Player { get; set; }

    private Puzzle masterPuzzle;

    public string GetSolution() 
    {
        //Debug.Log(solution);
        if (solution.Length > 0)
            return solution[0] == '-' ? PuzzleHelper.SkipFirstChar(solution) : solution;
        else
            return "";
    }
    
    
    private void OnEnable() 
    {

        //StartGrid();
        
    }

    private void Update()
    {
        if(currentLine != null)
        {
            currentLine.Play();
            currentLine.transform.position = currentNode.transform.position;
            currentLine.transform.localRotation = Quaternion.Inverse(GetComponentInParent<Puzzle>().transform.rotation);
            currentLine.SetPosition((new Vector3(Player.position.x,currentLine.transform.position.y ,Player.position.z) - currentLine.transform.position));
        }
    }

    private char SnapDirection(float angle)
    {
        float distance;
        char c = '0';


        throw new NotImplementedException();
    }

    //Setup puzzle from Puzzle
    public void StartGrid()
    {

        GenerateGrid();

        masterPuzzle = GetComponentInParent<Puzzle>();
        //allNodesLIST.AddRange(transform.GetComponentsInChildren<Node>());
        foreach (Node node in allNodes)
            node.gameObject.SetActive(false);

        startNode.gameObject.SetActive(true);
    }

    public List<Node> FindNeighbours(Node n)
    {

        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int nX = n.PosX + x;
                int nY = n.PosY + y;

                if (nX >= 0 && nX < size && nY >= 0 && nY < size)
                {
                    neighbours.Add(allNodes[nX, nY]);
                }
            }
        }
        
        return neighbours;
    }


    void GenerateGrid()
    {
        allNodes = new Node[size, size];
        Vector3 zVec = new Vector3(0, 0, 1);
        Vector3 xVec = new Vector3(1, 0, 0);
        int midIndex = size / 2;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if(x == midIndex && y == midIndex)
                    allNodes[x, y] = Instantiate(startNodePrefab, transform).GetComponent<Node>();
                else
                    allNodes[x, y] = Instantiate(nodePrefab, transform).GetComponent<Node>();

                allNodes[x, y].PosX = x;
                allNodes[x, y].PosY = y;

                allNodes[x, y].transform.position = transform.position;
                allNodes[x,y].transform.localPosition = (x * Vector3.right * nodeOffset) + (y * Vector3.forward * nodeOffset);

                
            }
        }

        foreach(Node n in allNodes)
        {
            n.SetNeighbours(FindNeighbours(n));
        }

        allNodes[midIndex, midIndex].SetStartNode();
        startNode = currentNode = allNodes[midIndex, midIndex];
        transform.localPosition = (Vector3.right * -midIndex * nodeOffset) + (Vector3.forward * -midIndex * nodeOffset);
    }



    public void StartPuzzle()
    {
        ActivateNode(startNode, false);
        InstantiateFirstLine();
    }
    private void InstantiateFirstLine()
    {
        //instansiera linje
        //rita linje från startnod till spelare
        currentLineObject = Instantiate(linePrefab, transform.parent);
        currentLine = currentLineObject.GetComponent<PuzzleLine>();      
    }

    internal void SetRestrictions(List<Vector2Int> unrestricted)
    {
        int midIndex = size / 2;
        foreach (Node n in allNodes)
            n.Drawable = false;
        
        foreach(Vector2Int vInt in unrestricted)
        {
            //Debug.Log(vInt);
            allNodes[vInt.x + midIndex, vInt.y + midIndex].Drawable = true;
        }

        startNode.Drawable = true;
    }

    private void AddSelectedNode(Node node) 
    {
        if (node == currentNode)
            return;

        LineObject newLine = new LineObject(node);
        

        //Om vi har en linje...
        if (lineRenderers.Count > 0 && lineRenderers.Peek().CompareLastLine(newLine))
        {
            //Checks if this was the last line that was drawn, if so delete that line (eraser)
            LineObject oldLine = lineRenderers.Pop();
            foreach (Node n in currentNode.neighbours.Keys)
            {
                n.RemoveEnablingNode(currentNode); 
            }

            /*
            if (lineNodes.Contains(currentNode))
                lineNodes.Remove(currentNode);
            */

            //REMOVE LAST CHAR IN SOLUTION OR CALCULATE EVERYTHING AFTERWARDS
            solution = PuzzleHelper.RemoveLastChar(solution);

            node.RemoveLineToNode(currentNode);
            currentNode.RemoveLineToNode(node);
            currentNode = node;
            ActivateNode(node, true);
            Destroy(oldLine.line);

            SendToPuzzleForEvaluation();
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
            newLineRenderer.transform.localRotation = Quaternion.Inverse(masterPuzzle.transform.rotation);

            newLineRenderer.GetComponent<PuzzleLine>().SetPosition((
                node.transform.localPosition - currentNode.transform.localPosition).normalized * 
                Vector3.Distance(node.transform.localPosition, currentNode.transform.localPosition),
                masterPuzzle.transform.rotation);

            LineObject line = new LineObject(currentNode, newLineRenderer);

            //ADD LINE
            node.AddLineToNode(currentNode);
            currentNode.AddLineToNode(node);
            lineRenderers.Push(line);

        }

        //THIS SHOULD BE DONE IN GETSOLUTION()
        solution += PuzzleHelper.TranslateLocalInput(node, currentNode);

        if (SendToPuzzleForEvaluation())
        {
            TurnOffCollision();
            return;
        }
            

        currentNode = node;
        lineNodes.Add(currentNode);
        ActivateNode(node, false);
    }

    private void TurnOffCollision()
    {
        foreach (Node n in allNodes)
        {
            n.TurnOffCollider();
        }
    }

    private bool SendToPuzzleForEvaluation()
    {
        if (solution.Length > 0)
        {
            //masterPuzzle.CheckIfClearedSymbol(solution[0] == '-' ? PuzzleHelper.SkipFirstChar(solution) : solution);
            if (masterPuzzle.EvaluateSolution())
                return true;
        }

        return false;
    }
    //Show and activate neighbours
    private void ActivateNode(Node node, bool eraser) 
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

            if (neighbour.Drawable == false)
                continue;

            neighbour.gameObject.SetActive(true);
            neighbour.TurnOnCollider();
            if(!eraser)
                neighbour.enabledBy.Add(node);

            neighbour.OnNodeSelected += AddSelectedNode;
        } 
    }

    #region TURNING_OFF_GRID
    public void CompleteGrid()
    {
        DestroyNodes();
        DestroyLines();
        if (currentLine != null)
        {
            currentLine.Stop();
            Destroy(currentLine, 2);
            currentLine = null;
        }

    }

    public void ResetGrid()
    {
        solution = "";
        TurnOffLines();
        TurnOffNodes();

        if (currentLineObject != null)
        {
            currentLine.Stop();
            Destroy(currentLineObject, 2);
            currentLine = null;
        }


    }

    private void TurnOffLines()
    {
        foreach (LineObject line in lineRenderers)
        {
            line.line.GetComponent<PuzzleLine>().TurnOffLine();
            Destroy(line.line, 2);
        }
        lineRenderers.Clear();
    }

    private void TurnOffNodes()
    {
        Debug.Log("TURN OFF NODES");
        foreach (Node n in allNodes)
        {
            if(n.startNode == false)
            {
                n.ResetNeighbours();
                n.TurnOffCollider();
                n.TurnOff();
                n.Drawable = true;
            }
        }
        //RestartStartNode();
        Invoke("RestartStartNode", 1f);
    }

    private void DestroyLines()
    {
        foreach (LineObject line in lineRenderers)
        {
            line.line.GetComponent<PuzzleLine>().TurnOffLine();
            Destroy(line.line, 2);
        }

        
    }
    private void DestroyNodes()
    {
        foreach (Node n in allNodes)
        {
            n.TurnOffCollider();
            n.TurnOff();
            Destroy(n, 2);
        }
    }

    private void RestartStartNode()
    {
        Debug.Log("Turning ON StartNode");
        currentNode = startNode;
        currentNode.gameObject.SetActive(true);
        currentNode.TurnOn();
        currentNode.TurnOnCollider();
        Invoke("TellPuzzleGridIsReady", 1.5f);
    }

    private void TellPuzzleGridIsReady()
    {
        masterPuzzle.InitiatePuzzle();
    }

    #endregion
}

public class LineObject
{
    //Object that can compare lines between nodes, stored in a stack in the grid
    //THIS LINE OBJECT SHOULD HOLD THE NODES THAT WERE ENABLED
    public Node originNode;
    public GameObject line;
    public List<Node> enabledNodes = new List<Node>();

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