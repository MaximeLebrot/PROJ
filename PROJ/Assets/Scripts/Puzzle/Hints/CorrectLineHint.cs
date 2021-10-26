using UnityEngine;

public class CorrectLineHint : MonoBehaviour
{
    [SerializeField] PuzzleInstance puzzleInstance;
    [SerializeField] PuzzleGrid puzzleGrid;

    [SerializeField] LineRenderer hintLineRendererPrefab;

    private void Start()
    {
        if (puzzleInstance == null)
            puzzleInstance = GetComponentInChildren<PuzzleInstance>();
        if (puzzleGrid == null)
            puzzleGrid = GetComponentInChildren<PuzzleGrid>();

        foreach (SymbolModPair smp in puzzleInstance.puzzleObjects)
        {
            Debug.Log(smp.symbol.name);
        }
    }

    private void ShowHint()
    {

    }
}
