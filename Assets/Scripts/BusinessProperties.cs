using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Business", menuName = "My Objects/Business", order = 51)]
public class BusinessProperties : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private string _name = "Business Name";
    public string Name { get { return _name;}}
    [SerializeField] private int _income = 10;
    public int Income { get { return _income;}}
    [SerializeField] private float _incomeDelayTimer = 3f;
    public float IncomeDelayTimer { get { return _incomeDelayTimer;}}
    [SerializeField] private int _price = 100;
    public int Price { get { return _price;}}

    [Header("Levels and upgrades")]
    // [SerializeField] private BusinessLevelData[] _businessLevelsData = new BusinessLevelData[5];
    // public BusinessLevelData[] BusinessLevelsData { get { return _businessLevelsData;}}
    [SerializeField] private BusinessUpgradeData[] _businessUpgradesData = new BusinessUpgradeData[2];
    public BusinessUpgradeData[] BusinessUpgradesData { get { return _businessUpgradesData;}}

    
    // [System.Serializable]
    // public class BusinessLevelData
    // {
    //     
    // }
    
    [System.Serializable]
    public class BusinessUpgradeData
    {
        [SerializeField] private string _name = "Upgrade Name";
        public string Name { get { return _name;}}
        [SerializeField] private int _price = 50;
        public int Price { get { return _price;}}
        [SerializeField] private int _additionalPercentageOfIncome = 50;
        public int AdditionalPercentageOfIncome { get { return _additionalPercentageOfIncome;}}
    }
}
