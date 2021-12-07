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

public class ActivatorEvent : IEvent
{
    public PuzzleInfo info;
    public ActivatorEvent(PuzzleInfo info) { this.info = info; }
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

public readonly struct CameraLookAtEvent : IEvent {

    public readonly Transform lookAtTarget;
    public readonly float transitionTime;
    public readonly float delayWhenDone;
    public readonly float rotationSpeed;
    
    public CameraLookAtEvent(Transform lookAtTarget, float transitionTime, float delayWhenDone, float rotationSpeed) {
        this.lookAtTarget = lookAtTarget;
        this.transitionTime = transitionTime;
        this.delayWhenDone = delayWhenDone;
        this.rotationSpeed = rotationSpeed;
    }

}

public readonly struct CameraLookAndMoveToEvent : IEvent {

    public readonly MoveToTransitionData moveToTransitionData;
    public readonly LookAtTransitionData lookAtTransitionData;

    public readonly Vector3 endPosition;
    public readonly Quaternion endRotation;
    
    public CameraLookAndMoveToEvent(Vector3 endPosition, Quaternion endRotation, MoveToTransitionData moveToTransitionData, LookAtTransitionData lookAtTransitionData) {
        this.endPosition = endPosition;
        this.endRotation = endRotation;
        this.moveToTransitionData = moveToTransitionData;
        this.lookAtTransitionData = lookAtTransitionData;
    }

}

public class ClearPuzzleEvent : IEvent
{
    public PuzzleInfo info;
    public ClearPuzzleEvent(PuzzleInfo info) { this.info = info; }
}
public class LoadPuzzleEvent : IEvent
{
    public PuzzleInfo info;
    public LoadPuzzleEvent(PuzzleInfo info) { this.info = info; }
}



public class AwayFromKeyboardEvent : IEvent { }
#endregion

public readonly struct LockInputEvent : IEvent {

    public readonly bool lockInput;

    public LockInputEvent(bool lockInput) => this.lockInput = lockInput;

}
public class SaveEvent : IEvent { }
public class SaveSettingsEvent : IEvent 
{
    public SettingsData settingsData;
    public SaveSettingsEvent(SettingsData data) => settingsData = data;
}

//Hazards
public class UpdateHazardEvent : IEvent
{
    public bool reverse;
    public UpdateHazardEvent (bool isReverse) { reverse = isReverse; }
}
public class ResetHazardEvent : IEvent{}

public class UnLoadSceneEvent : IEvent 
{
    public string sceneToLoad;

    public UnLoadSceneEvent(string scene) { sceneToLoad = scene; }
}

public class LoadSceneEvent : IEvent{}

public class SetUpCameraEvent : IEvent
{
    public Transform followTarget;
    public Transform shoulderPos;

    public SetUpCameraEvent(Transform f, Transform s) { followTarget = f; shoulderPos = s; }
}