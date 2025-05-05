using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CurrencyDataTable 
{
    public Dictionary<CurrencyType, CurrencySO> CurrencyDic = new Dictionary<CurrencyType, CurrencySO>();
    public List<CurrencySO> Lists = new List<CurrencySO>();
    public async void Initialize()
    {
        var handle = Addressables.LoadAssetsAsync<CurrencySO>("Currency", Cur =>
        {
            Lists.Add(Cur);
        });

        await handle.Task;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            foreach (var item in Lists) 
            {
                if(item is DIaCurrencySO)
                {
                    CurrencyDic[CurrencyType.Dia] = (DIaCurrencySO)item;
                }
               else if (item is GoldCurrencySO)
                {
                    CurrencyDic[CurrencyType.Gold] = (GoldCurrencySO)item;
                }
                else if (item is ActivityCurrencySO)
                {
                    CurrencyDic[CurrencyType.Activity] = (ActivityCurrencySO)item;
                }
                else if (item is GachaCurrencySO)
                {
                    CurrencyDic[CurrencyType.Gacha] = (GachaCurrencySO)item;
                }
                else if (item is CurMaxStaminaCurrency)
                {
                    CurrencyDic[CurrencyType.CurMaxStamina] = (CurMaxStaminaCurrency)item;
                }
                else if (item is CharacterEXPSO)
                {
                    CurrencyDic[CurrencyType.CharacterEXP] = (CharacterEXPSO)item;
                }
                else if (item is UserLevelSO)
                {
                    CurrencyDic[CurrencyType.UserLevel] = (UserLevelSO)item;
                }
                else if (item is UserEXPSO)
                {
                    CurrencyDic[CurrencyType.UserEXP] = (UserEXPSO)item;
                }
                else if (item is PurchaseCurrencySO)
                {
                    CurrencyDic[CurrencyType.purchaseCount] = (PurchaseCurrencySO)item;
                }

            }
        }
        else
        {
            Debug.Log("½ÇÆÐ");
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
