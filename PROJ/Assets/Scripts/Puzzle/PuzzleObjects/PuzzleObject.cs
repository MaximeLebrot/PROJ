using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PuzzleObject : MonoBehaviour
{
    [SerializeField] protected string translation;
    [SerializeField] private Transform modifierPosition;
    [SerializeField] protected ModifierInfo modifier;
    public ModifierVariant modVariant;



    private void Start()
    {
        //This should happen when the puzzle loads
        //PlaceModifier();
    }

    private void PlaceModifier()
    {
        //modifier.transform.position = modifierPosition.position;
    }

    public string GetTranslation()
    {
        translation = AdjustForModifiers();
        return translation;
    }

    protected string AdjustForModifiers()
    {
        string modifiedString = "";
        
        if(modifier.ModifierTranslation.Equals(""))
        {
            modifiedString += modifier.ModifierTranslation;
        }

        //CANNOT COMBINE MODIFIERS RIGHT NOW
        return modifiedString + translation;
    }

    public abstract void Activate();

}

public enum ModifierVariant
{
    None,
    Rotate,
    Repeat, 
    Mirrored, 
    Ignore
}
