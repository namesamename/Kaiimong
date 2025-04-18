using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDataTable 
{
    public Dictionary<CurrencyType, CurrencySO> CurrencyDic = new Dictionary<CurrencyType, CurrencySO>();
    public void Initialize()
    {
        CurrencySO[] currencySOs = Resources.LoadAll<CurrencySO>("Currency");
        for (int i = 0; i < currencySOs.Length; i++)
        {
            CurrencyDic[currencySOs[i].CurrencyType] = currencySOs[i];
        }
    }

    public T GetCurrencySOToEnum<T>(CurrencyType currency) where T : CurrencySO
    {
        if (CurrencyDic.ContainsKey(currency))
        {
            if(CurrencyDic[currency] is T Currency)
            {
                return Currency;
            }
        }
        return null;
  
    }


}
