using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public interface ISavable
{
    public void Save();
}



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
        if (CurrencySaveDic[CurrencyType.Activity] < ActSO.MaxCount)
        {
            Interval += Time.deltaTime;
            if (Interval > ActSO.AutoRecoveryPerMinute)
            {
                Interval -= ActSO.AutoRecoveryPerMinute;
                CurrencySaveDic[CurrencyType.Activity] += 1;
            }
        }
    }
    public void InitialIze()
    {
        HaveData();
    }
    private void HaveData()
    {
        ActSO = GlobalDatabase.Instance.currency.GetCurrencySOToEnum<ActivityCurrencySO>(CurrencyType.Activity);
        var SaveDAta = SaveDataBase.Instance.GetSaveDataToID<CurrencySaveData>(SaveType.Currency, "Currency");
        if (SaveDAta != null && SaveDAta is CurrencySaveData instance)
        {
            data = instance;
        }
        else
        {
            data = new CurrencySaveData()
            {
                Savetype = SaveType.Currency,
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
        if (GameSaveSystem.SaveDic.ContainsKey(SaveType.Currency))
        {
            PlayerPrefs.SetString(LastTimeExitKey, DateTime.UtcNow.ToString("o"));
            Debug.Log(DateTime.UtcNow.ToString("o"));
            PlayerPrefs.Save();
        }

    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(LastTimeExitKey))
        {
            Debug.Log(PlayerPrefs.GetString(LastTimeExitKey));
            DateTime last = DateTime.Parse(PlayerPrefs.GetString(LastTimeExitKey));
            DateTime Utc = last.ToUniversalTime();
            TimeSpan span = DateTime.UtcNow - Utc;
            Debug.Log($"{DateTime.UtcNow} - {Utc} = {span}");
            offTime = (float)span.TotalSeconds;
            DisableAutoCharge(offTime);
        }
    }
    public void DisableAutoCharge(float offTime)
    {
        TimeWhenNextCharge = (offTime % ActSO.AutoRecoveryPerMinute);
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
        if (CurrencySaveDic[currency] + amount > GlobalDatabase.Instance.currency.CurrencyDic[currency].MaxCount || CurrencySaveDic[currency] + amount < 0)
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
        CurrencySaveDic[CurrencyType.Gacha] = data.GachaValue;
        CurrencySaveDic[CurrencyType.Gold] = data.GoldValue;
        CurrencySaveDic[CurrencyType.Dia] = data.DIAValue;
        CurrencySaveDic[CurrencyType.Activity] = data.ActivityValue;
    }

    public CurrencySaveData DicToSaveData()
    {

        CurrencySaveData data = new CurrencySaveData()
        {
            GachaValue = CurrencySaveDic[CurrencyType.Gacha],
            GoldValue = CurrencySaveDic[CurrencyType.Gold],
            DIAValue = CurrencySaveDic[CurrencyType.Dia],
            ActivityValue = CurrencySaveDic[CurrencyType.Activity],
        };

        return data;
    }


    public void Save()
    {

        SaveDataBase.Instance.SetSaveInstances(DicToSaveData(), SaveType.Currency);
    }
}
