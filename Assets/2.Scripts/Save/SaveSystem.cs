using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SaveData
{
    public Save_MoneyData MoneyData;
    public Save_PlayerData PlayerData;
    public Save_PlayerEvenet PlayerEventData;
    //public Save_UnitData UnitData;
}

public class SaveSystem : MonoBehaviour
{
    public static string path; // 경로
    private static SaveData saveData = new();

    private void Awake()
    {
        path = Application.persistentDataPath + "/save";	// 경로 지정
        print(path);//경로디버그
    }

    public static bool SaveFile()
    {
        return File.Exists(path);
    }

    public static void Save()
    {
        saveData = new();

        GameManager.Instance.Save(ref saveData.MoneyData);
        GameManager.Instance.Player.Save(ref saveData.PlayerData);
        GameManager.Instance.PlayerEvent.Save(ref saveData.PlayerEventData);

        //if (isNewData)//true면
        //    GameManager.Instance.Player.PlayerNameSetting(name);
        //
        //GameManager.Instance.Player.Save(ref SaveData.PlayerData);
        //GameManager.Instance.DayCycleHandler.Save(ref SaveData.DayData);
        //GameManager.Instance.TileManager.Save(ref SaveData.TileData);
        //GameManager.Instance.NatureObjectController.Save(ref SaveData.NatureData, isNewData);
        //GameManager.Instance.Player.Inventory.Save(ref SaveData.InvenData, isNewData);
        ////SaveSceneData();
        //

        string data = JsonUtility.ToJson(saveData);

        if (File.Exists(path))
            File.Delete(path);

        File.WriteAllText(path, data);
    }

    public static void Load()
    {
        if (!File.Exists(path))//파일이 없으면
            return;

        string data = File.ReadAllText(path);
        saveData = new();
        saveData = JsonUtility.FromJson<SaveData>(data);

        SceneManager.sceneLoaded += SceneLoaded;
    }

    private static void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.Load(saveData.MoneyData);
        GameManager.Instance.Player.Load(saveData.PlayerData);
        GameManager.Instance.PlayerEvent.Load(saveData.PlayerEventData);

        //GameManager.Instance.Player.Load(SaveData.PlayerData);
        //GameManager.Instance.DayCycleHandler.Load(SaveData.DayData);
        //GameManager.Instance.TileManager.Load(SaveData.TileData);
        //GameManager.Instance.NatureObjectController.Load(SaveData.NatureData);
        //GameManager.Instance.Player.Inventory.Load(SaveData.InvenData);
        //LoadSceneData();

        SceneManager.sceneLoaded -= SceneLoaded;
    }

    #region Indoor/Outdoor나눌때
    //public static void SaveSceneData()
    //{
    //    if (GameManager.Instance.TileManager != null)
    //    {
    //        var sceneName = GameManager.Instance.LoadedSceneData.UniqueSceneName;
    //        var data = new SaveTileData();
    //        GameManager.Instance.TileManager.Save(ref data);
    //
    //        s_ScenesDataLookup[sceneName] = new SceneSaveData()
    //        {
    //            SceneName = sceneName,
    //            TerrainData = data
    //        };
    //    }
    //}
    //
    //public static void LoadSceneData()
    //{
    //    if (s_ScenesDataLookup.TryGetValue(GameManager.Instance.LoadedSceneData.UniqueSceneName, out var data))
    //    {
    //        GameManager.Instance.TileManager.Load(data.TerrainData);
    //    }
    //}

    #endregion


    public static void DataClear()
    {
        //nowSlot = -1;
        //SaveData = new();
        //slotData = new();
    }
}
