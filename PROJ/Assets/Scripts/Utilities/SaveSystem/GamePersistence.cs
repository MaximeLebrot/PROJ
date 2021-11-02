using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePersistence : MonoBehaviour
{
    private string currentSaveName;

    

    private SaveDataHolder savesHolder = new SaveDataHolder();

    private void OnEnable()
    {
        EventHandler<SaveEvent>.RegisterListener(OnSave);
        //Load();
    }

    private void OnDisable()
    {
        EventHandler<SaveEvent>.UnregisterListener(OnSave);
        //Save();
    }

    void OnSave(SaveEvent eve)
    {
        Save(currentSaveName);
    }

    public void Save(string saveName)
    {
        SaveData saveData = new SaveData();

        if (saveName != currentSaveName)
            currentSaveName = saveName;


        saveData.allPuzzles = PuzzleDictionary.GetPuzzles();
        saveData.PlayerPos = FindObjectOfType<MetaPlayerController>().transform.position;
        saveData.PlayerRot = FindObjectOfType<MetaPlayerController>().transform.rotation;

        //Checks if this saveFile already existst, if so, overwrite it.
        if(savesHolder.saves.Contains(currentSaveName) == false)
        {
            savesHolder.saves.Add(currentSaveName, saveData);
        }
        else
        {
            savesHolder.saves[currentSaveName] = saveData;
        }


        savesHolder.currentNrOfSaves++;

        var json = JsonUtility.ToJson(savesHolder);
        Debug.Log(saveName);
        PlayerPrefs.SetString("SaveData", json);

    }


    public void Load(string saveName)
    {
        SaveData saveData = new SaveData();

        //Update the current save file
        string json = PlayerPrefs.GetString("SaveData");
        if(json == "")
        {
            Debug.LogError("FAILED TO LOAD");
            return;
        }

        savesHolder = JsonUtility.FromJson<SaveDataHolder>(json);

        if(savesHolder.saves.Contains(saveName) == true)
        {
            currentSaveName = saveName;
        }

        saveData = savesHolder.saves[currentSaveName];


        PuzzleDictionary.SetPuzzles(saveData.allPuzzles);
        

        foreach (var puzzleInstance in FindObjectsOfType<PuzzleInstance>())
        {
            puzzleInstance.Load();
        }

        foreach(Puzzle puzzle in FindObjectsOfType<Puzzle>())
        {
            puzzle.Load();
        }

        FindObjectOfType<MetaPlayerController>().transform.position = saveData.PlayerPos;
        FindObjectOfType<MetaPlayerController>().transform.rotation =  saveData.PlayerRot;

        //WHAT HAPPENS if we were inside a puzzle when we loaded or saved? always load outside puzzle state?

    }
}
