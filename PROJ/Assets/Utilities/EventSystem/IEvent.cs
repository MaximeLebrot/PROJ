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

#region PUZZLE_EVENTS
public class StartPuzzleEvent : IEvent 
{
    public PuzzleInfo info;
    public StartPuzzleEvent(PuzzleInfo info) { this.info = info; }
    //Maybe this should hold puzzle id or position?
}

public class ExitPuzzleEvent : IEvent
{
    public PuzzleInfo info;
    public bool success;
    public ExitPuzzleEvent(PuzzleInfo info, bool state) { this.info = info; this.success = state; }
}


public class ResetPuzzleEvent : IEvent
{
    public PuzzleInfo info;
    public ResetPuzzleEvent(PuzzleInfo info) { this.info = info; }
}



public class EvaluateSolutionEvent : IEvent 
{
    public PuzzleInfo info;
    public EvaluateSolutionEvent(PuzzleInfo info) { this.info = info; }
}

public class PuzzleInfo
{
    public int ID;
    public Transform puzzlePos;

    public PuzzleInfo(int id) { ID = id; }
    public PuzzleInfo(int id, Transform pp) { ID = id; puzzlePos = pp; }
}

public readonly struct PlayerStateChangeEvent : IEvent {

    public readonly PlayerState newState;
    public PlayerStateChangeEvent(PlayerState state) => newState = state;
}

public class AwayFromKeyboardEvent : IEvent { }
public class SaveEvent : IEvent { }
#endregion
