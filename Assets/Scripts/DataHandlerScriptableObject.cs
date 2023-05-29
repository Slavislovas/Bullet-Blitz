using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "DataHandler", menuName = "ScriptableObjects/DataHandlerScriptableObject", order = 1)]
public class DataHandlerScriptableObject : ScriptableObject
{
    private string DataDirPath;
    private string DataFileName;
    public Data SavedData;
    public bool PlayingNormalMode = true;

    public void Initialize()
    {
            DataDirPath = Application.persistentDataPath;
            DataFileName = "save.json";

            string fullPath = Path.Combine(DataDirPath, DataFileName);

            if (!File.Exists(fullPath))
            {
                SavedData = new Data();

                FileStream saveFile = File.Create(fullPath);
                saveFile.Close();

                string saveFileInitialData = SavedData.ToJson();
                File.WriteAllText(fullPath, saveFileInitialData);
            }

            string savedDataJson = File.ReadAllText(fullPath);
            SavedData = JsonUtility.FromJson<Data>(savedDataJson);
    }

    public void SaveCompletedNormalMode(string levelName)
    {
        switch (levelName)
        {
            case "Forest":
                SavedData.isCompletedForestNormalMode = true;
                break;
            case "Dungeon":
                SavedData.isCompletedDungeonNormalMode = true;
                break;
        }
        string fullPath = Path.Combine(DataDirPath, DataFileName);
        string newSaveData = SavedData.ToJson();
        File.WriteAllText(fullPath, newSaveData);
    }
}
