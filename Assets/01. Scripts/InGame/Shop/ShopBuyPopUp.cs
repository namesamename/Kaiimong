using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyPopUp : UIPOPUP
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action onConfirm;

    public void Setup(Action confirmCallback)
    {
        onConfirm = confirmCallback;

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            onConfirm?.Invoke();
            Destroy(); // 혹은 Destroy(gameObject);
        });

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() =>
        {
            Destroy(); // 혹은 Destroy(gameObject);
        });
    }
}
