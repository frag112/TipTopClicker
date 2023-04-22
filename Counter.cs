using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Counter : MonoBehaviour
{
    [Header("Bonus Multipliers")]
    bool levelUpBonus;
    public float frenzyThreshold = 0.8f;
    private float lastClickTime, interval, frenzyCounter;
    //public float timeThreshold, frenzyBar;// frenzy bar is  invisible bar that can be filled by pressing button
    int totalMultiplier;

    [Header("Level Multipliers")]
    [Range(1, 500)]
    [SerializeField] private int addintionalMultiplier = 300;
    [Range(2, 6)]
    [SerializeField] private int powerMultiplier = 2;
    [Range(2, 20)]
    [SerializeField] private float divisionMultiplier = 7f;
    public int clickCost, clickUpgrade;
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public static event ClickAction OnLvlUp;
    public static event ClickAction OnlvlHundred;
    public static event ClickAction OnStoreUpdate;
    void Start()
    {
        DataHandler.Instance.data.requiredXP = CalculateRequiredXP();
        CalculateClickUpgradeCost();
    }
    // public void GameRated()
    // {
    //     DataHandler.Instance.data.gameRated = true;
    // }
    void Update()
    {
        // timeThreshold += Time.deltaTime;
        // if (timeThreshold > .5f)
        // {
        //     timeThreshold = 0;
        //     frenzyBar -= .30f;
        // }
        // if (frenzyBar < 0) frenzyBar = 0;
        // if (frenzyBar > 5) frenzyBar = 5;
        // if (frenzyBar > 1.9f) DataHandler.Instance.frenzy = true;
        // else DataHandler.Instance.frenzy = false;
        interval = Time.time - lastClickTime;
        if (interval > frenzyThreshold)
        {
            frenzyCounter = 0;
        }

        if (frenzyCounter <20)
        {
          DataHandler.Instance.frenzy = false;  
          DataHandler.Instance.doubleFrenzy = false;
        } 
        else if(frenzyCounter > 60) DataHandler.Instance.doubleFrenzy = true;
        else DataHandler.Instance.frenzy = true;

    }
    public void UpdateCounter()
    {
        interval = Time.time - lastClickTime;
        if (interval < frenzyThreshold)
        {
            frenzyCounter++;
        }
        lastClickTime = Time.time;

        CalculateFormula();
        DataHandler.Instance.data.currentAmoutCliks += totalMultiplier;
        DataHandler.Instance.data.globalAmountClicks += totalMultiplier;
        GainXP();
        if (OnClicked != null)
        {
            OnClicked();
        }
    }
    void CalculateFormula()
    {
        var baseValue = DataHandler.Instance.data.currentMultiplier * 1;
        // if (DataHandler.Instance.data.gameRated)
        // {
        //     baseValue += (int)(DataHandler.Instance.data.currentMultiplier * .1f);
        // }
        totalMultiplier = baseValue;
        if (DataHandler.Instance.data.currentPrestigeBonus != 0)
        {
            totalMultiplier += (int)(baseValue * (DataHandler.Instance.data.currentPrestigeBonus / 100f));
        }
        if (levelUpBonus)
        {
            totalMultiplier += (int)(baseValue * 0.45f);
        }
        //if (frenzy bonus) add more frenzy points
        if (frenzyCounter > 35) totalMultiplier += (int)(baseValue * .35f);

        else if (frenzyCounter > 70) totalMultiplier += (int)(baseValue * .7f);
    }
    void CalculatePrestigeBonus()
    {
        if (DataHandler.Instance.data.currentPrestige > 0)
        {
            if (DataHandler.Instance.data.currentPrestige == 1)
            {
                DataHandler.Instance.data.currentPrestigeBonus = 80;
            }
            else if (DataHandler.Instance.data.currentPrestigeBonus < 10000)
            {
                DataHandler.Instance.data.currentPrestigeBonus += 30;
            }
        }
    }
    void GainXP()
    {
        DataHandler.Instance.data.currentLevelProgress += totalMultiplier;
        while (DataHandler.Instance.data.currentLevelProgress >= DataHandler.Instance.data.requiredXP)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        if (DataHandler.Instance.data.currentLvl <= 99)
        {
            DataHandler.Instance.data.currentLvl++;
            StartCoroutine(LevelUpBonusCountDown());
            if (DataHandler.Instance.data.currentLvl == 100)
            {
                if (OnlvlHundred != null)
                {
                    OnlvlHundred();
                }
            }
            DataHandler.Instance.data.currentLevelProgress = Mathf.RoundToInt(DataHandler.Instance.data.currentLevelProgress - DataHandler.Instance.data.requiredXP);
            DataHandler.Instance.data.requiredXP = CalculateRequiredXP();
            if (OnLvlUp != null)
            {
                OnLvlUp();
            }
        }
    }
    IEnumerator LevelUpBonusCountDown()
    {
        levelUpBonus = true;
        yield return new WaitForSeconds(10f);
        levelUpBonus = false;
    }
    public void PrestigeUp()
    {
        if (DataHandler.Instance.data.currentLvl == 100)  //  убрать проверку в другое место
        {
            DataHandler.Instance.data.currentMultiplier = 1;
            DataHandler.Instance.data.currentPrestige++;
            CalculatePrestigeBonus();
            DataHandler.Instance.data.currentLevelProgress = 0;
            DataHandler.Instance.data.currentLvl = 1;
            DataHandler.Instance.data.currentAmoutCliks = 0;
            DataHandler.Instance.data.requiredXP = 1;
            totalMultiplier = 1;
            GainXP();
            if (OnClicked != null)
            {
                OnClicked();
            }
        }
    }
    int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        //solveForRequiredXP = Mathf.FloorToInt(DataHandler.Instance.data.currentLvl + addintionalMultiplier * Mathf.Pow(powerMultiplier, DataHandler.Instance.data.currentLvl / divisionMultiplier)); 
        // поправить формулу,  итог = вычисления + (вычисления * престиж %)
        solveForRequiredXP = Mathf.FloorToInt(DataHandler.Instance.data.currentLvl + (100 + DataHandler.Instance.data.currentPrestigeBonus) * Mathf.Pow(2, DataHandler.Instance.data.currentLvl / 6.7f));
        return solveForRequiredXP;
    }

    public void CalculateClickUpgradeCost()
    {
        // rint(x+50*(3^(x/11.1)))
        clickCost = Mathf.FloorToInt(DataHandler.Instance.data.clickUpgradeID + 50 * Mathf.Pow(3, DataHandler.Instance.data.clickUpgradeID / 11.1f));
        DataHandler.Instance.data.nextUpgradeClicksCost = clickCost;
        // rint(x+1*(2^(x/8.3)))

        clickUpgrade = Mathf.FloorToInt(DataHandler.Instance.data.clickUpgradeID + 1 * Mathf.Pow(2, DataHandler.Instance.data.clickUpgradeID / 8.3f));
        DataHandler.Instance.data.nextClicksAmount = clickUpgrade;
    }
    public void BuyClickMultiplier()
    {
        DataHandler.Instance.data.clickUpgradeID++;
        DataHandler.Instance.data.currentAmoutCliks -= clickCost;
        DataHandler.Instance.data.currentMultiplier = DataHandler.Instance.data.nextClicksAmount;
        //var uncut = DataHandler.Instance.data.clickUpgradeID + 50 * Mathf.Pow(3, DataHandler.Instance.data.clickUpgradeID / 11.1f);
        // string output = ($"buy id ={DataHandler.Instance.data.clickUpgradeID}; cost ={clickCost}; value ={clickUpgrade}; and it can be ={uncut}");
        // print(output);
        CalculateClickUpgradeCost();
        if (OnStoreUpdate != null)
        {
            OnStoreUpdate();
        }
        if (OnClicked != null)
        {
            OnClicked();
        }
    }
}