using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject parent, gamePanel;
    public float _openPosition = 264f, _closedPosition;
    public Vector2 _gamePanelWide, _gamePanelShrink;
    // public void RevealStore()
    // {
    //     var _currentPosition = parent.transform.GetComponent<RectTransform>().anchoredPosition.y;
    //     if (parent.transform.GetComponent<RectTransform>().anchoredPosition.y == _openPosition)
    //     {
    //         parent.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _closedPosition);

    //         gamePanel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _gamePanelWide.x);
    //         gamePanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1170, _gamePanelWide.y);
    //     }
    //     else
    //     {
    //         parent.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _openPosition);
    //         gamePanel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _gamePanelShrink.x);
    //         gamePanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1170, _gamePanelShrink.y);
    //     }
    // }
    public void BuyClickMultiplier()
    {
        // rint(x+50*(3^(x/11.1)))
        int cost = Mathf.FloorToInt(DataHandler.Instance.data.clickUpgradeID + 50 * Mathf.Pow(3, DataHandler.Instance.data.clickUpgradeID / 11.1f));
        DataHandler.Instance.data.currentAmoutCliks -= cost;
        // rint(x+1*(2^(x/8.3)))
        int newUpgradeValue = Mathf.FloorToInt(DataHandler.Instance.data.clickUpgradeID + 1 * Mathf.Pow(2, DataHandler.Instance.data.clickUpgradeID / 8.3f));
        var uncut = DataHandler.Instance.data.clickUpgradeID + 1 * Mathf.Pow(2, DataHandler.Instance.data.clickUpgradeID / 8.3f);
        string output = ($"buy id ={DataHandler.Instance.data.clickUpgradeID}; cost ={cost}; value ={newUpgradeValue}; and it can be ={uncut}");
        print(output);
    }
    public void Buy()
    {
        DataHandler.Instance.data.clickUpgradeID++;
        BuyClickMultiplier();
    }
}