using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyPopUp : UIPOPUP
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action onConfirm;

    public async void Setup(Action confirmCallback)
    {
        onConfirm = confirmCallback;

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(async () =>
        {
            onConfirm?.Invoke();
            Destroy();
            var popup = await UIManager.Instance.ShowPopup("CompletedPurchasePopup") as CompletedPurchasePopup;
        });

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() =>
        {
            Destroy();
        });
    }
}
