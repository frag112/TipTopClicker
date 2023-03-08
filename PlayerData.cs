using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{

    public int currentMultiplier, currentStorageCapacity, currentAutoSpeed;
    public int currentAmoutCliks, currentStorage; // сколько накликал, объем хранилища, сколько сейчас в хранилище
    public int nextUpgradeButton, nextUpgradeStorageAmount, nextUpgradeAutoSpeed; // какие прокачки куплены по кнопке, хранилищу и скорости, определяется по ИД прокачки
    public int currentPrestige, currentLvl, currentLevelProgress, requiredXP; // какой престиж, какой уровень, сколько накоплено для получения следущего уровня 
    public int globalAmountClicks;// сколько игрок накликал с первого запуска игры

    public PlayerData()
    {
        currentMultiplier = 18;
        currentPrestige = 1;
        currentLvl = 1;
    }

}