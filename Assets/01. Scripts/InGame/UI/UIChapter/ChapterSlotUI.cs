using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSlotUI : MonoBehaviour
{
    public Chapter Chapter;
    [SerializeField] private Image chapterIcon;
    [SerializeField] private TextMeshProUGUI chapterNameText;
    [SerializeField] private Button chapterButton;
    [SerializeField] private Image blurIcon;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    [SerializeField] private float finalPos = -350f;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        Appear();
    }

    void Start()
    {
        chapterButton.onClick.AddListener(OnChapterButton);
        SetChapterSlot();

    }

    private void SetChapterSlot()
    {
        //ChapterManager.Instance.InitializeChapter(Chapter.ID);
        chapterIcon.sprite = Resources.Load<Sprite>(Chapter.IconPath);
        chapterNameText.text = Chapter.Name;
        ChapterSaveData chapterData = ChapterManager.Instance.GetChapterSaveData(Chapter.ID);
        if (chapterData != null)
        {
            if (!chapterData.ChapterOpen)
            {
                blurIcon.gameObject.SetActive(true);
            }
            else
            {
                blurIcon.gameObject.SetActive(false);
            }
        }
    }

    private void OnChapterButton()
    {
        ChapterSaveData chapterData = ChapterManager.Instance.GetChapterSaveData(Chapter.ID);
        if (chapterData != null && chapterData.ChapterOpen)
        {
            ChapterManager.Instance.RegisterChapter(Chapter);
            SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);
        }
        else
        {
            Debug.Log("Null");
        }
    }

    private void Appear()
    {
        Vector2 startPos = rect.anchoredPosition;
        startPos.y = finalPos - 300;
        rect.anchoredPosition = startPos;
        canvasGroup.alpha = 0f;

        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPosY(finalPos, 1f).SetEase(Ease.OutCubic));
        seq.Join(canvasGroup.DOFade(1f, 2f));
    }
}
