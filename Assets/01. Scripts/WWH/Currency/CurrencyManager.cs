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
                    SetCurrency(CurrencyType.Activity, 1);
                    Debug.Log(CurrencySaveDic[CurrencyType.Activity]);
                }
            }
        }
    }
    public void InitialIze()
    {
        HaveData();
        StartCoroutine(StartStamina());
 
    }


    public IEnumerator StartStamina()
    {
        ActSO = GlobalDataTable.Instance.currency.GetCurrencySOToEnum<ActivityCurrencySO>(CurrencyType.Activity);
        yield return new WaitUntil(() => ActSO != null);
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
    private void HaveData()
    {
        var SaveData = SaveDataBase.Instance.GetSaveDataToID<CurrencySaveData>(SaveType.Currency, 0);
        if (SaveData != null && SaveData is CurrencySaveData instance)
        {
            data = instance;
        }
        else
        {
            data = new CurrencySaveData()
            {
                UserName = "JIHwan",
                Savetype = SaveType.Currency,
                UserLevel = 1,
                UserEXP = 0,
                CurrentStaminaMax = 160,
                ActivityValue = 100,
                GachaValue = 10000,
                GoldValue = 1000,
                DIAValue = 99999,
                CharacterEXP = 1000,
                ID = 0
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

    public void HealStamina(int amount)
    {
        if (CurrencySaveDic[CurrencyType.Activity] + amount > GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Activity].MaxCount)
        {
            CurrencySaveDic[CurrencyType.Activity] = GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Activity].MaxCount;
        }
        else
        {
            CurrencySaveDic[CurrencyType.Activity] += amount;
        }
  
    }
    public void SetCurrency(CurrencyType currency, int amount)
    {
        if (currency != CurrencyType.Activity)
        {

            if (CurrencySaveDic[currency] + amount > GlobalDataTable.Instance.currency.CurrencyDic[currency].MaxCount)
            {
                CurrencySaveDic[currency] = GlobalDataTable.Instance.currency.CurrencyDic[currency].MaxCount;

            }
            else if (CurrencySaveDic[currency] + amount < 0)
            {
                CurrencySaveDic[currency] = 0;
            }
            else
            {
                CurrencySaveDic[currency] += amount;
            }
        }
        else
        {
            if (CurrencySaveDic[currency] + amount > data.CurrentStaminaMax)
            {
                CurrencySaveDic[currency] = data.CurrentStaminaMax;

            }
            else if (CurrencySaveDic[currency] + amount < 0)
            {
                CurrencySaveDic[currency] = 0;
            }
            else
            {
                CurrencySaveDic[currency] += amount;
            }
        }
        Save();
    }
    public int GetCurrency(CurrencyType currency)
    {
        return CurrencySaveDic[currency];
    }

    public string GetUserName()
    {
        return data.UserName;
    }

    public void DicSet()
    {
        CurrencySaveDic[CurrencyType.UserLevel] = data.UserLevel;
        CurrencySaveDic[CurrencyType.UserEXP] = data.UserEXP;
        CurrencySaveDic[CurrencyType.Gacha] = data.GachaValue;
        CurrencySaveDic[CurrencyType.Gold] = data.GoldValue;
        CurrencySaveDic[CurrencyType.Dia] = data.DIAValue;
        CurrencySaveDic[CurrencyType.Activity] = data.ActivityValue;
        CurrencySaveDic[CurrencyType.CharacterEXP] = data.CharacterEXP;
        CurrencySaveDic[CurrencyType.CurMaxStamina] = data.CurrentStaminaMax;
    }

    public CurrencySaveData DicToSaveData()
    {
        CurrencySaveData data = new CurrencySaveData()
        {
            UserLevel = CurrencySaveDic[CurrencyType.UserLevel],
            UserEXP = CurrencySaveDic[CurrencyType.UserEXP],
            GachaValue = CurrencySaveDic[CurrencyType.Gacha],
            GoldValue = CurrencySaveDic[CurrencyType.Gold],
            DIAValue = CurrencySaveDic[CurrencyType.Dia],
            ActivityValue = CurrencySaveDic[CurrencyType.Activity],
            CharacterEXP = CurrencySaveDic[CurrencyType.CharacterEXP],
            Savetype = SaveType.Currency,
            ID = 0
        };
        return data;
    }

    public void Save()
    {
        SaveDataBase.Instance.SaveSingleData(DicToSaveData());
    }

}
