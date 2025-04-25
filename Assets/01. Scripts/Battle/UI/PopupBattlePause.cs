using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupBattlePause : UIPopup
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    public Action OnCancel;
    public Action OnConfirm;

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirmButton);
        cancelButton.onClick.AddListener(OnCancelButton);
    }

    private void OnCancelButton()
    {
        OnCancel?.Invoke();
        OnCancel = null;
        Destroy();
    }

    private void OnConfirmButton()
    {
        OnConfirm?.Invoke();
        OnConfirm = null;
        Destroy();
    }
}
