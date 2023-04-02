using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]private int min, max, current;
    [SerializeField] private Image mask, fill;
    [SerializeField] private Color color;
    void Start()
    {
        SetParams();
    }

    void OnEnable()
    {
        Counter.OnClicked += GetCurrentFill;
        Counter.OnLvlUp += SetParams;
    }
    void OnDisable()
    {
        Counter.OnClicked -= GetCurrentFill;
        Counter.OnLvlUp -=SetParams;
    }
    void GetCurrentFill()
    {
        current = DataHandler.Instance.data.currentLevelProgress;

        float fillAmount = ((current*100) / max)/100f;
        Debug.Log(fillAmount);
        mask.fillAmount = fillAmount;
       // fill.color = color;
    }
    void SetParams()
    {
        min = 0;
        max = DataHandler.Instance.data.requiredXP;
        current = DataHandler.Instance.data.currentLevelProgress;
    }
}
