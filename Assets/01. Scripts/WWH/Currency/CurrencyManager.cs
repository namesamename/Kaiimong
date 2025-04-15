using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISavable
{
    public void Save();
}



public class CurrencyManager : Singleton<CurrencyManager>, ISavable
{
    CurrencySaveData data;

    float offTime;
    float lastExitTime;
    public float Interval;
    float TimeWhenNextCharge;
    string LastTimeExitKey = "Timekey";


 
    private void Update()
    {
        var ActiveSo = GlobalDatabase.Instance.currency.GetCurrencySOToEnum<ActivityCurrencySO>(CurrencyType.Activity);
        if(data.ActivityValue <= ActiveSo.MaxCount)
        {
            Interval += Time.deltaTime;
            if(Interval > ActiveSo.AutoRecoveryPerMinute)
            {
                Interval -= ActiveSo.AutoRecoveryPerMinute;
                data.ActivityValue += 1;
                Debug.Log(data.ActivityValue);
                Save();
            }
        }


    }
    public void InitialIze()
    {
        HaveData();
    }
    private void HaveData()
    {
        var List = GameSaveSystem.Load(SaveType.Currency);
        if (List.Count > 0 && List != null)
        {
            if(List[0] is CurrencySaveData instance) 
            {
                data = instance;
            }
        }
        else
        {
            Debug.Log(">");
            data = new CurrencySaveData()
            {
                ActivityValue = 100,
                GachaValue = 0,
                GoldValue = 0,
                DIAValue = 0,
                ID = "Currency"
            };
            data.Savetype = SaveType.Currency;
        }
            
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
        var ActSo = GlobalDatabase.Instance.currency.GetCurrencySOToEnum<ActivityCurrencySO>(CurrencyType.Activity);
        TimeWhenNextCharge = (offTime % ActSo.AutoRecoveryPerMinute);
        data.ActivityValue += (int)(offTime / ActSo.AutoRecoveryPerMinute);
        Save();
    
    }

    public CurrencySaveData GetAllCurrencyInfo()
    {
        return data;
    }
    public void SetCurrency(CurrencyType currency, int amount)
    {
        switch (currency)
        {
            case CurrencyType.Gold:
                data.GoldValue += amount;
                break;
            case CurrencyType.Gacha:
                data.GachaValue += amount;
                break;
            case CurrencyType.Activity:
                data.ActivityValue += amount;
                break;
            case CurrencyType.Dia:
                data.DIAValue += amount;
                break;
        }
    }
    public int  GetCurrency(CurrencyType currency)
    {
        switch (currency) 
        {
            case CurrencyType.Gold:
                 return data.GoldValue;
            case CurrencyType.Gacha:
                return data.GachaValue;
            case CurrencyType.Activity:
                return data.ActivityValue;
            case CurrencyType.Dia:
                return data.DIAValue;
            default:
                return 0;
        }
    }

    public void Save()
    {
        GameSaveSystem.SaveData(data);
    }
}
