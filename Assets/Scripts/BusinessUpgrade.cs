using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessUpgrade : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private string _purchasedDescription = "Purchased";

    [Header("Components")] [SerializeField]
    private TextMeshProUGUI _nameTMP;

    [SerializeField] private TextMeshProUGUI _priceTMP;
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
        CheckUpgradePurchased();
    }

    public void Buy()
    {
        if(GameManager.CurrentMoney < _businessUpgradeData.Price)
            return;
        
        Debug.Log("Игрок покупает улучшение " + this);
        SafeManager.SaveBusinessUpgrade(_businessUpgradeData.Name);
        _isActive = true;
        _relatedBusiness.UpdateIncome();
        CheckUpgradePurchased();
    }

    private void CheckUpgradePurchased()
    {
        if (SafeManager.GetBusinessUpgrade(_businessUpgradeData.Name))
        {
            Debug.Log("Улучшение было приобретено: " + this);
            _priceTMP.SetText(_purchasedDescription);
            ActiveBuyButton(false);
        }
    }

    public void ActiveBuyButton(bool active = true)
    {
        if (_isActive)
            active = false;
        _buyButton.interactable = active;
    }
}
