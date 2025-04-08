using UnityEngine;
using DG.Tweening;

public class UIPopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private RectTransform popupRect;

    [SerializeField] private Vector2 hiddenPosition = new Vector2(0, -1000f);  // ¾Æ·¡ÂÊ
    [SerializeField] private Vector2 visiblePosition = new Vector2(0, 0);      // Áß¾Ó

    [SerializeField] private float duration = 0.5f;

    void Start()
    {
        popupPanel.SetActive(false);
    }

    public void OpenPopup()
    {
        popupPanel.SetActive(true);
        popupRect.anchoredPosition = hiddenPosition;

        popupRect.DOAnchorPos(visiblePosition, duration)
                 .SetEase(Ease.OutBack);
    }

    public void ClosePopup()
    {
        popupRect.DOAnchorPos(hiddenPosition, duration)
                 .SetEase(Ease.InBack)
                 .OnComplete(() =>
                 {
                     popupPanel.SetActive(false);
                 });
    }
}
