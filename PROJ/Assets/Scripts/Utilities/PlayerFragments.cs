using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFragments : MonoBehaviour
{
    private DictionaryOfStringAndBool collectedFragments = new DictionaryOfStringAndBool();

    void Start()
    {
        collectedFragments.Add("tutorial", false);
        collectedFragments.Add("earth", false);
        collectedFragments.Add("wind", false);
        collectedFragments.Add("lava", false);
    }

    void Load()
    {
        //fetch collected fragments from savefile
    }

    public void AddFragment(string nameOfFragment)
    {
        collectedFragments[nameOfFragment] = true;
    }

    public bool DepositFragment(string fragmentToCheck)
    {
        if (collectedFragments[fragmentToCheck])
        {
            collectedFragments[fragmentToCheck] = false;
            return true;
        }

        return false;

    }

    public bool CheckForFragment(string fragmentToCheck)
    {
        return collectedFragments[fragmentToCheck];
    }
}


[Serializable]
public class DictionaryOfStringAndBool : SerializableDictionary<string, bool> { }