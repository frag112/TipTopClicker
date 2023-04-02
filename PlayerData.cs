[System.Serializable]
public class PlayerData
{

    public int currentMultiplier, currentStorageCapacity, currentAutoSpeed, currentPrestigeBonus;
    public int currentAmoutCliks, currentStorage; // сколько накликал, объем хранилища, сколько сейчас в хранилище
    public int clickUpgradeID, nextUpgradeClicksCost,nextClicksAmount, nextUpgradeStorageAmount, nextUpgradeAutoSpeed; // какие прокачки куплены по кнопке, хранилищу и скорости, определяется по ИД прокачки
    public int currentPrestige, currentLvl, currentLevelProgress, requiredXP; // какой престиж, какой уровень, сколько накоплено для получения следущего уровня 
    public int globalAmountClicks;// сколько игрок накликал с первого запуска игры
    public bool storeAvailable;// gameRated;

    public PlayerData()
    {
        clickUpgradeID = 1;
        storeAvailable=false;
        currentMultiplier = 1;
        currentLvl = 1;
    }

}