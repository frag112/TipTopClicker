using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [Header("Top Panel")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI storageText;
    [SerializeField] TextMeshProUGUI saveLabel;
    [SerializeField] TextMeshProUGUI levelProgressText;
    [Header("Game Panel")]
    [SerializeField] TextMeshProUGUI currentAmountClicks;
    [SerializeField] TextMeshProUGUI clickButtonText;
    [Header("Store Panel")]
    [SerializeField] TextMeshProUGUI upgradeClickText;
    [SerializeField] TextMeshProUGUI upgradeClickCost;
    [SerializeField] TextMeshProUGUI upgradeVaultText;
    [SerializeField] TextMeshProUGUI upgradeVaultCost;
    [SerializeField] TextMeshProUGUI upgradeSpeedText;
    [SerializeField] TextMeshProUGUI upgradeSpeedCost;


    void OnEnable()
    {
        Counter.OnClicked += UpdateCountersTexts;
        Counter.OnLvlUp += UpdateLevelText;
        DataHandler.OnSave += ShowSaveLabel;
    }
    void OnDisable()
    {
        Counter.OnClicked -= UpdateCountersTexts;
        Counter.OnLvlUp -= UpdateLevelText;
        DataHandler.OnSave -= ShowSaveLabel;
    }
    void Start()
    {
        UpdateCountersTexts();
        UpdateLevelText();
    }
    void UpdateCountersTexts()
    {
        currentAmountClicks.text = DataHandler.Instance.data.currentAmoutCliks.ToString();
        levelProgressText.text = DataHandler.Instance.data.currentLevelProgress + "/" + DataHandler.Instance.data.requiredXP;
    }
    void UpdateLevelText()
    {
        levelText.text = "LVL " + DataHandler.Instance.data.currentLvl;
    }
    void ShowSaveLabel()
    {
        saveLabel.enabled = true;
        Invoke("HideSaveLabel", 2f);
    }
    void HideSaveLabel()
    {
        saveLabel.enabled = false;
    }

}
