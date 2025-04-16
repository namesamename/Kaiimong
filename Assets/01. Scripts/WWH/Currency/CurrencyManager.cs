using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;






public class CurrencyManager : Singleton<CurrencyManager>, ISavable
{
    CurrencySaveData data;

    ActivityCurrencySO ActSO;
    float offTime;
    float Interval;
    public float TimeWhenNextCharge;
    string LastTimeExitKey = "Timekey";

    private Dictionary<CurrencyType, int> CurrencySaveDic = new Dictionary<CurrencyType, int>();
    private void Update()
    {
        if (CurrencySaveDic.ContainsKey(CurrencyType.Activity))
        {
            if (CurrencySaveDic[CurrencyType.Activity] < ActSO.MaxCount)
            {
                Interval += Time.deltaTime;
                if (Interval > ActSO.AutoRecoveryPerMinute)
                {
                    TimeWhenNextCharge = ActSO.AutoRecoveryPerMinute - Interval;
                    Interval -= ActSO.AutoRecoveryPerMinute;
                    CurrencySaveDic[CurrencyType.Activity] += 1;
                    Debug.Log(CurrencySaveDic[CurrencyType.Activity]);
                }
            }
        }
    }
    public void InitialIze()
    {
        HaveData();
    }
    private void HaveData()
    {
        var SaveData = SaveDataBase.Instance.GetSaveDataToID<CurrencySaveData>(SaveType.Currency, "Currency");
        if (SaveData != null && SaveData is CurrencySaveData instance)
        {
            data = instance;
        }
        else
        {
            data = new CurrencySaveData()
            {
                Savetype = SaveType.Currency,
                UserLevel = 1,
                UserEXP = 0,
                ActivityValue = 100,
                GachaValue = 0,
                GoldValue = 0,
                DIAValue = 0,
                ID = "Currency"
            };
        }
        DicSet();
    }


    public void OnApplicationQuit()
    {
        if (SaveDataBase.Instance.SaveDic.ContainsKey(SaveType.Currency))
        {
            PlayerPrefs.SetString(LastTimeExitKey, DateTime.UtcNow.ToString("o"));
            Debug.Log(DateTime.UtcNow.ToString("o"));
            PlayerPrefs.Save();
        }

    }
    private void Start()
    {
        ActSO = GlobalDataTable.Instance.currency.GetCurrencySOToEnum<ActivityCurrencySO>(CurrencyType.Activity);
        if (PlayerPrefs.HasKey(LastTimeExitKey))
        {
            Debug.Log(PlayerPrefs.GetString(LastTimeExitKey));
            DateTime last = DateTime.Parse(PlayerPrefs.GetString(LastTimeExitKey));
            DateTime Utc = last.ToUniversalTime();
            TimeSpan span = DateTime.UtcNow - Utc;
            offTime = (float)span.TotalSeconds;
            DisableAutoCharge(offTime);
        }
    }
    public void DisableAutoCharge(float offTime)
    {
        CurrencySaveDic[CurrencyType.Activity] += (int)(offTime / ActSO.AutoRecoveryPerMinute);
        if (CurrencySaveDic[CurrencyType.Activity] >= ActSO.MaxCount)
        {
            CurrencySaveDic[CurrencyType.Activity] = ActSO.MaxCount;
        }
    }

    public CurrencySaveData GetAllCurrencyInfo()
    {
        return DicToSaveData();
    }
    public void SetCurrency(CurrencyType currency, int amount)
    {
        if (CurrencySaveDic[currency] + amount > GlobalDataTable.Instance.currency.CurrencyDic[currency].MaxCount || CurrencySaveDic[currency] + amount < 0)
        {
            return;
        }
        else
        {
            CurrencySaveDic[currency] += amount;
        }
    }
    public int GetCurrency(CurrencyType currency)
    {
        return CurrencySaveDic[currency];
    }

    public void DicSet()
    {
        CurrencySaveDic[CurrencyType.UserLevel] = data.UserLevel;
        CurrencySaveDic[CurrencyType.UserEXP] = data.UserEXP;
        CurrencySaveDic[CurrencyType.Gacha] = data.GachaValue;
        CurrencySaveDic[CurrencyType.Gold] = data.GoldValue;
        CurrencySaveDic[CurrencyType.Dia] = data.DIAValue;
        CurrencySaveDic[CurrencyType.Activity] = data.ActivityValue;
    }

    public CurrencySaveData DicToSaveData()
    {
        CurrencySaveData data = new CurrencySaveData()
        {
            UserLevel = 1,
            UserEXP = 0,
            GachaValue = CurrencySaveDic[CurrencyType.Gacha],
            GoldValue = CurrencySaveDic[CurrencyType.Gold],
            DIAValue = CurrencySaveDic[CurrencyType.Dia],
            ActivityValue = CurrencySaveDic[CurrencyType.Activity],
            Savetype = SaveType.Currency,
            ID = "Currency"
        };
        return data;
    }


    public void Save()
    {
        SaveDataBase.Instance.SetSingleSaveInstance(DicToSaveData(), SaveType.Currency);
    }
}
