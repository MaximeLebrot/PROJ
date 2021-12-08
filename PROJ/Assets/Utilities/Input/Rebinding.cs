using UnityEngine;
using UnityEngine.InputSystem;

public class Rebinding : MonoBehaviour
{
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference exitPuzzleAction;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    public void RebindAction(InputActionReference action)
    {
       rebindingOperation = action.action.PerformInteractiveRebinding()
                                         .WithControlsExcluding("Mouse")
                                         .OnMatchWaitForAnother(0.1f)
                                         .OnComplete(operation => BindFinished(action))
                                         .Start();

    }
    private void BindFinished(InputActionReference action)
    {
        rebindingOperation.Dispose();
        Debug.Log("Rebind complete, " + action + " is now " + action.action.bindings[0]);
    }
    public void RebindSprint()
    {
        RebindAction(sprintAction);
    }
    public void RebindExitPuzzle()
    {
        RebindAction(exitPuzzleAction);
    }

}
