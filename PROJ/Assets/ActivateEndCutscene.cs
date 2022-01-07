using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateEndCutscene : MonoBehaviour {

    [SerializeField] private int IDToListenFor;
    [SerializeField] private string cutsceneName;
    
    private void OnEnable() => EventHandler<ExitPuzzleEvent>.RegisterListener(TriggerCutScene);
    private void OnDisable() => EventHandler<ExitPuzzleEvent>.UnregisterListener(TriggerCutScene);

    private void TriggerCutScene(ExitPuzzleEvent e) {

        if (e.success && e.info.ID == IDToListenFor)
            SceneManager.LoadScene(cutsceneName);

    }
    
}
