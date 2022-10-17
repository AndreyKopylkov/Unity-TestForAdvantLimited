using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBalanceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceTMP;
    [SerializeField] private string _textBeforeValue = "Balance: ";
    [SerializeField] private string _textAfterValue = "$";

    private void OnEnable()
    {
        GameManager.onBalanceChange += UpdateCashValue;
    }

    public void UpdateCashValue(int value)
    {
        _balanceTMP.SetText(_textBeforeValue + value.ToString() + _textAfterValue);
    }
}
