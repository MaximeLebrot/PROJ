using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour {

    [SerializeField] private ControllerInputReference controllerInputReference;

    private void Start() => controllerInputReference.InputMaster.Menu.performed += LoadNewScene;

    private void OnDisable() => controllerInputReference.InputMaster.Menu.performed -= LoadNewScene;

    private void LoadNewScene(InputAction.CallbackContext e) => SceneManager.LoadScene("TutorialMainHub");

}
