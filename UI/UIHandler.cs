using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [Header("Design Colors")]
    [SerializeField] Color mainColor;
    [SerializeField] Color additionalColor;
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
    [SerializeField] GameObject permanentBonus;

    [Header("Store Panel")]
    [SerializeField] Image storeIcon;
    [SerializeField] StoreEntry upgradeClick;
    [SerializeField] StoreEntry upgradeVault;
    [SerializeField] StoreEntry upgradeSpeed;
    [Header("Prestige Panel")] // P panel
    [SerializeField] TextMeshProUGUI globalClickCountText;
    [SerializeField] TextMeshProUGUI levelTextP; // большая п в конце это принадлежность к панели престиж
    [SerializeField] TextMeshProUGUI levelProgressTextP;
    [SerializeField] GameObject prestigeButton;


    void OnEnable()
    {
        Counter.OnClicked += UpdateCountersTexts;
        Counter.OnLvlUp += LevelUpdated;
        DataHandler.OnSave += ShowSaveLabel;
        Counter.OnlvlHundred += AbleToPrestige;
        Counter.OnStoreUpdate += UpdateStore;
    }
    void OnDisable()
    {
        Counter.OnClicked -= UpdateCountersTexts;
        Counter.OnLvlUp -= LevelUpdated;
        Counter.OnlvlHundred -= AbleToPrestige;
        DataHandler.OnSave -= ShowSaveLabel;
        Counter.OnStoreUpdate -= UpdateStore;
    }
    void Update()
    {
        if (DataHandler.Instance.data.storeAvailable == false && DataHandler.Instance.data.currentPrestige == 0 && DataHandler.Instance.data.currentAmoutCliks >= 50)
        {
            storeIcon.enabled = true;
            DataHandler.Instance.data.storeAvailable = true;
        }
    }
    void Start()
    {
        upgradeClick.Start();
        upgradeClick.UpdateValues(DataHandler.Instance.data.nextUpgradeClicksCost, DataHandler.Instance.data.nextClicksAmount);
        UpdateCountersTexts();
        UpdateLevelText();
        UpdatePrestigeBonus();
        if (DataHandler.Instance.data.storeAvailable) storeIcon.enabled = true;
    }
    void UpdateStore()
    {
        upgradeClick.UpdateValues(DataHandler.Instance.data.nextUpgradeClicksCost, DataHandler.Instance.data.nextClicksAmount);
    }
    void UpdateCountersTexts()
    {
        currentAmountClicks.text = DataHandler.Instance.data.currentAmoutCliks.ToString();
        levelProgressTextP.text = DataHandler.Instance.data.currentLevelProgress + "/" + DataHandler.Instance.data.requiredXP;
        globalClickCountText.text = DataHandler.Instance.data.globalAmountClicks.ToString();

        if (DataHandler.Instance.data.currentAmoutCliks >= DataHandler.Instance.data.nextUpgradeClicksCost)
        {
            upgradeClick.ChangeState(mainColor, true);
        }
        else
        {
            upgradeClick.ChangeState(secondaryColor, false);
        }
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
    public void ShowPermanentBonus()
    {
        //permanentBonus.SetActive(true);
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

[System.Serializable]
class StoreEntry
{
    public GameObject entry;
    public string text;
    private TextMeshProUGUI mainText, description, cost;
    private Button button;
    private Image buyButton, lockImage;
    public void Start()
    {
        var textholder = entry.transform.GetChild(0);

        mainText = textholder.GetChild(0).GetComponent<TextMeshProUGUI>();
        description = textholder.GetChild(1).GetComponent<TextMeshProUGUI>();
        button = entry.GetComponentInChildren<Button>();
        cost = button.GetComponentInChildren<TextMeshProUGUI>();
        buyButton = button.transform.GetComponent<Image>();
        lockImage = entry.transform.GetChild(2).GetComponent<Image>();

    }
    public void UpdateValues(int cost, int value = 0)
    {
        this.cost.text = cost.ToString();
        if (value > 0)
        {
            mainText.text = text + $" X{value}";
        }

    }

    public void RemoveLock()
    {
        // for the vault and speed -  removing the lock from buy button
    }
    public void ChangeState(Color newColor, bool state)
    {
        mainText.color = newColor;
        description.color = newColor;
        buyButton.color = newColor;
        cost.color = newColor;

        button.enabled = state;
    }
}