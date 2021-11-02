using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePersistance : MonoBehaviour
{
    private string currentSaveName;

    private GameData gameData = new GameData();
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
        if (saveName != currentSaveName)
            currentSaveName = saveName;

        gameData.allPuzzles = PuzzleDictionary.GetPuzzles();
        gameData.PlayerPos = FindObjectOfType<MetaPlayerController>().transform.position;
        gameData.PlayerRot = FindObjectOfType<MetaPlayerController>().transform.rotation;

        

        var json = JsonUtility.ToJson(gameData);
        Debug.Log(json);
        PlayerPrefs.SetString("GameData" + currentSaveName, json);

        
    }
    public void Load(string saveName)
    {
        //Update the current save file
        currentSaveName = saveName;

        string json = PlayerPrefs.GetString("GameData" + currentSaveName);

        Debug.Log(json);
        gameData = JsonUtility.FromJson<GameData>(json);

        Debug.Log(gameData.allPuzzles.Count);

        PuzzleDictionary.SetPuzzles(gameData.allPuzzles);

        foreach (var puzzleInstance in FindObjectsOfType<PuzzleInstance>())
        {
            puzzleInstance.Load();
        }

        foreach(Puzzle puzzle in FindObjectsOfType<Puzzle>())
        {
            puzzle.Load();
        }

        
    }
}
