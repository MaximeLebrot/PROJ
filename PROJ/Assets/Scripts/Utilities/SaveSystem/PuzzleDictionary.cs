using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PuzzleDictionary
{


    private static DictionaryOfIntAndBool allPuzzles = new DictionaryOfIntAndBool(); /*puzzleID & puzzleSuccess*/

    public static DictionaryOfIntAndBool GetPuzzles() { return allPuzzles; }
    public static void SetPuzzles(DictionaryOfIntAndBool puzzles) 
    {  
        allPuzzles = puzzles; 
        
    }


    public static bool GetState(int id) 
    {
        if (allPuzzles.ContainsKey(id))
        {
            return allPuzzles[id];
        }
        else
        {
            allPuzzles.Add(id, false);
            return allPuzzles[id];
        }
    }
    public static void AddPuzzle(int id)
    {
        if(allPuzzles.ContainsKey(id) == false)
        {
            allPuzzles.Add(id, false);
        }
    }

    internal static void SetState(int puzzleID, bool currentState)
    {
        if (allPuzzles.ContainsKey(puzzleID))
        {
            allPuzzles[puzzleID] = currentState;
        }
        else
        {
            allPuzzles.Add(puzzleID, currentState);
        }
    }

    public static int GetCount() { return allPuzzles.Count; }

    

    
}

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }

    public bool Contains(TKey key)
    {
        foreach(TKey k in keys)
        {
            if(k.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
}

[Serializable] public class DictionaryOfIntAndBool : SerializableDictionary<int, bool> { }