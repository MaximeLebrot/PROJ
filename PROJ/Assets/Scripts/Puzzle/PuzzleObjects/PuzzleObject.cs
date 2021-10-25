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
    [SerializeField] private GameObject modHolder;

    //[HideInInspector]
    [SerializeField] private ModifierVariant modVariant;
    

    private ModInfo modInfo;
    private Image modifierImage; //dekal som ska visas någonstans!?!? HUR GÖR MAN
    private GameObject modifier;

    private void OnEnable()
    {
        
    }


    public string GetTranslation()
    {
        return AdjustForModifiers();
    }

    protected string AdjustForModifiers()
    {
        string modifiedString = "";
        
        if(modInfo != null)
        {
            modifiedString += modInfo.translation;
        }

        Debug.Log(modifiedString + translation);
        //CANNOT COMBINE MODIFIERS RIGHT NOW
        return modifiedString + translation;
    }


    public void SetModifier(ModifierVariant modVar)
    {
        if (modifier != null)
            Destroy(modifier);
        modInfo = modHolder.GetComponent<ModifierHolder>().GetModifier(modVar);
        modifier = Instantiate(modInfo.modifier);
        modifier.transform.parent = transform;
        modifier.transform.localPosition = modifierPosition;
        modifier.transform.rotation = transform.rotation;
    }

}

public enum ModifierVariant
{
    None,
    Rotate,
    Repeat, 
    Mirrored, 
    Double
}
