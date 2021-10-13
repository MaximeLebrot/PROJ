//Author: Pol Lozano Llorens
//Secondary Author: Rickard Lindgren
using UnityEngine;

public interface IEvent { }

#region DEBUG_EVENT
public struct DebugInfo
{
    public GameObject obj;
    public int verbosity;
    public string message;
}

public class DebugEvent : IEvent
{
    public DebugInfo Info { get; }
    public DebugEvent(DebugInfo info) => Info = info;
}
#endregion


#region ATTACK_EVENTS
public class StartPlayerAttackEvent : IEvent { }

public class EndPlayerAttackEvent : IEvent { }
#endregion


