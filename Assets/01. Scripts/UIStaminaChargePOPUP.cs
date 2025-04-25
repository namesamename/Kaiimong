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
            Buttons[0].onClick.RemoveAllListeners();
        }

    }


    public void BTNSet()
    {

        Buttons[0].onClick.AddListener(Destroy);
        Buttons[1].onClick.AddListener(Destroy);
        //개별 조건을 달아준다
        Buttons[2].onClick.AddListener(SmallStaminaUse);
        Buttons[3].onClick.AddListener(BigStaminaUse);
        Buttons[4].onClick.AddListener(CrystalStaminaUse);
    }

    public void ImageSET()
    {
        //Images[3].sprite = Resources.Load<Sprite>( GlobalDataTable.Instance.Item.GetItemDataToID(0).IconPath);
        //Images[4].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.Item.GetItemDataToID(0).IconPath);
        //Images[5].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<DIaCurrencySO>(CurrencyType.Dia).IconPath);

    }

    public void TextSet()
    {
      
    }

    public void SmallStaminaUse()
    {

    }

    public void BigStaminaUse()
    {
        
    }

    public void CrystalStaminaUse()
    {

    }


}
