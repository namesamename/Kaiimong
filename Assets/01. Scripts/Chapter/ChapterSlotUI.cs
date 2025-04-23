using System.Linq;
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

    void Start()
    {
        chapterButton.onClick.AddListener(OnChapterButton);
        SetChapterSlot();
    }

    private void SetChapterSlot()
    {
        ChapterManager.Instance.InitializeChapter(Chapter.ID);
        chapterIcon.sprite = Resources.Load<Sprite>(Chapter.IconPath);
        chapterNameText.text = Chapter.Name;
        if (!SaveDataBase.Instance.GetSaveDataToID<ChapterSaveData>(SaveType.Chapter, Chapter.ID).ChapterOpen)
        {
            blurIcon.gameObject.SetActive(true);
        }
        else
        {
            blurIcon.gameObject.SetActive(false);
        }

    }

    private void OnChapterButton()
    {
        if (SaveDataBase.Instance.GetSaveDataToID<ChapterSaveData>(SaveType.Chapter, Chapter.ID).ChapterOpen)
        {
            ChapterManager.Instance.CurChapter = Chapter;
            SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);
        }
    }
}
