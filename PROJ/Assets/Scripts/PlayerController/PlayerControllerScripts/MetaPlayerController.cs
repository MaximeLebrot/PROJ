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

    //StateMachine
    private StateMachine stateMachine;
    [SerializeField] private PlayerState[] states;
    [SerializeField] public List<ControllerValues> controllerValues = new List<ControllerValues>();
    
    public ControllerInputReference inputReference;

    private void Awake()
    {
        inputReference.Initialize();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        physics = GetComponent<PlayerPhysicsSplit>();
        playerController3D = GetComponent<PlayerController>();
        puzzleController = GetComponent<PuzzlePlayerController>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine(this, states);
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

    //TEMPORARY
    private void OnHub(InputAction.CallbackContext obj)
    {
        transform.position = new Vector3(871.52002f, 13.1800003f, 608.859985f);
    }

    private void StartPuzzle(StartPuzzleEvent spe)
    {
        puzzleController.CurrentPuzzleID = spe.info.ID;
        puzzleController.PuzzleTransform = spe.info.puzzlePos;
        stateMachine.ChangeState<PuzzleState>();
    }

    Vector3 storedVelocity; 
    private void EnterInGameMenuState(InGameMenuEvent inGameMenuEvent) {
     
        Debug.Log("Enter int game menu");
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
    public float decelerationValueForCoroutine = 5;
    IEnumerator SetDeceleration()
    {
        float storedDeceleration = playerController3D.GetDeceleration();
        playerController3D.SetDeceleration(decelerationValueForCoroutine);
        float time = 1.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        playerController3D.SetDeceleration(storedDeceleration);
    }
    
    public void ChangeStateToOSPuzzle(StartPuzzleEvent eve) => stateMachine.ChangeState<OSPuzzleState>();

    public void ChangeStateToOSWalk(ExitPuzzleEvent eve) => stateMachine.ChangeState<OSWalkState>();

    private void Update()
    {
        
        stateMachine.RunUpdate();
    }


    public void Save(string gameName)
    {
        
    }

    public void Load(string gameName)
    {
        
    }
}