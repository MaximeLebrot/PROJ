using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RebindUIButton : MonoBehaviour
{
    public Text description;
    public Button resetToDefault;
    public Button bindingButton;
    public Text bindingButtonText;

    private ControllerInputReference inputReference;
    private InputAction action;

}
