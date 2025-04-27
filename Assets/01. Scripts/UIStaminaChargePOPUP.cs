using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaChargePOPUP : UIBreakPOPUP
{
    Button[] Buttons;
    Image[] Images;
    TextMeshProUGUI[] textMesh;

    private void Awake()
    {
        Buttons = GetComponentsInChildren<Button>();
        Images = GetComponentsInChildren<Image>();
        textMesh = GetComponentsInChildren<TextMeshProUGUI>();

    }


    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++) 
        {
            Buttons[i].onClick.RemoveAllListeners();
        }

        BTNSet();

    }
    public void BTNSet()
    {

        Buttons[0].onClick.AddListener(Destroy);
        Buttons[1].onClick.AddListener(Destroy);
        //개별 조건을 달아준다

        //if(SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, 0).Value > 0)
        //{
        //    Buttons[2].onClick.AddListener(SmallStaminaUse);
        //}
        //if (SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, 0).Value > 0)
        //{ 
        //    Buttons[3].onClick.AddListener(BigStaminaUse);
        //}
        //if (SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, 0).Value > 0)
        //{ 
        //    Buttons[4].onClick.AddListener(CrystalStaminaUse);
        //}
    }

    public void ImageSET()
    {
        //Images[3].sprite = Resources.Load<Sprite>( GlobalDataTable.Instance.Item.GetItemDataToID(0).IconPath);
        //Images[4].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.Item.GetItemDataToID(0).IconPath);
        //Images[5].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<DIaCurrencySO>(CurrencyType.Dia).IconPath);

    }

    public void TextSet()
    {
        textMesh[1].text = ItemManager.Instance.GetItemSaveData(1).Value.ToString();
        textMesh[2].text = ItemManager.Instance.GetItemSaveData(2).Value.ToString();
        textMesh[3].text = ItemManager.Instance.GetItemSaveData(3).Value.ToString();
        textMesh[4].text = $"{CurrencyManager.Instance.GetCurrency(CurrencyType.Activity)}/{CurrencyManager.Instance.GetCurrency(CurrencyType.CurMaxStamina)} ";
    }

    public void SmallStaminaUse()
    {
        CurrencyManager.Instance.HealStamina((GlobalDataTable.Instance.Item.GetConsume(1).Value));
    }

    public void BigStaminaUse()
    {
        CurrencyManager.Instance.HealStamina((GlobalDataTable.Instance.Item.GetConsume(2).Value));
    }

    public void CrystalStaminaUse()
    {
        CurrencyManager.Instance.HealStamina(100);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Dia, -100);
    }


}
