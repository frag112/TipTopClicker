using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStore : MonoBehaviour
{
    public GameObject parent, gamePanel;
    public float _openPosition = 264f, _closedPosition;
    public Vector2 _gamePanelWide, _gamePanelShrink;
    public void RevealStore()
    {
        var _currentPosition = parent.transform.GetComponent<RectTransform>().anchoredPosition.y;
        if (parent.transform.GetComponent<RectTransform>().anchoredPosition.y == _openPosition)
        {
            parent.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _closedPosition);

            gamePanel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _gamePanelWide.x);
            gamePanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1170, _gamePanelWide.y);
        }
        else
        {
            parent.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _openPosition);
            gamePanel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _gamePanelShrink.x);
            gamePanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1170, _gamePanelShrink.y);
        }
    }
}
