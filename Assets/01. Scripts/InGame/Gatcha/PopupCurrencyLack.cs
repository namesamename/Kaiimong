using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCurrencyLack : UIPOPUP
{
    [SerializeField] private Button cancleButton;
    [SerializeField] private Button purchaseButton;

    public CurrencyType currencyType;
    Action setUI;

    private void Start()
    {
        cancleButton.onClick.AddListener(Destroy);
        purchaseButton.onClick.AddListener(OnPurchaseButton);
    }

    private async void OnPurchaseButton()
    {
        switch (currencyType)
        {
            case CurrencyType.Activity:
                UIStaminaChargePOPUP popup = await UIManager.Instance.ShowPopup("StaminaChargePOPUP") as UIStaminaChargePOPUP;
                popup.ClearAction();
                popup.OnChargeStamina += setUI;
                Destroy();
                break;
        }
    }

    public void SetDelegate(Action action)
    {
        setUI = action;
    }
}
