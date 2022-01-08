using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBlindMode : MonoBehaviour
{
    [SerializeField] private GameObject breadLoaf;

    private InputMaster inputMaster;
    private bool blindToggle;

    private FMOD.Studio.EventInstance currentAudioSource;
    private FMOD.Studio.EventInstance nextAudioSource;

    Breadloaf breadloaf;

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
                breadloaf = breadLoaf.GetComponent<Breadloaf>();
                breadloaf.EndAudioSources();
                ExitBlindMode();
            }
            else if (!blindToggle)
            {
                EnterBlindMode();
            }
        }
    }

    private void EnterBlindMode()
    {
        breadLoaf.SetActive(true);
        blindToggle = true;
    }

    private void ExitBlindMode()
    {
        breadLoaf.SetActive(false);
        blindToggle = false;
    }
}