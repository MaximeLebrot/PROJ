using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class MetaPlayerController : MonoBehaviour, IPersist
{
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public PlayerController playerController3D { get; private set; }
    public PuzzlePlayerController puzzleController { get; private set; }
    public Animator animator { get; private set; }
    public ControllerInputReference inputReference;
    public InputActionAsset inputMaster;

    //StateMachine
    private StateMachine stateMachine;
    [SerializeField] private PlayerState[] states;
    [SerializeField] public List<ControllerValues> controllerValues = new List<ControllerValues>();
    

    [SerializeField] private float decelerationValueForCoroutine = 5;
    private Vector3 storedVelocity;


    public bool oneSwitchMode = false;

    private void Awake()
    {
        //Must listen even when script is disabled, so unregister cannot be called in "OnDisable"
        //Therefore, the register cannot be done in "OnEnable", because that subs the method several times.
        EventHandler<InGameMenuEvent>.RegisterListener(EnterInGameMenuState);

        inputReference.Initialize();
        DontDestroyOnLoad(this);

        physics = GetComponent<PlayerPhysicsSplit>();
        playerController3D = GetComponent<PlayerController>();
        puzzleController = GetComponent<PuzzlePlayerController>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine(this, states);
    }

    private void OnEnable()
    {
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
        //EventHandler<SaveSettingsEvent>.RegisterListener(HandleOneSwitchSetting);
    }
    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
        //EventHandler<SaveSettingsEvent>.UnregisterListener(HandleOneSwitchSetting)
    }

    private void Start() => (GameMenuController.Instance.RequestOption<OneHandMode>() as OneHandMode).AddListener(SetOneSwitchMode);

    private void SetOneSwitchMode(bool isActive) => oneSwitchMode = isActive;
    
    //TEMPORARY
    private void OnHub(InputAction.CallbackContext obj)
    {
        transform.position = new Vector3(871.52002f, 13.1800003f, 608.859985f);
    }

    private void StartPuzzle(StartPuzzleEvent spe)
    {
        Debug.Log("Start puzzle, one switch mode is:" + oneSwitchMode);
        puzzleController.CurrentPuzzleID = spe.info.ID;
        puzzleController.PuzzleTransform = spe.info.puzzle.transform;
        playerController3D.ResetCharacterModel();
        if (!oneSwitchMode)
        {
            Debug.Log("Changing to puzzle state");
            stateMachine.ChangeState<PuzzleState>();
        }
        else
        {
            //OSPuzzle osPuzzle = spe.info.puzzlePos.gameObject.GetComponent<OSPuzzle>();
            OSPuzzle osPuzzle = GetComponent<OSPuzzle>();
            osPuzzle.enabled = true;
            osPuzzle.StartOSPuzzle(spe);
        }
    }

    private void EnterInGameMenuState(InGameMenuEvent inGameMenuEvent) {

        if (inGameMenuEvent.Activate)
        {
            storedVelocity = physics.velocity;
            physics.velocity = Vector3.zero;
            this.enabled = false;
            animator.enabled = false;
        }
        else
        {
            StartCoroutine(SetDeceleration());
            physics.velocity = storedVelocity;
            this.enabled = true;
            animator.enabled = true;
        }
    }

    IEnumerator SetDeceleration()
    {
        float storedDeceleration = playerController3D.GetDeceleration();
        playerController3D.SetDeceleration(decelerationValueForCoroutine);

        yield return new WaitForSeconds(0.8f);
        playerController3D.SetDeceleration(storedDeceleration);
    }

    #region OS
    public void ChangeStateToOSPuzzle(StartPuzzleEvent eve)
    {
        Debug.Log("Change State to OS Puzzle");
        stateMachine.ChangeState<OSPuzzleState>();
    }

    public void ChangeStateToOSWalk(ExitPuzzleEvent eve)
    {
        Debug.Log("Change State to OS Walk");
        stateMachine.ChangeState<OSWalkState>();
    }

    //private void HandleOneSwitchSetting(SaveSettingsEvent eve)
    //{
    //    Debug.Log("One Switch is :" + eve.settingsData.oneSwitchMode);
    //    oneSwitchMode = eve.settingsData.oneSwitchMode;
    //    OSPuzzle osPuzzle = GetComponent<OSPuzzle>();
    //    osPuzzle.enabled = oneSwitchMode;
    //    if (oneSwitchMode)
    //    {
    //        stateMachine.ChangeState<OSSpinState>();
    //    }
    //}
    #endregion
    private void Update()
    {
        stateMachine.RunUpdate();
    }

    public void Save(string gameName) {
        
    }

    public void Load(string gameName) {
        
    }
}