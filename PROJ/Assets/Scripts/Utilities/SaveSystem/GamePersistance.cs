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

    public void Save(string gameName)
    {
        

        gameData.allPuzzles = PuzzleDictionary.GetPuzzles();
        gameData.PlayerPos = FindObjectOfType<MetaPlayerController>().transform.position;
        gameData.PlayerRot = FindObjectOfType<MetaPlayerController>().transform.rotation;

        var json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData" + gameName, json);

        
    }
    public void Load(string gameName)
    {

        string json = PlayerPrefs.GetString("GameData" + gameName);
        gameData = JsonUtility.FromJson<GameData>(json);

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
