using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    private FMOD.Studio.EventInstance PuzzleSolved;
    private FMOD.Studio.EventInstance PuzzleStart;

    private void OnEnable()
    {
        //EventHandler<ExitPuzzleEvent>.RegisterListener(OnExitPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(OnStartPuzzle);
        //EventHandler<ClearPuzzleEvent>.RegisterListener(OnNextPuzzle);
        //EventHandler<ResetPuzzleEvent>.RegisterListener(OnResetPuzzle);
        EventHandler<ActivatorEvent>.RegisterListener(OnNextPuzzle);
    }
    private void OnDisable()
    {
        //EventHandler<ExitPuzzleEvent>.UnregisterListener(OnStartPuzzle);
        EventHandler<StartPuzzleEvent>.RegisterListener(OnStartPuzzle);
        //EventHandler<ClearPuzzleEvent>.RegisterListener(OnNextPuzzle);
        //EventHandler<ResetPuzzleEvent>.RegisterListener(OnResetPuzzle);
        EventHandler<ActivatorEvent>.RegisterListener(OnNextPuzzle);

    }
    //private void OnExitPuzzle(ExitPuzzleEvent eve)
    //{
        //PuzzleSolved = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Puzzle/PuzzleSolved");
        //PuzzleSolved.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //PuzzleSolved.start();
       //PuzzleSolved.release();
    //}

    private void OnStartPuzzle(StartPuzzleEvent eve)
    {
        PuzzleStart = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Puzzle/PuzzleStart");
        PuzzleStart.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PuzzleStart.start();
        PuzzleStart.release();
    }

    private void OnNextPuzzle(ActivatorEvent eve)
    {
        PuzzleStart = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Puzzle/SectionSolved");
        PuzzleStart.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PuzzleStart.start();
        PuzzleStart.release();
    }

    //private void OnResetPuzzle(ResetPuzzleEvent eve)
    //{
        //PuzzleStart = FMODUnity.RuntimeManager.CreateInstance("event:/Game/Puzzle/PuzzleExit");
        //PuzzleStart.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //PuzzleStart.start();
        //PuzzleStart.release();
    //}
}

