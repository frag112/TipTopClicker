using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System.IO;
public class DataHandler : MonoBehaviour
{
    [SerializeField] float saveInterval = 12f;
    [SerializeField] string SaveName = "Save";
    public static DataHandler Instance;

    public PlayerData data;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            LoadData();
            StartCoroutine(SavingRoutine());
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void LoadData()
    {
        string fileContent = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + SaveName + ".json");
        data = JsonUtility.FromJson<PlayerData>(fileContent);
    }
    IEnumerator SavingRoutine()
    {
        while (true)
        {
            SaveData();
            yield return new WaitForSeconds(saveInterval);
        }
    }
    public void SaveData()
    {
#if UNITY_EDITOR
        string fileContent = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + SaveName + ".json";
        File.WriteAllText(filePath, fileContent);
#elif UNITY_WEBGL
    Debug.Log("Unity WebGL saving data man, saving it all!");
#endif
    }
    public void BuildPrestigeLeaderboard()
    {
        // берет данные с сервера и возвращает таблицу в нужном формате
    }
    public void BuildClickLeaderboard()
    {
        // берет данные с сервера и возвращает таблицу в нужном формате
    }

}