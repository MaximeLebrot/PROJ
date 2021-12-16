using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlightAnimationScript : MonoBehaviour
{
    private Animator jumpAnimator;

    private MetaPlayerController mpc;
    private new Transform transform;

    private Vector3 adjustedPosition = new Vector3();
    private Vector2 mpcXZPos, XZPos;
    public bool moveToCenter;
    private float adjustedYValue;
    private float colSizeOffset;

    public float time;
    private bool flight;

    [SerializeField] private string triggerValue;
    
    private void Awake()
    {
        colSizeOffset = GetComponent<SphereCollider>().radius;
        transform = GetComponent<Transform>();
        jumpAnimator = GetComponent<Animator>();
        XZPos = new Vector2(transform.position.x, transform.position.z);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            jumpAnimator.SetTrigger(triggerValue);
            mpc = col.GetComponent<MetaPlayerController>();
            mpc.physics.enabled = false;
            mpc.transform.parent = transform;
            adjustedYValue = mpc.transform.localPosition.y;
            mpc.enabled = false;
            moveToCenter = true;
            mpcXZPos = new Vector2(mpc.transform.position.x, mpc.transform.position.z);
            GameObject.FindGameObjectWithTag("Solver").SetActive(false);
            //  playerAnimator.SetLayerWeight(2, 1f);
            flight = true;
            EventHandler<TransportationBegunEvent>.FireEvent(null);            
        }       
    }

    public void ActivateScripts()
    {
        EventHandler<TransportationEndedEvent>.FireEvent(null);
        GetComponent<SphereCollider>().enabled = false;
        mpc.transform.parent = null;
        mpc.physics.enabled = true;
        mpc.enabled = true;
        EventHandler<TransportationEndedEvent>.FireEvent(null);
        GameObject.FindGameObjectWithTag("Solver").SetActive(true);
        flight = false;
    //    playerAnimator.SetLayerWeight(2, 0);
    }

    private void Update()
    {
        if(moveToCenter)
        {
            mpcXZPos.x = mpc.transform.position.x;
            mpcXZPos.y = mpc.transform.position.z;
            XZPos.x = transform.position.x;
            XZPos.y = transform.position.z;

            if (Vector2.Distance(XZPos, mpcXZPos) < 0.05f)
            {
                Debug.Log("Detach");
                moveToCenter = false;
            }

            adjustedPosition = transform.position;
            adjustedPosition.y += adjustedYValue;
            mpc.transform.position = Vector3.MoveTowards(mpc.transform.position, adjustedPosition, Time.deltaTime * colSizeOffset);
        }

        if(flight) 
            mpc.animator.SetLayerWeight(2, time);

    }


    //Maybe we also want to destroy this game object when we're done with it, OR make it part of some type of object pooling. Just leaving it in the scene serves no purpose.

}

