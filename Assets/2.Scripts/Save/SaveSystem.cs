using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SaveData
{
    public Save_GameData MoneyData;
    public Save_PlayerData PlayerData;
    public Save_PlayerEvenet PlayerEventData;
    public Save_UnitData UnitData;
    public Save_TimeData TimeData;
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

    public static bool SaveFile()//파일있나없나
    {
        return File.Exists(path);
    }

    private static void DataReset()//세이브데이타 new
    {
        saveData = new();

        saveData.MoneyData = new();
        saveData.PlayerData = new();
        saveData.PlayerEventData = new();
        saveData.UnitData = new();
        saveData.TimeData = new();
    }

    public static void Save()
    {
        DataReset();

        GameManager.Instance.Save(ref saveData.MoneyData);
        GameManager.Instance.Player.Save(ref saveData.PlayerData);
        GameManager.Instance.PlayerEvent.Save(ref saveData.PlayerEventData);
        GameManager.Instance.UnitManager.Save(ref saveData.UnitData);
        GameManager.Instance.TimeManager.Save(ref saveData.TimeData);

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
        GameManager.Instance.UnitManager.Load(saveData.UnitData);
        GameManager.Instance.TimeManager.Load(saveData.TimeData);

        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
