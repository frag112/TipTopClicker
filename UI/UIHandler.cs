using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [Header("Design Colors")]
    [SerializeField] Color mainColor;
    [SerializeField] Color activeColor;
    [SerializeField] Color secondaryColor;
    [SerializeField] Color backgroundColor;
    [Header("Top Panel")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI storageText;
    [SerializeField] TextMeshProUGUI saveLabel;
    [SerializeField] TextMeshProUGUI levelProgressText;
    [Header("Game Panel")]
    [SerializeField] TextMeshProUGUI currentAmountClicks;
    [SerializeField] TextMeshProUGUI clickButtonText;
    [Header("Store Panel")]
    [SerializeField] GameObject upgradeClick;
    List<GameObject> ClickChildren = new List<GameObject>();
    [SerializeField] TextMeshProUGUI upgradeVaultText;
    [SerializeField] TextMeshProUGUI upgradeVaultCost;
    [SerializeField] TextMeshProUGUI upgradeSpeedText;
    [SerializeField] TextMeshProUGUI upgradeSpeedCost;


    void OnEnable()
    {
        Counter.OnClicked += UpdateCountersTexts;
        Counter.OnClicked += CheckStoreAvailability;
        Counter.OnLvlUp += UpdateLevelText;
        DataHandler.OnSave += ShowSaveLabel;
    }
    void OnDisable()
    {
        Counter.OnClicked -= UpdateCountersTexts;
        Counter.OnClicked -= CheckStoreAvailability;
        Counter.OnLvlUp -= UpdateLevelText;
        DataHandler.OnSave -= ShowSaveLabel;
    }
    void Start()
    {
        UpdateCountersTexts();
        UpdateLevelText();
        CollectChildren(upgradeClick, ClickChildren);
    }
    void CollectChildren(GameObject father, List<GameObject> children)
    {
        var imageArray = father.GetComponentsInChildren<Image>();
        foreach (var element in imageArray)
        {
            children.Add(element.gameObject);
        }
        var textArray = father.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var element in textArray)
        {
            children.Add(element.gameObject);
        }
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
    void CheckStoreAvailability() // цвет меняется но альфа падает в ноль, нужно поправить ее тоже
    {
        if (DataHandler.Instance.data.currentAmoutCliks < 100)
        {
            foreach (var element in ClickChildren)
            {
                if (element.GetComponent<Image>() != null)
                {
                    element.GetComponent<Image>().color = mainColor;
                }
                if (element.GetComponent<TextMeshProUGUI>() != null)
                {
                    element.GetComponent<TextMeshProUGUI>().color = mainColor;
                }
            }
        }else{
                        foreach (var element in ClickChildren)
            {
                if (element.GetComponent<Image>() != null)
                {
                    element.GetComponent<Image>().color = secondaryColor;
                }
                if (element.GetComponent<TextMeshProUGUI>() != null)
                {
                    element.GetComponent<TextMeshProUGUI>().color = secondaryColor;
                }
            }
        }
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
