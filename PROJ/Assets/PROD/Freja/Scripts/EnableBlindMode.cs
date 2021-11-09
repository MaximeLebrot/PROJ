using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBlindMode : MonoBehaviour
{
    [SerializeField]private GameObject blindPanel;

    private InputMaster inputMaster;
    private bool blindToggle;

    void Awake()
    {
        
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    void Start()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
        ExitBlindMode();
    }

    void Update()
    {
        if (inputMaster.PuzzleDEBUGGER.BlindMode.triggered)
        {
            if (blindToggle)
            {
                ExitBlindMode();
            }else if (!blindToggle)
            {
                EnterBlindMode();
            }
        }
    }

    private void EnterBlindMode()
    {
        blindPanel.SetActive(true);
        blindToggle = true;
    }

    private void ExitBlindMode()
    {
        blindPanel.SetActive(false);
        blindToggle = false;
    }
}
