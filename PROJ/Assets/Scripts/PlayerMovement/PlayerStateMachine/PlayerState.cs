using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject
{
    //Movement values, ScriptableObject
    [SerializeField] protected ControllerValues values;
    
    //References
    protected StateMachine stateMachine;
    protected MetaPlayerController player;
    protected object owner;

    //Input
    protected InputMaster inputMaster;
    protected float xMove, zMove;

    public virtual void Initialize(StateMachine stateMachine, object owner) 
    {   
        this.stateMachine = stateMachine;
        this.owner = owner;
        player = (MetaPlayerController)owner;
        Debug.Assert(player.physics);

        //Should not be needed when events are properly used for input
        inputMaster = player.inputMaster;
        inputMaster.Enable();

        Initialize();
    }
    public virtual void Initialize() { }
    public virtual void EnterState() 
    {
        if (values)
            player.physics.SetValues(values);        
    }
    public virtual void RunUpdate() { }
    public virtual void ExitState() { }
}
