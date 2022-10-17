using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class SafeManager
{
    public static void SafeBalanceValue(int value)
    {
        PlayerPrefs.SetInt("BalanceValue", value);
        PlayerPrefs.Save();
    }

    public static int GetBalanceValue()
    {
        return PlayerPrefs.GetInt("BalanceValue");
    }
    
    public static void SaveBusinessLevel(string name, int level)
    {
        PlayerPrefs.SetInt("Business_" + name, level);
        PlayerPrefs.Save();
    }
    
    public static int GetBusinessLevel(string name)
    {
        return PlayerPrefs.GetInt("Business_" + name);
    }
    
    public static void SaveBusinessIncomeProgress(string name, float progressValue)
    {
        PlayerPrefs.SetFloat("BusinessIncomeProgress_" + name, progressValue);
        PlayerPrefs.Save();
    }
    
    public static float GetBusinessIncomeProgress(string name)
    {
        return PlayerPrefs.GetFloat("BusinessIncomeProgress_" + name);
    }
    
    public static void SaveBusinessUpgrade(string name)
    {
        PlayerPrefs.SetInt("BusinessUpgrade_" + name, 1);
        PlayerPrefs.Save();
    }

    public static bool GetBusinessUpgrade(string name)
    {
        int value = PlayerPrefs.GetInt("BusinessUpgrade_" + name);
        
        if (value == 1)
            return true;
        else
            return false;
    }
    
    public static void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
