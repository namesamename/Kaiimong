using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public int categoryID;

    private ChapterSelectUI chapterSelectUI;
    private Button categoryButton;
    public TextMeshProUGUI categoryName;

    private RectTransform rect;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        chapterSelectUI = GetComponentInParent<ChapterSelectUI>();
        categoryButton = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Start()
    {
        Appear();
        categoryButton.onClick.AddListener(OnCategoryButton);
    }

    private void OnCategoryButton()
    {
        chapterSelectUI.SetChapterSlots(categoryID);
    }


    private void Appear()
    {
        float finalPos = rect.anchoredPosition.y;
        Vector2 startPos = rect.anchoredPosition;
        startPos.y = finalPos - 200;
        rect.anchoredPosition = startPos;
        canvasGroup.alpha = 0f;

        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPosY(finalPos, 1f).SetEase(Ease.OutCubic));
        seq.Join(canvasGroup.DOFade(1f, 2f));
    }
}
