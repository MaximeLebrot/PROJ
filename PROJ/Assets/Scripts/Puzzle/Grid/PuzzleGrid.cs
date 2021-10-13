using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour {

    [SerializeField] private LineRenderer lineRendererPrefab;
    private int gridWidth = 8;
    Node[,] allNodes;
    
    
    private int _width;
    private int _height;
    
    private List<Node> _closestNodes = new List<Node>();

    private List<LineRenderer> _lineRenderers = new List<LineRenderer>();

    private Node _currentNode;
    
    private void Awake() {
        List<Node> allNodes = new List<Node>();


        allNodes.AddRange( transform.GetComponentsInChildren<Node>());

        Debug.Log(allNodes.Count);
        Node startNode = _currentNode = FindStartNode(ref allNodes);
        
        _width = CalculateWidth(ref allNodes);
        _height = allNodes.Count / _width;
        
        foreach(Node node in allNodes) 
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
    
    private void AddSelectedNode(Node node) {
        LineRenderer newLineRenderer = Instantiate(lineRendererPrefab, transform);
        newLineRenderer.SetPosition(0, _currentNode.transform.position);
        newLineRenderer.SetPosition(1, node.transform.position);
        
        _lineRenderers.Add(newLineRenderer);

        _currentNode = node;
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

        if (_closestNodes.Count > 0) {
            foreach(Node closestNode in _closestNodes)
                closestNode.ClearSelectable();
        }
        
        _closestNodes = node._neighbours;
        
        foreach (Node neighbour in node._neighbours) {
            neighbour.gameObject.SetActive(true);
            neighbour.OnNodeSelected += AddSelectedNode;
        }
            
    }
    

}