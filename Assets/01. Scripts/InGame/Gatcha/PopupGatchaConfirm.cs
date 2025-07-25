using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupGatchaConfirm : UIPOPUP
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private System.Action onConfirmAction;

    public void Setup(string message, System.Action onConfirm)
    {
        messageText.text = message;
        onConfirmAction = onConfirm;

        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() => {
            onConfirmAction?.Invoke();
            Hide();
        });

        cancelButton.onClick.AddListener(Hide);
    }
}
