using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ActivityLogger : MonoBehaviour
{
    private string [] data = new string[25];
    private List<string[]> rowData = new List<string[]>();
    float lastCompletion = 0;
    int fileNumber = 0;
    int dataPointCounter = 1;

    void Start()
    {
        FileInitialize();
        Save();
    }
    private void OnEnable()
    {
        EventHandler<ActivatorEvent>.RegisterListener(OnPuzzleComplete);
        EventHandler<UnLoadSceneEvent>.RegisterListener(OnSceneLoaded);
    }
    private void OnDisable()
    {
        EventHandler<ActivatorEvent>.UnregisterListener(OnPuzzleComplete);
        EventHandler<UnLoadSceneEvent>.UnregisterListener(OnSceneLoaded);
    }

    private void FileInitialize()
    {
        while (File.Exists(getPath(fileNumber)))
        {
            fileNumber++;
        }

        int numberOfColumns = 20;

        List<string> tempList = new List<string>(); 
        for (int i = 1; i < numberOfColumns; i++)
        {
            tempList.Add("ID " + i);
        }

        //Scene breaks
        tempList.Insert(0, "PuzzleID");
        tempList.Insert(3, "Earth Ruin");
        tempList.Insert(9, "Wind Ruin");
        tempList.Insert(17, "Lava Ruin");
        
        string [] rowDataTemp = tempList.ToArray();
        rowData.Add(rowDataTemp);
        rowData.Add(data);
    }
    void Save()
    {
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath(fileNumber);

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    private void OnPuzzleComplete(ActivatorEvent eve)
    {
        Debug.Log("OnPuzzleComplete Log");
        AddDataPoint(eve.info.ID);
        Save();
    }
    private void OnSceneLoaded(UnLoadSceneEvent eve)
    {
        //magic numbers for correct positions in the resulting log file
        switch(eve.sceneToLoad)
        {
            case "TutorialMainHub":
                dataPointCounter = 1;
                break;
            case "EarthRuin_2.0":
                dataPointCounter = 4;
                break;
            case "WindRuin_3.0":
                dataPointCounter = 10;
                break;
            case "LavaRuin_Beta":
                dataPointCounter = 18;
                break;
        };
    }
    private void AddDataPoint(int id)
    {
        data[dataPointCounter++] = (Time.realtimeSinceStartup - lastCompletion).ToString();
        lastCompletion = Time.realtimeSinceStartup;
        rowData[1] = data;
    }
    private string getPath(int i)
    {
        return Application.dataPath + "/StreamingAssets/" + "Log("+ i +").csv";
    }
}