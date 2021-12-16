using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    private FMOD.Studio.EventInstance PuzzleSolved;

    private void OnEnable()
    {
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnExitPuzzle);
    }
    private void OnDisable()
    {
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnExitPuzzle);
    }
    private void OnExitPuzzle(ExitPuzzleEvent eve)
    {
        /*
        PuzzleSolved = FMODUnity.RuntimeManager.CreateInstance("event:/Game/PuzzleSolved");
        PuzzleSolved.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PuzzleSolved.start();
        PuzzleSolved.release();
        */
    }
}

