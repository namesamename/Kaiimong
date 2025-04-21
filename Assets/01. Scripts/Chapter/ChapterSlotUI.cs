using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSlotUI : MonoBehaviour
{
    public Chapter Chapter;
    [SerializeField] private Image chapterIcon;
    [SerializeField] private TextMeshProUGUI chapterNameText;
    [SerializeField] private Button chapterButton;

    void Start()
    {
        chapterButton.onClick.AddListener(OnChapterButton);
        SetChapterSlot();
    }

    private void SetChapterSlot()
    {
        chapterIcon.sprite = Resources.Load<Sprite>(Chapter.IconPath);
        chapterNameText.text = Chapter.Name;
    }

    private void OnChapterButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);
    }
}
