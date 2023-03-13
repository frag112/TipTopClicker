using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Counter : MonoBehaviour
{
    [Header("Bonus Multipliers")]
    bool levelUpBonus;
    public float timeThreshold, frenzyBar;// frenzy bar is  invisible bar that can be filled by pressing button
    int frenzyBonus, totalMultiplier;
    [Header("Level Multipliers")]
    [Range(1, 500)]
    [SerializeField] private int addintionalMultiplier = 300;
    [Range(2, 6)]
    [SerializeField] private int powerMultiplier = 2;
    [Range(2, 20)]
    [SerializeField] private float divisionMultiplier = 7f;
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public static event ClickAction OnLvlUp;
    public static event ClickAction OnlvlHundred;
    void Start()
    {
        DataHandler.Instance.data.requiredXP = CalculateRequiredXP();
    }
    void Update()
    {
        timeThreshold += Time.deltaTime;
        if (timeThreshold > .5f)
        {
            timeThreshold = 0;
            frenzyBar -= .30f;
        }
        if (frenzyBar < 0) frenzyBar = 0;
        if (frenzyBar > 5) frenzyBar = 5;
    }
    public void UpdateCounter()
    {
        if(frenzyBar<1)frenzyBar += .12f;
        if (frenzyBar>=1)frenzyBar += .22f;
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
        switch (frenzyBar)
        {
            case 1:
                totalMultiplier += (int)(baseValue * .35f);
                break;
            case 2:
                totalMultiplier += (int)(baseValue * .7f);
                break;
            default:
                break;
        }
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
}