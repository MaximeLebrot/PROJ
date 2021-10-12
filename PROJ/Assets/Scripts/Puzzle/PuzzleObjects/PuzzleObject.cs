using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public abstract class PuzzleObject : MonoBehaviour
{
    [SerializeField] protected string translation;
    [SerializeField] private Vector3 modifierPosition;
    [SerializeField] private ModifierHolder modifier;

    [HideInInspector]
    [SerializeField] private ModifierVariant modVariant;
    

    private ModifierInfo modInfo;
    private Image modifierImage; //dekal som ska visas någonstans!?!? HUR GÖR MAN

    private void Start()
    {
        
    }

    private void PlaceModifier()
    {
        //modifier.transform.position = modifierPosition.position;
    }

    public string GetTranslation()
    {
        return AdjustForModifiers();
    }

    protected string AdjustForModifiers()
    {
        modInfo = modifier.GetModifier(modVariant);

        string modifiedString = "";
        
        if(modInfo != null)
        {
            modifiedString += modInfo.ModifierTranslation;
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
