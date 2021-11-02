using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveNameGenerator
{
    static int currentNrOfSaves;

    static string saveName = "save ";

    public static string GetNewSaveName()
    {
        currentNrOfSaves++;
        return saveName + currentNrOfSaves;
    }

    public static string GetCurrentSaveName()
    {
        return saveName + currentNrOfSaves;
    }


}
