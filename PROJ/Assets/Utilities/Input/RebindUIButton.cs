using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class RebindUIButton : MonoBehaviour
{
    public Text description;
    public Button resetToDefault;
    public Button bindingButton;
    public Text bindingButtonText;
    public InputActionReference action;

    public ControllerInputReference inputRef;
    InputAction button;
    private void Start()
    {
        button = inputRef.InputMaster.REBINDTEST;
        button.Enable();
        inputRef.InputMaster.REBINDTEST.performed += OnBtnPress;
    }

    private void OnBtnPress(InputAction.CallbackContext obj)
    {
        bindingButton.onClick.Invoke();
    }
}
