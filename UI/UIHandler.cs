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
    }
    void OnDisable()
    {
        Counter.OnClicked -= UpdateCountersTexts;
    }
    void Start(){
        UpdateCountersTexts();
    }
    void UpdateCountersTexts()
    {
        currentAmountClicks.text = DataHandler.Instance.data.currentAmoutCliks.ToString();
    }
}
