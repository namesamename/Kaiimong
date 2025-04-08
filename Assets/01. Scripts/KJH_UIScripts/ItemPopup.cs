using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemPopup : MonoBehaviour
{
    public RectTransform rect;
    public Image itemIcon;
    public Text itemName;

    private Vector2 hiddenPos = new Vector2(0, -300);
    private Vector2 visiblePos = new Vector2(0, 0);

    public float duration = 0.5f;

    public void Show(Sprite icon, string name)
    {
        itemIcon.sprite = icon;
        itemName.text = name;

        rect.anchoredPosition = hiddenPos;
        gameObject.SetActive(true);

        rect.DOAnchorPos(visiblePos, duration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // 일정 시간 후 자동으로 사라지기
                Invoke(nameof(Hide), 1.2f);
            });
    }

    void Hide()
    {
        rect.DOAnchorPos(hiddenPos, duration * 0.6f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
               // ItemPopupManager.Instance.ReturnToPool(this);
            });
    }
}
