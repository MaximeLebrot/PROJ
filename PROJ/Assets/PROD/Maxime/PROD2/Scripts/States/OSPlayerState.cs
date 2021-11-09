using UnityEngine;

public class OSPlayerState : ScriptableObject
{
    //Movement values, ScriptableObject
    //[SerializeField] protected ControllerValues values;

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

        Initialize();
    }
    public virtual void Initialize() { }
    public virtual void EnterState() { }
    public virtual void RunUpdate() { }
    public virtual void ExitState() { }
}
