using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Camera/Type Dictionary")]
public class TypeNameDictionary : ScriptableObject {

    [SerializeField] private Dictionary<int, HashSet<string>> typeDictionary = new Dictionary<int, HashSet<string>>();

    public bool AddType(int ID, string newName) {
        if (TypeExists(ID, newName) == false)
            return false;
        
        typeDictionary[ID].Add(newName);
        return true;
    }

    public bool RemoveType(int ID, string name) {
        if (TypeExists(ID, name)) 
            return typeDictionary[ID].Remove(name);
        
        return false;
    }
    
    public bool TypeExists(int ID, string name) {

        return typeDictionary.ContainsKey(ID) ? typeDictionary[ID].Contains(name) : false;

    }

    public HashSet<string> GetTypeList(int ID) {

        if (typeDictionary.ContainsKey(ID))
            return typeDictionary[ID];

        return null;
    }

    public HashSet<string> GetAllCombinedTypeLists(bool excludeID, int excludedID) {

        HashSet<string> newSet = new HashSet<string>();

        foreach (KeyValuePair<int, HashSet<string>> lists in typeDictionary) {

            if (excludeID && lists.Key == excludedID)
                continue;
            
            foreach (string name in lists.Value)
                newSet.Add(name);
        }
        
        return newSet;
    }
}




