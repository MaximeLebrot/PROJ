using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePlayerController : MonoBehaviour
{
    #region Parameters exposed in the inspector
    [Header("Puzzle Player Control")]
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float maxSpeed = 5f;
    [Tooltip("The amount of slow-down applied when turning")]
    [SerializeField] private float turnRate = 4f;
    [Tooltip("The speed at which the character model rotates when changing direction")]
    [SerializeField] private float turnSpeed;


    #endregion

    [HideInInspector] public Vector3 force;
    
    //Component references
    public PlayerPhysicsSplit physics { get; private set; }
    public Animator animator { get; private set; }


    //Input
    private InputAction quitPuzzle;
    private Vector3 input;
    private float inputThreshold = 0.1f;

    public int CurrentPuzzleID { get; set; }
    public Transform PuzzleTransform { get; set; }

    public MetaPlayerController metaPlayerController;
    private void OnEnable()
    {
        quitPuzzle = metaPlayerController.inputReference.InputMaster.ExitPuzzle;
        quitPuzzle.Enable();
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed += OnQuitPuzzle;
        metaPlayerController.inputReference.InputMaster.PlayPuzzleDescription.performed += OnPlayPuzzleDescription;
        EventHandler<InGameMenuEvent>.RegisterListener(DisableInputWhenInGameMenu);
        EventHandler<ClearPuzzleEvent>.RegisterListener(OnPuzzleCompleted);
    }

    

    private void OnDisable()
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed -= OnQuitPuzzle;
        metaPlayerController.inputReference.InputMaster.PlayPuzzleDescription.performed -= OnPlayPuzzleDescription;
        EventHandler<ClearPuzzleEvent>.UnregisterListener(OnPuzzleCompleted);
        quitPuzzle.Disable();
    }

    private void OnPuzzleCompleted(ClearPuzzleEvent obj)
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed -= OnQuitPuzzle;
    }

    void Start()
    {
        physics = GetComponent<PlayerPhysicsSplit>();
    }
    private void OnQuitPuzzle(InputAction.CallbackContext obj)
    {
        EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(PuzzleTransform.GetComponent<Puzzle>().GetPuzzleID()), false));
    }

    //Fyfan
    private void DisableInputWhenInGameMenu(InGameMenuEvent e) {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed -= OnQuitPuzzle;
        EventHandler<InGameMenuEvent>.UnregisterListener(DisableInputWhenInGameMenu);
        EventHandler<InGameMenuEvent>.RegisterListener(EnableInput);
    }
    
    private void EnableInput(InGameMenuEvent e) {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed += OnQuitPuzzle;
        EventHandler<InGameMenuEvent>.UnregisterListener(EnableInput);
        EventHandler<InGameMenuEvent>.RegisterListener(DisableInputWhenInGameMenu);
    }

    private void OnPlayPuzzleDescription(InputAction.CallbackContext obj)
    {
        if(PuzzleTransform != null)
            PuzzleTransform.GetComponent<Puzzle>().PlayPuzzleDescription();
    }

    //NOTE! Currently not safe for very low FPS
    private void FixedUpdate()
    {
        physics.AddForce(force);
        force = Vector3.zero;
    }

    #region Movement
    public void SetInput(Vector2 inp)
    {
        //Local space
        input =
        PuzzleTransform.right * inp.x +
        PuzzleTransform.forward * inp.y ;

        RotateCharacterInsidePuzzle();
        if (input.magnitude < inputThreshold)
        {
            Decelerate();
            return;
        }
        else 
        {
            if (input.magnitude > 1f)
                input.Normalize();
        }
        Accelerate();            
    }
    private void Accelerate()
    {
        Vector3 inputXZ = new Vector3(input.x, 0, input.z);
        float dot = Vector3.Dot(inputXZ.normalized, physics.GetXZMovement().normalized);

        force = input * acceleration;
        force -= (((dot - 1) * turnRate * -physics.GetXZMovement().normalized) / 2);
    }

    private void Decelerate()
    {
        Vector3 projectedDeceleration = -physics.GetXZMovement() * deceleration;
        force += projectedDeceleration;
    }

    private void RotateCharacterInsidePuzzle()
    {
        transform.forward = Vector3.Lerp(transform.forward, input.normalized, turnSpeed * Time.deltaTime);
    }

    #endregion


}
