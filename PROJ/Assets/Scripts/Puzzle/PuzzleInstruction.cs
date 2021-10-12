using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/Instructions")]
public class PuzzleInstruction : ScriptableObject
{

    [SerializeField] protected List<string> instructions = new List<string>();

    public virtual List<string> GetInstructions() { return instructions; }
}
