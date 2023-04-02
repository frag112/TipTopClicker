using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int min, max, current;
    [SerializeField] private float fillAmount = 0;
    [SerializeField] private Image mask, fill;
    [SerializeField] private Color color;
    void Start()
    {
        SetParams();
        GetCurrentFill();
    }

    void OnEnable()
    {
        Counter.OnClicked += GetCurrentFill;
        Counter.OnLvlUp += SetParams;
    }
    void OnDisable()
    {
        Counter.OnClicked -= GetCurrentFill;
        Counter.OnLvlUp -= SetParams;
    }
    void GetCurrentFill()
    {
        current = DataHandler.Instance.data.currentLevelProgress;
        if (current != 0)
        {
            fillAmount = ((current * 100f) / max) / 100f;
        }
        mask.fillAmount = fillAmount;
        // fill.color = color;
    }
    void SetParams()
    {
        min = 0;
        max = DataHandler.Instance.data.requiredXP;
        if (max==0) max = 111;
        current = DataHandler.Instance.data.currentLevelProgress;
    }
}
