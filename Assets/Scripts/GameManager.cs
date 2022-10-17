using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BusinessesManager _businessesManager;

    private static PlayerBalanceUI _playerBalanceUI;

    private static int _currentMoney = 0;

    public static int CurrentMoney
    {
        get { return _currentMoney; }
    }

    //Actions
    public static Action<int> onBalanceChange;

    private void Awake()
    {
        _currentMoney = SafeManager.GetBalanceValue();
    }

    private void Start()
    {
        ChangeMoney(0);
        _businessesManager.Initialize();
        CheckFirstAppLaunch();
    }

    public static void ChangeMoney(int value)
    {
        Debug.Log("Счёт игрока изменился");
        _currentMoney += value;
        SafeManager.SafeBalanceValue(_currentMoney);
        onBalanceChange.Invoke(_currentMoney);
    }

    private void CheckFirstAppLaunch()
    {
        string businessName = _businessesManager.Businesses[0].BusinessProperties.Name;
        if (SafeManager.GetBusinessLevel(businessName) == 0)
        {
            Debug.Log("Сохранение при первом запуске приложения");
            SafeManager.SaveBusinessLevel(businessName, 1);
            _businessesManager.Businesses[0].Initialize();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SaveAllData();
    }

    private void OnApplicationQuit()
    {
        SaveAllData();
    }

    private void SaveAllData()
    {
        foreach (var business in _businessesManager.Businesses)
        {
            business.SaveIncomeProgress();
        }
    }
}
