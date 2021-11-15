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
    protected float xMove, zMove;

    public virtual void Initialize(StateMachine stateMachine, object owner) 
    {   
        this.stateMachine = stateMachine;
        this.owner = owner;
        player = (MetaPlayerController)owner;
        Debug.Assert(player.physics);



        Initialize();
    }
    public virtual void Initialize() { }
    public virtual void EnterState() 
    {
        if (values)
            player.physics.SetValues(values);
    }
    public virtual void EnterState(PlayerState previousState)
    {
        if (values)
            player.physics.SetValues(values);
    }
    public virtual void RunUpdate() { }
    public virtual void ExitState() { }
}
