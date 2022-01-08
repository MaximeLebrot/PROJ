using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUIButton : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Button resetToDefault;
    public Button bindingButton;
    public TextMeshProUGUI bindingButtonText;
    public InputActionReference action;
    private void OnBtnPress(InputAction.CallbackContext obj)
    {
        bindingButton.onClick.Invoke();
    }

}
