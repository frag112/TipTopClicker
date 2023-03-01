using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
public int _counter;
public int _multiplierBasic;
public TMP_Text _currentCount;
public void UpdateCounter(){
_counter+=(1*_multiplierBasic);
_currentCount.text = _counter.ToString();
}
}
