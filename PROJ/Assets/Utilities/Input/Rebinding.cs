using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class Rebinding : MonoBehaviour
{
    public ControllerInputReference inputReference;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private RebindUIButton currentButton;
    private string currentKey; 

    /// <summary>
    /// Currently doesn't support rebinding multiple bindings, hard coded to use the
    /// top one, and any others wont be accessible from here.
    /// </summary>
    /// <param name="action"></param>
    public void RebindAction(InputAction action, int bindingIndex = 0)
    {


        Debug.Log("Rebind started binding index is " + bindingIndex);
        action.Disable();

        if (action.bindings[bindingIndex].isComposite)
        {
            Debug.Log("is part of composite, bindingIndex is:" + bindingIndex);
            if (action.bindings.Count > bindingIndex)
                Rebind(action, bindingIndex, true);
        }
        else
        {
            Rebind(action, bindingIndex, true);
           
        }
        action.Enable();
    }
    private void Rebind(InputAction action, int currentBindingIndex, bool composite = false)
    {
        Debug.Log("Rebind..");
        rebindingOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                CleanUp();
                Debug.Log("BindFinished`");
                UpdateUIButton(action);
                if (composite)
                {
                    //Are there more parts to this composite?                   
                    int nextBindingIndex = currentBindingIndex + 1;
                    if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                    {
                        Rebind(action, nextBindingIndex, true);
                    }
                }
            })
            .OnCancel(operation => BindCancelled())
            .Start();
    }
    private void BindCancelled()
    {
        Debug.Log("BindCancelled`");
        CleanUp();
    }
    private void UpdateUIButton(InputAction action)
    {
        int bindingIndex = action.GetBindingIndexForControl(action.controls[0]);
        currentButton.bindingButtonText.text = InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
    private void CleanUp()
    {
        rebindingOperation.Dispose();
        rebindingOperation = null;
    }
    #region Methods Called from buttons
    public void RebindSprint(RebindUIButton calledFrom)
    {
        currentButton = calledFrom;
        RebindAction(inputReference.InputMaster.Sprint);      
    }
    public void RebindExitPuzzle(RebindUIButton calledFrom)
    {
        currentButton = calledFrom;
        RebindAction(inputReference.InputMaster.ExitPuzzle);
    }
    public void RebindMove(RebindUIButton calledFrom)
    {
        currentButton = calledFrom;
        RebindAction(inputReference.InputMaster.Movement);
    }
    #endregion
    //Borrowed from Unity's "RebindActionUI"
   /* [Tooltip("Text label that will receive the current, formatted binding string.")]
    [SerializeField]
    private Text m_BindingText;
    [Tooltip("Reference to action that is to be rebound from the UI.")]
    [SerializeField]
    private InputAction m_Action;
    [SerializeField]
    private string m_BindingId = "<Keyboard>/leftShift";
    public void UpdateBindingDisplay()
    {
        var displayString = string.Empty;
        var deviceLayoutName = default(string);
        var controlPath = default(string);

        // Get display string from action.

        
        if (m_Action != null)
        {
            var bindingIndex = m_Action.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
            displayString = m_Action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath);
            Debug.Log(" new BindingIndex is:" + displayString);
        }

        // Set on label (if any).
        if (currentButton.bindingButtonText != null)
            currentButton.bindingButtonText.text = displayString;

    }
    public bool ResolveActionAndBinding(out int bindingIndex)
    {
        bindingIndex = -1;
        // Look up binding index.
        var bindingId = new Guid(m_BindingId);
        bindingIndex = m_Action.bindings.IndexOf(x => x.id == bindingId);
        if (bindingIndex == -1)
        {
            Debug.LogError($"Cannot find binding with ID '{bindingId}' on '{m_Action}'", this);
            return false;
        }

        return true;
    }
    */
}
