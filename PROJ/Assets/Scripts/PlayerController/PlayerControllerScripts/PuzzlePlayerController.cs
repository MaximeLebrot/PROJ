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
    public Transform puzzleTransform { get; set; }
    public MetaPlayerController metaPlayerController;

    //Input
    private Vector3 input;
    private float inputThreshold = 0.1f;

    public int currentPuzzleID { get; set; }


    private void OnEnable()
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed += OnQuitPuzzle;
        metaPlayerController.inputReference.InputMaster.PlayPuzzleDescription.performed += OnPlayPuzzleDescription;

        EventHandler<StartPuzzleEvent>.RegisterListener(OnPuzzleStart);
        EventHandler<ClearPuzzleEvent>.RegisterListener(OnPuzzleCompleted);
        (GameMenuController.Instance.RequestOption<EasyPuzzleControls>() as EasyPuzzleControls).AddListener(ApplySettings);

        EventHandler<RequestSettingsEvent>.FireEvent(null);
    }    
    private void OnDisable()
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed -= OnQuitPuzzle;
        metaPlayerController.inputReference.InputMaster.PlayPuzzleDescription.performed -= OnPlayPuzzleDescription;
        EventHandler<ClearPuzzleEvent>.UnregisterListener(OnPuzzleCompleted);
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
        (GameMenuController.Instance.RequestOption<EasyPuzzleControls>() as EasyPuzzleControls).RemoveListener(ApplySettings);
    }  
    void Start()
    {
        physics = GetComponent<PlayerPhysicsSplit>();
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
        puzzleTransform.right * inp.x +
        puzzleTransform.forward * inp.y ;

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
        if (input.magnitude < inputThreshold)
            return;
        transform.forward = Vector3.Lerp(transform.forward, input.normalized, turnSpeed * Time.deltaTime);
    }

    #endregion

    #region OnEvent Methods
    private void ApplySettings(bool easyControls)
    {
        if (easyControls == true)
            acceleration = 50;
        else
            acceleration = 100;
    }

    private void OnPuzzleStart(StartPuzzleEvent obj)
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed += OnQuitPuzzle;
    }

    private void OnPuzzleCompleted(ClearPuzzleEvent obj)
    {
        metaPlayerController.inputReference.InputMaster.ExitPuzzle.performed -= OnQuitPuzzle;

    }
    private void OnQuitPuzzle(InputAction.CallbackContext obj)
    {
        EventHandler<ExitPuzzleEvent>.FireEvent(new ExitPuzzleEvent(new PuzzleInfo(puzzleTransform.GetComponent<Puzzle>().GetPuzzleID()), false));
    }


    private void OnPlayPuzzleDescription(InputAction.CallbackContext obj)
    {
        if (puzzleTransform != null)
            puzzleTransform.GetComponent<Puzzle>().PlayPuzzleDescription();
    }


    #endregion



}
