using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private LayerMask nodeLayer;
    
    public delegate void OnSelected(Node node);
    public event OnSelected OnNodeSelected;

    public Dictionary<Node, bool> neighbours { get; private set; }
    public List<Node> EnabledNodes = new List<Node>(); // this can be in LineObject instead so that a LINE knows what nodes it lit up
    
    public bool startNode;

    public CorrectLineHint clh;
    public GameObject currentNode;
    public GameObject correctNextNode;
    
    private void Awake() {
        neighbours = new Dictionary<Node, bool>();
        if (clh == null)
        {
            GetComponentInParent<Transform>().gameObject.GetComponentInParent<CorrectLineHint>();
        }
        if (startNode)
            clh.startNode = this;
        FindNeighbours();
    }
    


    private void OnTriggerEnter(Collider other)
    {
        clh.ShowHint(this);
        OnNodeSelected?.Invoke(this);
    }


    private void FindNeighbours() {
        float angle = 0; 
        
        for (int i = 0; i < 8; i++) {

            Vector3 direction = transform.parent.rotation * new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad),0);
            
            Physics.Raycast(transform.position, direction, out var hit, 5, nodeLayer);

            //Debug.DrawRay(transform.position, direction * 5, Color.cyan, 10);

            if(hit.collider)
                neighbours.Add(hit.transform.GetComponent<Node>(), false);



            angle += 45f;
            
        }
    }

    public Node FindSpecificNeighbour(Vector3 direction)
    {
        Physics.Raycast(transform.position, direction, out var hit, 5, nodeLayer);

        if (hit.collider)
        {
            Debug.Log("hit " + hit.collider.gameObject.name);
            return hit.transform.GetComponent<Node>();
        }
        Debug.Log("hit nothing");

        return null;
    }

    public void AddLineToNode(Node n)
    {
        neighbours[n] = true;
    }

    public void RemoveLineToNode(Node n)
    {
        neighbours[n] = false;
    }

    public void ResetNeighbours()
    {
        foreach (var key in neighbours.Keys.ToList())
        {
            neighbours[key] = false;
        }
    }

    public bool HasLineToNode(Node n)
    {
        return neighbours[n];
    }

    public void TurnOffCollider()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    public void MarkCurrentNode()//debugging
    {
        currentNode.SetActive(true);
    }
    
    public void UnmarkCurrentNode()//debugging
    {
        currentNode.SetActive(false);
    }

    public void HintCorrectNextNode()
    {
        correctNextNode.SetActive(true);
    }

    public void UnHintCorrectNextNode()
    {
        correctNextNode.SetActive(false);
    }


    public void ClearSelectable() => OnNodeSelected = null;
}
