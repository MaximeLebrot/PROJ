using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleObjectModifier : MonoBehaviour
{
    [SerializeField] private Sprite image; //modifier image to display
    [SerializeField] private char modifierTranslation;

    public char GetModifier() { return modifierTranslation; }

}
