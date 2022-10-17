using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessesManager : MonoBehaviour
{
    [SerializeField] private BusinessProperties[] _businessProperties;
    [SerializeField] private Business _businessPrefab;
    [SerializeField] private Transform _contentParent;

    private List<Business> _businesses = new List<Business>();
    public List<Business> Businesses { get { return _businesses; } }

    public void Initialize()
    {
        foreach (var businessProperties in _businessProperties)
        {
            Business newBusiness = Instantiate(_businessPrefab, _contentParent);
            newBusiness.BusinessProperties = businessProperties;
            newBusiness.Initialize();
            _businesses.Add(newBusiness);
        }
        Destroy(_businessPrefab.gameObject);

        // foreach (var business in _businesses)
        // {
        //     if (SafeManager.GetBusiness() == 0)
        //     {
        //         business.IsActive = true;
        //     }
        // }
    }

    private void Update()
    {
        foreach (var business in _businesses)
        {
            business.UpdateIncomeProcess();
        }
    }
}
