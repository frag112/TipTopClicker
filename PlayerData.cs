using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{

    public int currentAmoutCliks, storageCapacity, currentStorageAmount; // сколько накликал, объем хранилища, сколько сейчас в хранилище
    public int upgradeButtonId, upgradeStorageAmountId, upgradeAutoSpeedId; // какие прокачки куплены по кнопке, хранилищу и скорости, определяется по ИД прокачки
    public int currentPrestige, currentLvl, currentLevelProgress; // какой престиж, какой уровень, сколько накоплено для получения следущего уровня 
    public int globalAmountClicks;// сколько игрок накликал с первого запуска игры

}