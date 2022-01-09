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

    //StateMachine
    private StateMachine stateMachine;
    [SerializeField] private PlayerState[] states;
    [SerializeField] public List<ControllerValues> controllerValues = new List<ControllerValues>();
    

    [SerializeField] private float decelerationValueForCoroutine = 5;
    private Vector3 storedVelocity;


    public bool oneSwitchMode = false;

    private void Awake()
    {
        inputReference.Initialize();
        DontDestroyOnLoad(this);

        physics = GetComponent<PlayerPhysicsSplit>();
        playerController3D = GetComponent<PlayerController>();
        puzzleController = GetComponent<PuzzlePlayerController>();
        animator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        EventHandler<StartPuzzleEvent>.RegisterListener(StartPuzzle);
        EventHandler<InGameMenuEvent>.RegisterListener(EnterInGameMenuState);
    }
    private void OnDisable()
    {
        EventHandler<StartPuzzleEvent>.UnregisterListener(StartPuzzle);
    }

    private void OnDestroy()
    {
        EventHandler<InGameMenuEvent>.UnregisterListener(EnterInGameMenuState);
    }
    private void Start() {
        (GameMenuController.Instance.RequestOption<OneSwitchMode>() as OneSwitchMode).AddListener(SetOneSwitchMode);
        stateMachine = new StateMachine(this, states);
    }

    private void SetOneSwitchMode(bool isActive) => oneSwitchMode = isActive;

    //TEMPORARY
    private void OnHub(InputAction.CallbackContext obj)
    {
        transform.position = new Vector3(871.52002f, 13.1800003f, 608.859985f);
    }

    private void StartPuzzle(StartPuzzleEvent spe)
    {
        if (stateMachine.currentState.GetType() == typeof(PuzzleState))
            return;
        puzzleController.currentPuzzleID = spe.info.ID;
        puzzleController.puzzleTransform = spe.info.puzzle.transform;
        playerController3D.ResetCharacterModel();
        if (!oneSwitchMode)
        {
            stateMachine.ChangeState<PuzzleState>();
        }
        else
        {
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