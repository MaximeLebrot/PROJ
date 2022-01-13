using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Rebinding : MenuSettings
{
    public ControllerInputReference inputReference;
    [SerializeField] private List<RebindUIButton> rebindButtons = new List<RebindUIButton>();
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private RebindUIButton currentButton;
    private string compositeName = "";

    protected override void SubMenuInitialize()
    {
        LoadBindingOverrides();
    }
    /// Currently doesn't support rebinding multiple bindings, hard coded to use the
    /// top one, and any others wont be accessible from here.
    /// </summary>
    /// <param name="action"></param>
    public void RebindAction(InputAction action, int bindingIndex = 0)
    {
        compositeName = "";

       // Debug.Log("Action binding is : ´" + action.bindings[bindingIndex]);
        //Composite binding
        if (action.bindings[bindingIndex].isComposite)
        {
            int firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count)
                Rebind(action, firstPartIndex, composite: true);
        }
        else
        {
            Rebind(action, bindingIndex, composite: false);         
        }
    }


    private void Rebind(InputAction action, int currentBindingIndex, bool composite = false)
    {
        //Debug.Log("Binding started for " + action.name + " index " + currentBindingIndex);
        rebindingOperation?.Cancel();

        action.Disable();

        rebindingOperation = action.PerformInteractiveRebinding(currentBindingIndex)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .WithCancelingThrough("<Keyboard>/escape")
            .WithCancelingThrough("<Gamepad>/start")
            .OnCancel(operation =>
            {
                CleanUp();
                action.Enable();
            })
            .OnComplete(operation =>
            {
                CleanUp();
                //Debug.Log("New action binding is : ´" + action.bindings[0]);

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

                
                SaveBindingOverride(action);
                action.Enable();
            })           
            .Start();

        //Debug.Log("Action binding count is " + action.bindings.Count);
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
    public void BuildCompositeName(InputAction action, int bindingIndex)
    {
        string compositeName = "";
        while (bindingIndex < action.bindings.Count && action.bindings[bindingIndex].isPartOfComposite)
        {
            compositeName += InputControlPath.ToHumanReadableString(
               action.bindings[bindingIndex].effectivePath,
               InputControlPath.HumanReadableStringOptions.OmitDevice);
            bindingIndex++;

            //if there are more parts to this composite, add slash
            if(action.bindings[bindingIndex].isPartOfComposite)
                compositeName += "/";
        }
        UpdateUIButton(compositeName);
        return;
    }


    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }
 
    public void LoadBindingOverrides()
    {
        foreach (RebindUIButton btn in rebindButtons)
        {
            InputAction action = inputReference.Asset.asset.FindAction(btn.action.action.name);
            Debug.Assert(action != null);
            string bindingName = "";
            for (int i = 0; i < action.bindings.Count; i++)
            {               
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                {
                    string loadedBindingName = PlayerPrefs.GetString(action.actionMap + action.name + i);
                    action.ApplyBindingOverride(i, loadedBindingName);
                    //Debug.Log("Load: " + action + " applied override " + loadedBindingName + "counter " + i);
                    bindingName += InputControlPath.ToHumanReadableString(loadedBindingName, InputControlPath.HumanReadableStringOptions.OmitDevice);

                    if (i + 1  < action.bindings.Count && action.bindings[i + 1].isPartOfComposite)
                        bindingName += "/";
                }
            }
            if(!string.IsNullOrEmpty(bindingName))
                btn.bindingButtonText.text = bindingName; 
        }
    }

    public void RebindButton(RebindUIButton calledFrom)
    {
        currentButton = calledFrom;
        RebindAction(inputReference.inputMaster.asset.FindAction(currentButton.action.action.name));    
    }
    public void RestoreDefault(RebindUIButton calledFrom)
    {

        currentButton = calledFrom;
        InputAction action = inputReference.inputMaster.asset.FindAction(currentButton.action.action.name);

        if (action.bindings[0].isComposite)
            BuildCompositeName(action, 1); 
        else
            UpdateUIButton(action);

        action.RemoveBindingOverride(0);
    }


    public override void SetMenuItems(SettingsData settingsData) {
    }

    public override void ApplyItemValues(ref SettingsData settingsData) {
    }
}
