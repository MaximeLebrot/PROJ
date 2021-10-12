using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Puzzle/ObjectModifierHolder")]
public class ModifierHolder : ScriptableObject
{

    [SerializeField] private List<KeyValuePair> modifiers = new List<KeyValuePair>();

    public ModifierInfo GetModifier(ModifierVariant var) 
    { 
        foreach(KeyValuePair kv in modifiers)
        {
            if (kv.variant == var)
                return kv.info;
        }

        return null;
    }

}

[System.Serializable]
public class KeyValuePair
{
    public ModifierVariant variant;
    public ModifierInfo info;
}



