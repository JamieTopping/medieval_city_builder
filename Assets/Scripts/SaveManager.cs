using System.IO;
using UnityEngine;

public class SaveManager
{
    private static int saveSlot = 0;
    private static SaveData _saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public TileManagerData tileManagerData;
        public GuyManagerData guyManagerData;
    }

    public static void AutoSave()
    {
        Save(0);
        // display autosave message
    }

    private static string SaveFileName()
    {
        // for build must replace file location with Application.persistentDataPath
        string saveFileName;
        if (saveSlot < 0)
        {
            saveFileName = "/Users/jamietopping/Documents/Jamie Home/medieval_city_builder/Assets/Persistant Data" + "/DevSave" + ".txt";
        } else if (saveSlot == 0)
        {
            saveFileName = "/Users/jamietopping/Documents/Jamie Home/medieval_city_builder/Assets/Persistant Data" + "/AutoSave" + ".txt";
        } else
        {
            saveFileName = "/Users/jamietopping/Documents/Jamie Home/medieval_city_builder/Assets/Persistant Data" + "/Save" + saveSlot + ".txt";
        }
        Debug.Log(saveFileName);
        return saveFileName;
    }

    public static void Save(int targetSaveSlot)
    {
        saveSlot = targetSaveSlot;
        HandleSaveData();
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandleSaveData()
    {
        GuyManager._instance.Save(ref _saveData.guyManagerData);
        TileManager._instance.Save(ref _saveData.tileManagerData);
    }
    
    public static void Load(int targetSaveSlot)
    {
        saveSlot = targetSaveSlot;
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        GuyManager._instance.Load(_saveData.guyManagerData);
        TileManager._instance.Load(_saveData.tileManagerData);
    }
}
