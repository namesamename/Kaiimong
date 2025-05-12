using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCurrencyLack : UIPOPUP
{
    [SerializeField] private Button cancleButton;
    [SerializeField] private Button purchaseButton;

    public CurrencyType currencyType;

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
                await UIManager.Instance.ShowPopup<UIStaminaChargePOPUP>();
                Destroy();
                break;
        }

    }
}
