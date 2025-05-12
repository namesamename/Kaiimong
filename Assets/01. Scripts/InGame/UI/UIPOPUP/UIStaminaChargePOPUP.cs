using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaChargePOPUP : UIBreakPOPUP
{
    Button[] Buttons;
    Image[] Images;
    TextMeshProUGUI[] textMesh;


    TopBarUI topBarUI;


    int CrystalValue = 40;

    private void Awake()
    {
        Buttons = GetComponentsInChildren<Button>();
        Images = GetComponentsInChildren<Image>();
        textMesh = GetComponentsInChildren<TextMeshProUGUI>();
        topBarUI = FindAnyObjectByType<TopBarUI>();
    }


    private void Update()
    {
        textMesh[0].text = $"{CurrencyManager.Instance.GetCurrency(CurrencyType.Activity)}/{CurrencyManager.Instance.GetCurrency(CurrencyType.CurMaxStamina)}";
    }


    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++) 
        {
            Buttons[i].onClick.RemoveAllListeners();
        }

        BTNSet();
        TextSet();
        transform.GetChild(8).gameObject.SetActive(false);
    }
    public void BTNSet()
    {

        Buttons[0].onClick.AddListener(Destroy);
        Buttons[1].onClick.AddListener(Destroy);


        Buttons[2].onClick.AddListener(SmallStaminaUseSetting);
        Buttons[3].onClick.AddListener(BigStaminaUseSetting);
        Buttons[4].onClick.AddListener(CrystalStaminaUseSetting);
   
    }

    public void ImageSET()
    {
        //Images[3].sprite = Resources.Load<Sprite>( GlobalDataTable.Instance.Item.GetItemDataToID(0).IconPath);
        //Images[4].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.Item.GetItemDataToID(0).IconPath);
        //Images[5].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<DIaCurrencySO>(CurrencyType.Dia).IconPath);

    }

    public void TextSet()
    {
       
        textMesh[2].text = ItemManager.Instance.GetItemSaveData(9).Value.ToString();
        textMesh[3].text = ItemManager.Instance.GetItemSaveData(10).Value.ToString();
        if(CurrencyManager.Instance.GetCurrency(CurrencyType.purchaseCount) <= 1) 
        {
            CrystalValue = 40;
            textMesh[4].text = CrystalValue.ToString();
        }
        else if(CurrencyManager.Instance.GetCurrency(CurrencyType.purchaseCount) <= 3)
        {
            CrystalValue = 80;
            textMesh[4].text = CrystalValue.ToString();
        }
        else if (CurrencyManager.Instance.GetCurrency(CurrencyType.purchaseCount) <= 5)
        {
            CrystalValue = 120;
            textMesh[4].text = CrystalValue.ToString();
        }
        else if (CurrencyManager.Instance.GetCurrency(CurrencyType.purchaseCount) <= 7)
        {
            CrystalValue = 160;
            textMesh[4].text = CrystalValue.ToString();
        }
        else if (CurrencyManager.Instance.GetCurrency(CurrencyType.purchaseCount) <= 9)
        {
            CrystalValue = 200;
            textMesh[4].text = CrystalValue.ToString();
        }
        else
        {
            Buttons[4].enabled = false;
            textMesh[4].text = "Today MAX";
        }



    }

    public void SmallStaminaUseSetting()
    {
        Buttons[5].onClick.RemoveAllListeners();
        textMesh[5].text = "smallstamina";
        transform.GetChild(8).gameObject.SetActive(false);

        if (ItemManager.Instance.GetItemSaveData(9) != null && ItemManager.Instance.GetItemSaveData(9).Value > 0)
        {
            Buttons[5].interactable = true;
            Buttons[5].onClick.AddListener(SmallStaminaUse);
        }
        else
        {
            Buttons[5].interactable = false;
        }
    }

    public void PurchaseButtonReset()
    {
        textMesh[5].text = "Set Item";
        Buttons[5].onClick.RemoveAllListeners();
    }
    public void SmallStaminaUse()
    {
        CurrencyManager.Instance.HealStamina((GlobalDataTable.Instance.Item.GetConsume(1).Value));
        ItemManager.Instance.SetitemCount(9, -1);
        TextSet();
        PurchaseButtonReset();
        topBarUI.UpdateResource();
    }

    public void BigStaminaUseSetting()
    {
        Buttons[5].onClick.RemoveAllListeners();
        textMesh[5].text = "Bigstamina";
        transform.GetChild(8).gameObject.SetActive(false);


        if (ItemManager.Instance.GetItemSaveData(10) != null && ItemManager.Instance.GetItemSaveData(10).Value > 0)
        {
            Buttons[5].interactable = true;
            Buttons[5].onClick.AddListener(BigStaminaUse);
        }
        else
        {
            Buttons[5].interactable = false;
        }

    }

    public void BigStaminaUse()
    {
        CurrencyManager.Instance.HealStamina((GlobalDataTable.Instance.Item.GetConsume(2).Value));
        ItemManager.Instance.SetitemCount(10, -1);
        TextSet();
        PurchaseButtonReset();
        topBarUI.UpdateResource();
    }

    public void CrystalStaminaUseSetting()
    {
        Buttons[5].onClick.RemoveAllListeners(); Buttons[5].onClick.RemoveAllListeners();
        textMesh[5].text = "Crystal";
        transform.GetChild(8).gameObject.SetActive(true);
        transform.GetChild(8).GetComponentInChildren<TextMeshProUGUI>().text = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia).ToString();

        if (CurrencyManager.Instance.GetCurrency(CurrencyType.Dia) >= CrystalValue && CurrencyManager.Instance.GetCurrency(CurrencyType.purchaseCount)<10)
        {
            Buttons[5].interactable = true;
            Buttons[5].onClick.AddListener(CrstalStaminaUse);
        }
        else
        {
            transform.GetChild(8).gameObject.SetActive(true);
            transform.GetChild(8).GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            Buttons[5].interactable = false;
        }

     

    }


    public void CrstalStaminaUse()
    {
        CurrencyManager.Instance.HealStamina(100);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Dia, -CrystalValue);
        CurrencyManager.Instance.SetCurrency(CurrencyType.purchaseCount, 1);
        TextSet();
        PurchaseButtonReset();
        transform.GetChild(8).gameObject.SetActive(false);
        topBarUI.UpdateResource();
    }


}
