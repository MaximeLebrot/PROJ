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
        //LoadMostRecent(); //Continue button should call this
    }

    private void OnDisable()
    {
        EventHandler<SaveEvent>.UnregisterListener(OnSave);
        //Save();
    }

    void OnSave(SaveEvent eve)
    {
        if (currentSaveName == null)
            currentSaveName = SaveNameGenerator.GetNewSaveName();


        SaveData(currentSaveName);
    }

    public void Save()
    {
        currentSaveName = SaveNameGenerator.GetNewSaveName();
        SaveData(currentSaveName);

    }

    private void SaveData(string saveName)
    {
        SaveData saveData = new SaveData();


        saveData.allPuzzles = PuzzleDictionary.GetPuzzles();
        saveData.PlayerPos = FindObjectOfType<MetaPlayerController>().transform.position;
        saveData.PlayerRot = FindObjectOfType<MetaPlayerController>().transform.rotation;

        //Checks if this saveFile already exists, if so, overwrite it.
        if (savesHolder.saves.Contains(saveName) == false)
        {
            savesHolder.saves.Add(saveName, saveData);
        }
        else
        {
            savesHolder.saves[saveName] = saveData;
        }

        Debug.Log(saveName);
        savesHolder.currentNrOfSaves++;
        var json = JsonUtility.ToJson(savesHolder);
        PlayerPrefs.SetString("SaveData", json);

    }


    public void Load(string saveName)
    {
        SaveData saveData = new SaveData();

        string json = PlayerPrefs.GetString("SaveData");

        if(json == "")
        {
            Debug.LogError("FAILED TO LOAD");
            return;
        }

        savesHolder = JsonUtility.FromJson<SaveDataHolder>(json);

        if(savesHolder.saves.Contains(saveName) == false)
        {
            Debug.LogError("COULD NOT FIND SAVE FILE");
            return;
        }

        currentSaveName = saveName;

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

        Debug.Log("LOAD : " + currentSaveName);

        //WHAT HAPPENS if we were inside a puzzle when we loaded or saved? always load outside puzzle state?

    }

    public void Reload()
    {
        SaveData saveData = new SaveData();

        string json = PlayerPrefs.GetString("SaveData");

        if (json == "")
        {
            Debug.LogError("FAILED TO LOAD");
            return;
        }

        savesHolder = JsonUtility.FromJson<SaveDataHolder>(json);

        if (savesHolder.saves.Contains(currentSaveName) == false)
        {
            Debug.LogError("COULD NOT FIND SAVE FILE");
            return;
        }

        saveData = savesHolder.saves[currentSaveName];


        PuzzleDictionary.SetPuzzles(saveData.allPuzzles);


        foreach (var puzzleInstance in FindObjectsOfType<PuzzleInstance>())
        {
            puzzleInstance.Load();
        }

        foreach (Puzzle puzzle in FindObjectsOfType<Puzzle>())
        {
            puzzle.Load();
        }

        FindObjectOfType<MetaPlayerController>().transform.position = saveData.PlayerPos;
        FindObjectOfType<MetaPlayerController>().transform.rotation = saveData.PlayerRot;

        Debug.Log("RELOAD : " + currentSaveName);

        //WHAT HAPPENS if we were inside a puzzle when we loaded or saved? always load outside puzzle state?

    }

    private void LoadMostRecent()
    {
        SaveData saveData = new SaveData();

        string json = PlayerPrefs.GetString("SaveData");

        if (json == "")
        {
            Debug.LogError("FAILED TO LOAD");
            return;
        }

        savesHolder = JsonUtility.FromJson<SaveDataHolder>(json);

        if (savesHolder.saves.Count < 1)
        {
            Debug.LogError("COULD NOT FIND SAVE FILE");
            return;
        }

        saveData = savesHolder.saves.Last().Value;
        currentSaveName = savesHolder.saves.Last().Key;

        PuzzleDictionary.SetPuzzles(saveData.allPuzzles);


        foreach (var puzzleInstance in FindObjectsOfType<PuzzleInstance>())
        {
            puzzleInstance.Load();
        }

        foreach (Puzzle puzzle in FindObjectsOfType<Puzzle>())
        {
            puzzle.Load();
        }

        FindObjectOfType<MetaPlayerController>().transform.position = saveData.PlayerPos;
        FindObjectOfType<MetaPlayerController>().transform.rotation = saveData.PlayerRot;

        Debug.Log("Load Most Recent : " + currentSaveName);

        //WHAT HAPPENS if we were inside a puzzle when we loaded or saved? always load outside puzzle state?
    }
}
