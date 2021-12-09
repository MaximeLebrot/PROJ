using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class Rebinding : MonoBehaviour
{
    public ControllerInputReference inputReference;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private RebindUIButton currentButton;
    private string compositeName = "";
    
    
    /// <summary>
    /// Currently doesn't support rebinding multiple bindings, hard coded to use the
    /// top one, and any others wont be accessible from here.
    /// </summary>
    /// <param name="action"></param>
    public void RebindAction(InputAction action, int bindingIndex = 0)
    {
        compositeName = "";     
        
        //Composite binding
        if (action.bindings[bindingIndex].isComposite)
        {
            int firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count )
                Rebind(action, firstPartIndex, composite: true);
        }
        else
        {
            Rebind(action, bindingIndex, composite: false);
           
        }
    }

    private void Rebind(InputAction action, int currentBindingIndex, bool composite = false)
    {
        rebindingOperation?.Cancel();

        action.Disable();

        rebindingOperation = action.PerformInteractiveRebinding(currentBindingIndex)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(operation =>
            {
                CleanUp();
                action.Enable();
            })
            .OnComplete(operation =>
            {               
                CleanUp();
                
                if (composite)
                {
                    //Building a string for a composite key
                    compositeName += InputControlPath.ToHumanReadableString(
                    action.bindings[currentBindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);

                    //Are there more parts to this composite? In that case, call this method recursively                  
                    int nextBindingIndex = currentBindingIndex + 1;
                    if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                    {
                        compositeName += "/";
                        Rebind(action, nextBindingIndex, composite: true);
                    }
                    else
                    {
                        //Done with composite rebinding, assign the name. Only done AFTER the entire composite is complete, which is a bit unfortunate
                        UpdateUIButton(compositeName);
                    }
                   
                }
                else
                    UpdateUIButton(action);

                action.Enable();
            })           
            .Start();
    }
    private void CleanUp()
    {
        rebindingOperation.Dispose();
        rebindingOperation = null;
    }
    private void UpdateUIButton(string text)
    {
        currentButton.bindingButtonText.text = text;
    }
    private void UpdateUIButton(InputAction action)
    {
        int bindingIndex = action.GetBindingIndexForControl(action.controls[0]);
        currentButton.bindingButtonText.text = InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public static void LoadBindingOverride(string actionName)
    {
        /*if (inputActions == null)
            inputActions = new RebindJumping();

        InputAction action = inputActions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
        }*/
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
    public void RebindResetPuzzle(RebindUIButton calledFrom)
    {
        currentButton = calledFrom;
        //RebindAction(inputReference.InputMaster.ResetPuzzle);
    }
    #endregion
    
}
