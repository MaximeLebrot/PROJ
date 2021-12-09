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

    [SerializeField] private List<Material> materials_EASY_MEDIUM_HARD = new List<Material>();
    private Dictionary<string, Material> materialsByDifficulty = new Dictionary<string, Material>();
    

    private ModInfo modInfo;
    private Image modifierImage; //dekal som ska visas någonstans!?!? HUR GÖR MAN
    private GameObject modifier;
    private Animator anim;
    private MeshRenderer mesh;
    public bool Active { get; private set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        //SetUpMaterials();
        Debug.Log(mesh.material);
    }

    private void SetUpMaterials()
    {
        materialsByDifficulty.Add("easy", materials_EASY_MEDIUM_HARD[0]);
        materialsByDifficulty.Add("medium", materials_EASY_MEDIUM_HARD[1]);
        materialsByDifficulty.Add("hard", materials_EASY_MEDIUM_HARD[2]);
    }

    private void SetMaterialBasedOnDifficulty(string difficulty)
    {
        mesh.material = materialsByDifficulty[difficulty];
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
        modifier.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        modifier.transform.localPosition = modifierPosition;
        modifier.transform.rotation = transform.rotation;
    }

    internal void Unload()
    {
        Invoke("DestroyPuzzleObject", 2);
    }

    private void DestroyPuzzleObject()
    {
        anim.SetTrigger("off");

        if (modInfo.variant != ModifierVariant.None == true)
        {
            modifier.GetComponent<Animator>().SetTrigger("off");
        }
        Destroy(gameObject, 2);
    }
    internal void TurnOn()
    {
        Debug.Log(gameObject + " ON ");
    }
    internal void TurnOff()
    {
        Debug.Log(gameObject + " OFF ");
    }

    internal void Activate(bool hasBeenSolved)
    {
        if(Active != hasBeenSolved)
        {
            Debug.Log(gameObject + "  " + hasBeenSolved);
            Active = hasBeenSolved;

            if(hasBeenSolved == true)
            {
                anim.SetTrigger("activate");

                if (modInfo.variant != ModifierVariant.None == true)
                {
                    modifier.GetComponent<Animator>().SetTrigger("activate");
                }
            }
            else
            {
                anim.SetTrigger("deactivate");

                if (modInfo.variant != ModifierVariant.None == true)
                {
                    modifier.GetComponent<Animator>().SetTrigger("deactivate");
                }
            }
            
        }

        

    }
}

public enum ModifierVariant
{
    None,
    Mirrored, 
    Double,
    Rotate90,
    Rotate180,
    Rotate270,
    RepeatOPEN,
    RepeatCLOSE
}
