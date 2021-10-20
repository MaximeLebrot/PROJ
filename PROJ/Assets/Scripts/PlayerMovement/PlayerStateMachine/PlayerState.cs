using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject
{
    [SerializeField] protected ControllerValues values;
    protected StateMachine stateMachine;
    protected MetaPlayerController player;
    protected object owner; 
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
        player.physics.SetValues(values);
    }
    public virtual void RunUpdate() { }
    public virtual void ExitState() { }
}
