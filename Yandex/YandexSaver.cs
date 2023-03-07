using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using DataManagement;
// задачи у класса -
// сохранять игру на сервер яндекса после каждого клика
// загружать при запуске
// загружать две таблицы рекордов по требованию
public class YandexSaver : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SavePlayerData(string data);
    [DllImport("__Internal")]
    private static extern void LoadPlayerData();
    public PlayerData _data;
    [DllImport("__Internal")]
    private static extern void GiveMePlayerData();
    [DllImport("__Internal")]
    private static extern void RateGame();

    public static YandexSaver Instance;
    //public TextMeshProUGUI _nameText;  передать эти и другие значения в таблицу рекордов
    //public RawImage _photo;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            LoadPlayerData();
        }

    }
    public void HelloButton()
    {
        GiveMePlayerData();// поменять название
        RateGame(); //  вызывает окошко яндекса для рейтинга и рецензии, можно вызвать один раз
    }
    /* методы для подбора картинки и имен игроков пригодятся для таблицы рекордов
    public void SetName(string name)
    {
        _nameText.text = name;  // recieve player name from yandex
    }

    public void SetPhoto(string url)  // recieve player icon from yandex
    {
        StartCoroutine(DownloadImage(url));
    }
    IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            _photo.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
*/


    public void SaveData()
    {
        string jsonString = JsonUtility.ToJson(_data);
        SavePlayerData(jsonString);
    }
    public void LoadData(string value)
    {
        _data = JsonUtility.FromJson<PlayerData>(value);

    }
}
