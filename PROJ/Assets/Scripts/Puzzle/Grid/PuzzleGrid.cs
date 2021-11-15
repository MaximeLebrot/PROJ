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

        #region ERASER
        //If a line already exists with currentNode, erase
        if (lineRenderers.Count > 0 && lineRenderers.Peek().CompareLastLine(newLine))
        {
            EraseLine(node);
            return;
        }
        #endregion

        #region CREATE_NEW_LINE
        //create new Line
        else
        {
            //Unless that line already exists
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
            CreateNewLine(node);

        }
        #endregion

        #region ADD_TO_SOLUTION

        solution += PuzzleHelper.TranslateLocalInput(node, currentNode);

        //Every time we collide with node. Check if we have solution
        if (SendToPuzzleForEvaluation())
        {
            TurnOffCollision();
            return;
        }

        #endregion

        #region MOVE_CURRENT_NODE

        currentNode = node;
        lineNodes.Add(currentNode);
        ActivateNode(node, false);

        #endregion
    }

    private void CreateNewLine(Node node)
    {
        //Instantiate Line
        GameObject newLineRenderer = Instantiate(linePrefab, transform);
        newLineRenderer.transform.position = currentNode.transform.position;
        newLineRenderer.transform.localRotation = Quaternion.Inverse(masterPuzzle.transform.rotation);


        //Set position of the particle system
        newLineRenderer.GetComponent<PuzzleLine>().SetPosition((
            node.transform.localPosition - currentNode.transform.localPosition).normalized *
            Vector3.Distance(node.transform.localPosition, currentNode.transform.localPosition),
            masterPuzzle.transform.rotation);

        LineObject line = new LineObject(currentNode, newLineRenderer);

        //ADD Line to neighbourList and list of lines
        node.AddLineToNode(currentNode);
        currentNode.AddLineToNode(node);
        lineRenderers.Push(line);
    }

    private void EraseLine(Node node)
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

        //REMOVE LAST CHAR IN SOLUTION
        solution = PuzzleHelper.RemoveLastChar(solution);

        node.RemoveLineToNode(currentNode);
        currentNode.RemoveLineToNode(node);
        currentNode = node;
        ActivateNode(node, true);
        Destroy(oldLine.line);

        SendToPuzzleForEvaluation();
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
            {
                DestroyCurrentLine();
                return true;
            }
                
        }

        return false;
    }

    private void DestroyCurrentLine()
    {
        if (currentLine != null)
        {
            currentLine.Stop();
            Destroy(currentLine, 2);
            currentLine = null;
        }
    }

    //Show and activate neighbours
    private void ActivateNode(Node node, bool eraser) 
    {
        Debug.Log(node);

        node.gameObject.SetActive(true);
        
        //MARK CURRENTNODE

        //MARK THE NODES YOU CANT WALK TO
        
        foreach (Node neighbour in node.neighbours.Keys) {

            
            if (neighbour.Drawable == false)
                continue;
            
            //SHOW THE NODES THAT YOU CAN WALK TO 
            neighbour.gameObject.SetActive(true);
            neighbour.TurnOnCollider();
            if(!eraser)
                neighbour.enabledBy.Add(node);

            neighbour.OnNodeSelected += AddSelectedNode;
        } 
    }

    #region TURNING_OFF_GRID

    #region RESET_GRID
    public void ResetGrid()
    {
        solution = "";
        TurnOffLines();
        TurnOffNodes();

        DestroyCurrentLine();


    }

    private void TurnOffLines()
    {
        foreach (LineObject line in lineRenderers)
        {
            line.line.GetComponent<PuzzleLine>().TurnOffLine();
            Destroy(line.line, 3);
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

    private void RestartStartNode()
    {
        currentNode = startNode;
        currentNode.TurnOnCollider();
        currentNode.ResetNeighbours();
        Invoke("TellPuzzleGridIsReady", 1.5f);
    }

    private void TellPuzzleGridIsReady()
    {
        masterPuzzle.InitiatePuzzle();
    }
    #endregion

    #region COMPLETE_GRID

    public void CompleteGrid()
    {
        DestroyNodes();
        DestroyLines();
        DestroyCurrentLine();

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
            Destroy(n.gameObject, 2);
        }
    }
    #endregion


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