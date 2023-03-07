using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagement;

public class DataHandler : MonoBehaviour
{
    public SavingSystem savingSystem;
    public static DataHandler Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
public void LoadData(){

}
public void SaveData(){

}
public void BuildPrestigeLeaderboard(){
 // берет данные с сервера и возвращает таблицу в нужном формате
}
public void BuildClickLeaderboard(){
// берет данные с сервера и возвращает таблицу в нужном формате
}

}
public enum SavingSystem
{
DefaultSaver,
YandexSaver
}