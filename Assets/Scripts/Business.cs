using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Business : MonoBehaviour
{
    private BusinessProperties _businessProperties;
    public BusinessProperties BusinessProperties
    { get { return _businessProperties;} set { _businessProperties = value; } }
    [Space]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _levelTMP;
    [SerializeField] private TextMeshProUGUI _incomeTMP;
    [SerializeField] private TextMeshProUGUI _priceTMP;
    [SerializeField] private Image _incomeProgressBarImage;
    [Space]
    [SerializeField] private BusinessUpgrade[] _businessUpgrades = new BusinessUpgrade[2];

    private float _incomeDelayCurrentProgress = 0;
    private float _incomeDelayProgressStep;
    private float _startIncomeMultiplayer = 1;
    private float _currentIncomeMultiplayer = 1;
    private int _currentBusinessLevel = 0;
    private int _priceForNextLvl;
    private int _currentIncome;
    
    private bool _isActive = false;

    // private BusinessesManager _businessesManager;
    // public BusinessesManager BusinessesManager { set { _businessesManager = value; } }

    public void Initialize()
    {
        _currentBusinessLevel = SafeManager.GetBusinessLevel(_businessProperties.Name);
        if (_currentBusinessLevel != 0)
        {
            _isActive = true;
            _incomeDelayCurrentProgress = SafeManager.GetBusinessIncomeProgress(_businessProperties.Name);
        }
        else
        {
            for (int i = 0; i < _businessUpgrades.Length; i++) 
                _businessUpgrades[i].ActiveBuyButton(false);
        }

        _priceForNextLvl = (_businessProperties.Price + 1) * (_currentBusinessLevel + 1);
        _nameTMP.SetText(_businessProperties.Name);
        _incomeProgressBarImage.fillAmount = 0;
        _incomeDelayProgressStep = 1 / _businessProperties.IncomeDelayTimer;
        _levelTMP.SetText((_currentBusinessLevel).ToString());
        _priceTMP.SetText("Price: " + _priceForNextLvl + "$");
        
        for (int i = 0; i < _businessUpgrades.Length; i++)
        {
            _businessUpgrades[i].Initialize(_businessProperties.BusinessUpgradesData[i], this);
        }
        
        UpdateIncome();
    }
    
    public void UpdateIncomeProcess()
    {
        if(!_isActive)
            return;
        
        _incomeDelayCurrentProgress += Time.deltaTime;
        _incomeProgressBarImage.fillAmount = _incomeDelayCurrentProgress * _incomeDelayProgressStep;
        CheckIncomeCondition();
    }
    
    public void BuyLvlUp()
    {
        if(GameManager.CurrentMoney < _priceForNextLvl)
            return;
        
        Debug.Log("Уровень бизнеса" + this.name + " повышен");
        _isActive = true;
        _currentBusinessLevel++;
        GameManager.ChangeMoney(-_priceForNextLvl);
        SafeManager.SaveBusinessLevel(_businessProperties.Name, _currentBusinessLevel);
        _priceForNextLvl = (_businessProperties.Price + 1) * (_currentBusinessLevel + 1);
        UpdateIncome();

        for (int i = 0; i < _businessUpgrades.Length; i++) 
            _businessUpgrades[i].ActiveBuyButton();
        
        _levelTMP.SetText((_currentBusinessLevel).ToString());
        _priceTMP.SetText("Price: " + _priceForNextLvl + "$");
    }

    private void CheckIncomeCondition()
    {
        if (_incomeDelayCurrentProgress < _businessProperties.IncomeDelayTimer)
            return;

        _incomeDelayCurrentProgress = 0;
        TransferMoney();
    }

    private void TransferMoney()
    {
        GameManager.ChangeMoney(_currentIncome);
    }

    public void SaveIncomeProgress()
    {
        if(_isActive)
            SafeManager.SaveBusinessIncomeProgress(_businessProperties.Name, _incomeDelayCurrentProgress);
    }

    public void UpdateIncome()
    {
        int additionalPercentageOfIncomeSum = 0;
        for (int i = 0; i < _businessProperties.BusinessUpgradesData.Length; i++)
        {
            if (_businessUpgrades[i].IsActive)
                additionalPercentageOfIncomeSum += _businessProperties.BusinessUpgradesData[i].AdditionalPercentageOfIncome;
        }

        float multiplayer = 1 + (additionalPercentageOfIncomeSum / 100);
        float currentIncome = _businessProperties.Income * multiplayer * _currentBusinessLevel;
        _currentIncome = (int)currentIncome;
        _incomeTMP.SetText((_businessProperties.Income * _currentBusinessLevel).ToString());
    }
}
