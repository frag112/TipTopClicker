using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Counter : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public void UpdateCounter()
    {
        if (OnClicked!=null){
            OnClicked();
        }
        DataHandler.Instance.data.currentAmoutCliks += (1 * DataHandler.Instance.data.currentMultiplier);

    }
    void CalculateFormula(){

    }
}
