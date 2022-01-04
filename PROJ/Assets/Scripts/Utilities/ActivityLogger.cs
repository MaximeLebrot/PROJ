using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ActivityLogger : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    float lastCompletion = 0;
    void Start()
    {
        FileInitialize();
        Save();
    }
    private void OnEnable()
    {
        EventHandler<ActivatorEvent>.RegisterListener(OnPuzzleComplete);
    }
    private void OnDisable()
    {
        EventHandler<ActivatorEvent>.UnregisterListener(OnPuzzleComplete);
    }

    private void FileInitialize()
    {
        string[] rowDataTemp = new string[3];
        rowDataTemp[0] = "ID";
        rowDataTemp[1] = "Time since last puzzle completion";
        rowDataTemp[2] = "Total time to complete";
        rowData.Add(rowDataTemp);
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


        string filePath = getPath();

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    private void OnPuzzleComplete(ActivatorEvent eve)
    {
        Debug.Log("OnPuzzleComplete Log");
        int puzzleID = eve.info.ID;
        float time = Time.realtimeSinceStartup;
        AddDataPoint(puzzleID);
        Save();
    }
    private void AddDataPoint(int id)
    {
        string [] rowDataTemp = new string[3];
        rowDataTemp[0] = "" + id;
        rowDataTemp[1] = (Time.realtimeSinceStartup - lastCompletion).ToString();
        rowDataTemp[2] = Time.realtimeSinceStartup.ToString();
        rowData.Add(rowDataTemp);
        lastCompletion = Time.realtimeSinceStartup;
    }
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/StreamingAssets/" + "Log.csv";
#endif
    }
}