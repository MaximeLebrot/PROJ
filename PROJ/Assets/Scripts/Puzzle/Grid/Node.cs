using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private LayerMask nodeLayer;

    
    public delegate void OnSelected(Node node);
    public event OnSelected OnNodeSelected;



    //MAKE PRIVATE
    public Dictionary<Node, bool> neighbours { get; private set; }
    public List<Node> enabledBy = new List<Node>();
    //public List<Node> enabledNodes = new List<Node>(); // this can be in LineObject instead so that a LINE knows what nodes it lit up
    
    public bool startNode;

    public int PosX, PosY;

    public bool Drawable { get; set; }


    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
        neighbours = new Dictionary<Node, bool>();
        Drawable = true;
        TurnOn();
        //FindNeighbours();
        //PosX = transform.localPosition.x;
        //PosY = transform.localPosition.y;
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
        TurnOn();
    }


    private void OnTriggerEnter(Collider other)
    {
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

    public void ClearSelectable() => OnNodeSelected = null;

    public void TurnOnCollider()
    {
        GetComponent<SphereCollider>().enabled = true;
    }

    public void SetStartNode()
    {
        startNode = true;
    }

    public void SetNeighbours(List<Node> list)
    {
        foreach(Node n in list)
        {
            //Debug.Log(n.PosX + "   " + n.PosY);
            neighbours.Add(n, false);
        }
    }

    public void TurnOn()
    {
        //Animate shit
        //anim.SetTrigger("on");
    }

    internal void TurnOff()
    {
        //Animate Shit
        anim.SetTrigger("off");
        enabledBy.Clear();
        //gameObject.SetActive(false);
    }

    public void TurnOffGameObject()
    {
        if (startNode == false)
            gameObject.SetActive(false);
    }

    internal void Restart()
    {
        Invoke("TurnOn", 1);
    }

    internal void RemoveEnablingNode(Node currentNode)
    {
        enabledBy.Remove(currentNode);
        if (enabledBy.Count == 0)
            TurnOff();
    }
}
