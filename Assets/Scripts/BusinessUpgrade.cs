using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessUpgrade : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private string _purchasedDescription = "Purchased";

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _priceTMP;
    [SerializeField] private TextMeshProUGUI _incomeTMP;
    [SerializeField] private Button _buyButton;

    private BusinessProperties.BusinessUpgradeData _businessUpgradeData;
    private Business _relatedBusiness;
    private bool _isActive = false;
    public bool IsActive { get { return _isActive; } }

    public void Initialize(BusinessProperties.BusinessUpgradeData businessUpgradeData, Business relatedBusiness)
    {
        _businessUpgradeData = businessUpgradeData;
        _relatedBusiness = relatedBusiness;
        _nameTMP.SetText(_businessUpgradeData.Name);
        _priceTMP.SetText("Price: " + _businessUpgradeData.Price.ToString());
        _incomeTMP.SetText("Income: + " + _businessUpgradeData.AdditionalPercentageOfIncome + "%");
        CheckUpgradePurchased();
    }

    public void Buy()
    {
        if(GameManager.CurrentMoney < _businessUpgradeData.Price)
            return;
        
        Debug.Log("Игрок покупает улучшение " + this);
        GameManager.ChangeMoney(-_businessUpgradeData.Price);
        SafeManager.SaveBusinessUpgrade(_businessUpgradeData.Name);
        CheckUpgradePurchased();
    }

    private void CheckUpgradePurchased()
    {
        if (SafeManager.GetBusinessUpgrade(_businessUpgradeData.Name))
        {
            Debug.Log("Улучшение было приобретено: " + this);
            _isActive = true;
            _priceTMP.SetText(_purchasedDescription);
            ActiveBuyButton(false);
            _relatedBusiness.UpdateIncome();
        }
    }

    public void ActiveBuyButton(bool active = true)
    {
        if (_isActive)
            active = false;
        _buyButton.interactable = active;
    }
}
