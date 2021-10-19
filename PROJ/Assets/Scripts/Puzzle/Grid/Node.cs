using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private LayerMask nodeLayer;
    
    public delegate void OnSelected(Node node);
    public event OnSelected OnNodeSelected;

    public Dictionary<Node, bool> neighbours { get; private set; }
    
    public bool startNode;
    
    private void Awake() {
        neighbours = new Dictionary<Node, bool>();

        FindNeighbours();
    }
    


    private void OnTriggerEnter(Collider other)
    {
        OnNodeSelected?.Invoke(this);
    }


    private void FindNeighbours() {
        float angle = 0; 
        
        for (int i = 0; i < 8; i++) {

            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),0 ,Mathf.Sin(angle * Mathf.Deg2Rad));
            
            Physics.Raycast(transform.position, direction, out var hit, 5, nodeLayer);
            
            if(hit.collider)
                neighbours.Add(hit.transform.GetComponent<Node>(), false);

            angle += 45f;
            
        }
    }

    public void AddLineToNode(Node n)
    {
        neighbours[n] = true;
    }

    public void RemoveLineToNode(Node n)
    {
        neighbours[n] = false;
    }

    public bool HasLineToNode(Node n)
    {
        return neighbours[n];
    }

    public void ClearSelectable() => OnNodeSelected = null;
}
