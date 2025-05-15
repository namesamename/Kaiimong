using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShopTabSlider : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private int tabCount = 4;
    [SerializeField] private float snapDuration = 0.3f;

    private float[] positions;      // 각 탭의 위치
    private bool isDragging = false;

    private void Awake()
    {
        positions = new float[tabCount];
        float step = 1f / (tabCount - 1);

        for (int i = 0; i < tabCount; i++)
        {
            positions[i] = step * i;
        }

        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    public void MoveToTab(int index)
    {
        if (index < 0 || index >= tabCount) return;

        float target = positions[index];

        // 이동 중 드래그 방지
        scrollRect.enabled = false;

        DOTween.To(() => scrollRect.horizontalNormalizedPosition,
                   value => scrollRect.horizontalNormalizedPosition = value,
                   target, snapDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => {
                scrollRect.enabled = true;
            });
    }


    private void OnScroll(Vector2 pos)
    {
        if (!isDragging)
        {
            SnapToNearest();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        SnapToNearest();
    }

    private void SnapToNearest()
    {
        float currentPos = scrollRect.horizontalNormalizedPosition;
        float closest = positions[0];
        int closestIndex = 0;

        for (int i = 1; i < positions.Length; i++)
        {
            if (Mathf.Abs(currentPos - positions[i]) < Mathf.Abs(currentPos - closest))
            {
                closest = positions[i];
                closestIndex = i;
            }
        }

        MoveToTab(closestIndex);
    }
}
