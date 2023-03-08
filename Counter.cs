using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Counter : MonoBehaviour
{
    [Header("Level Multipliers")]
    [Range(1, 500)]
    [SerializeField] private int addintionalMultiplier = 300;
    [Range(2, 6)]
    [SerializeField] private int powerMultiplier = 2;
    [Range(2, 20)]
    [SerializeField] private int divisionMultiplier = 7;
    [SerializeField]private int totalMultiplier;
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public static event ClickAction OnLvlUp;
    void Start(){
        DataHandler.Instance.data.requiredXP = CalculateRequiredXP();
    }
    public void UpdateCounter()
    {
        totalMultiplier = (1 * DataHandler.Instance.data.currentMultiplier);
        DataHandler.Instance.data.currentAmoutCliks += totalMultiplier;
        GainXP();
        if (OnClicked != null)
        {
            OnClicked();
        }
    }
    void CalculateFormula()
    {

    }
    void GainXP()
    {
        DataHandler.Instance.data.currentLevelProgress += totalMultiplier;
        if (DataHandler.Instance.data.currentLevelProgress >= DataHandler.Instance.data.requiredXP) LevelUp();
    }
    public void LevelUp()
    {
        DataHandler.Instance.data.currentLvl++;
        DataHandler.Instance.data.currentLevelProgress = Mathf.RoundToInt(DataHandler.Instance.data.currentLevelProgress - DataHandler.Instance.data.requiredXP);
        DataHandler.Instance.data.requiredXP = CalculateRequiredXP();
                if (OnLvlUp != null)
        {
            OnLvlUp();
        }
    }
    private int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        for (int levelCycle = 1; levelCycle <= DataHandler.Instance.data.currentLvl; levelCycle++)
        {
            solveForRequiredXP += (int)Mathf.Floor(levelCycle + addintionalMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXP / 4;
    }
}