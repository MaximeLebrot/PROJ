using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayMainTheme : MonoBehaviour {
    [SerializeField] private ControllerInputReference controllerInputReference;
    
    //[SerializeField] private AudioClip theme;
    //SerializeField] private AudioSource source;

    private FMOD.Studio.EventInstance cutsceneTheme;
    //private void Awake()
    //{
    //    if (source == null)
    //        source = GetComponent<AudioSource>();
    //}

    private void Start() {
        controllerInputReference.InputMaster.Menu.performed += EndCutSceneEarly;
    }
    
    public void PlayTheme()
    {
        //    source.PlayOneShot(theme);
        cutsceneTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Cutscene/Aria Main theme startscreen");
        cutsceneTheme.start();
        cutsceneTheme.release();
    }

    public void StopTheme()
    {
        cutsceneTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        cutsceneTheme.release();
    }

    private void EndCutSceneEarly(InputAction.CallbackContext e) {
        controllerInputReference.InputMaster.Menu.performed -= EndCutSceneEarly;
        StopTheme();
        GetComponent<Animator>().StopPlayback();
        GetComponent<SceneChanger>().Scene("TutorialMainHub");
    }

}
