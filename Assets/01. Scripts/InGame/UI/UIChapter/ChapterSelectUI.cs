using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectUI : MonoBehaviour
{
    public ChapterCategory CurChapterCategory;
    [SerializeField] private GameObject contentBox;
    [SerializeField] private GameObject categoryButtonBox;
    [SerializeField] private Button backButton;

    public List<ChapterSlotUI> slots = new List<ChapterSlotUI>();

    [SerializeField] private RectTransform[] buttonsRect;
    [SerializeField] private float buttonPosY;

    void Start()
    {
        MoveUI();
        backButton.onClick.AddListener(OnBackButton);
        SetCategoryButtons();
        InitChapter();
    }

    private void InitChapter()
    {
        CurChapterCategory = GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic[1];
        SetChapterSlots(1);
    }

    public void SetChapterSlots(int chapterID)
    {
        if (slots.Count > 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Destroy(slots[i].gameObject);
            }

            slots.Clear();
        }
        CurChapterCategory = GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic[chapterID];
        for (int i = 0; i < CurChapterCategory.ChaptersID.Length; i++)
        {
            int chapterDataID = CurChapterCategory.ChaptersID[i];
            Chapter chapterData = GlobalDataTable.Instance.Chapter.ChapterDic[chapterDataID];

            GameObject obj = Resources.Load("UI/Chapter/ChapterSlotUI") as GameObject;
            GameObject clone = Instantiate(obj, contentBox.transform);
            ChapterSlotUI objSlot = clone.GetComponent<ChapterSlotUI>();
            objSlot.Chapter = chapterData;
            slots.Add(objSlot);

        }
    }

    private void SetCategoryButtons()
    {
        for (int i = 1; i < GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic.Count + 1; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/Chapter/CategoryButton") as GameObject, categoryButtonBox.transform);
            obj.GetComponent<CategoryButton>().categoryID = i;
            obj.GetComponent<CategoryButton>().categoryName.text = GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic[i].Name;
        }

    }

    private void OnBackButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
    }

    private void MoveUI()
    {
        for (int i = 0; i < buttonsRect.Length; i++)
        {
            buttonsRect[i].DOLocalMoveY(buttonPosY, 1f);
        }
    }
}
