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
    [Header("Top Panel")] //T panel
    [SerializeField] TextMeshProUGUI levelTextT;
    [SerializeField] TextMeshProUGUI storageText;
    [SerializeField] TextMeshProUGUI saveLabel;
    [Header("Game Panel")]
    [SerializeField] TextMeshProUGUI currentAmountClicks;
    [SerializeField] TextMeshProUGUI clickButtonText;
    [Header("Bonus Panel")]
    [SerializeField] GameObject prestigeBonus;
    [SerializeField] GameObject lvlUpBonus;
    //public bool canShowlvlUpBonus = false;
    [SerializeField] GameObject frenzyBonus;

    [Header("Store Panel")]
    [SerializeField] GameObject upgradeClick;
    List<GameObject> ClickChildren = new List<GameObject>();
    [SerializeField] TextMeshProUGUI upgradeVaultText;
    [SerializeField] TextMeshProUGUI upgradeVaultCost;
    [SerializeField] TextMeshProUGUI upgradeSpeedText;
    [SerializeField] TextMeshProUGUI upgradeSpeedCost;
    [Header("Prestige Panel")] // P panel
    [SerializeField] TextMeshProUGUI globalClickCountText;
    [SerializeField] TextMeshProUGUI levelTextP; // большая п в конце это принадлежность к панели престиж
    [SerializeField] TextMeshProUGUI levelProgressTextP;
    [SerializeField] GameObject prestigeButton;


    void OnEnable()
    {
        Counter.OnClicked += UpdateCountersTexts;
        Counter.OnClicked += CheckStoreAvailability;
        Counter.OnLvlUp += LevelUpdated;
        DataHandler.OnSave += ShowSaveLabel;
        Counter.OnlvlHundred += AbleToPrestige;
    }
    void OnDisable()
    {
        Counter.OnClicked -= UpdateCountersTexts;
        Counter.OnClicked -= CheckStoreAvailability;
        Counter.OnLvlUp -= LevelUpdated;
        Counter.OnlvlHundred -= AbleToPrestige;
        DataHandler.OnSave -= ShowSaveLabel;
    }
    void Start()
    {
        UpdateCountersTexts();
        UpdateLevelText();
        CollectChildren(upgradeClick, ClickChildren);
        UpdatePrestigeBonus();
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
        levelProgressTextP.text = DataHandler.Instance.data.currentLevelProgress + "/" + DataHandler.Instance.data.requiredXP;
        globalClickCountText.text = DataHandler.Instance.data.globalAmountClicks.ToString();
    }
    void AbleToPrestige()
    {
        prestigeButton.SetActive(true);
    }
    public void UpdatePrestigeBonus()
    {
        if (DataHandler.Instance.data.currentPrestige > 0)
        {
            prestigeBonus.SetActive(true);
            prestigeBonus.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+ " + DataHandler.Instance.data.currentPrestigeBonus + "%";

        }
    }
    void LevelUpdated()
    {
        //canShowlvlUpBonus = true; // я не могу запустить отсюда корутину потому что делегат воид, поэтому делаю трюк с конями через апдейт
        UpdateLevelText();
        StopAllCoroutines();
        StartCoroutine(LevelUpBonusCountDown());
    }
    void UpdateLevelText()
    {
        levelTextT.text = "LVL " + DataHandler.Instance.data.currentLvl;
        levelTextP.text = "Level " + DataHandler.Instance.data.currentLvl;
    }
    IEnumerator LevelUpBonusCountDown()
    {
        //canShowlvlUpBonus = false;
        lvlUpBonus.SetActive(true);
        for (int i = 10; i > 0; i--)
        {
            lvlUpBonus.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ($"+ 45% ({i - 1})sec");
            yield return new WaitForSeconds(1);
        }
        lvlUpBonus.SetActive(false);
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
        }
        else
        {
            foreach (var element in ClickChildren)
            {
                if (element.GetComponent<Image>() != null)
                {
                    element.GetComponent<Image>().color = activeColor;
                }
                if (element.GetComponent<TextMeshProUGUI>() != null)
                {
                    element.GetComponent<TextMeshProUGUI>().color = activeColor;
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
