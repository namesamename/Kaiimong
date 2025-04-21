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

    void Start()
    {
        backButton.onClick.AddListener(OnBackButton);
        SetCategoryButtons();
        InitChapter();
    }

    private void InitChapter()
    {
        CurChapterCategory = GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic[0];
        SetChapterSlots(0);
    }

    public void SetChapterSlots(int chapterID)
    {
        CurChapterCategory = GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic[chapterID];
        for (int i = 0; i < CurChapterCategory.ChaptersID.Length; i++)
        {
            int chapterDataID = CurChapterCategory.ChaptersID[i];
            Chapter chapterData = GlobalDataTable.Instance.Chapter.ChapterDic[chapterDataID];

            ChapterSlotUI reusableSlot = slots.Find(slot => slot.Chapter == chapterData);
            if (reusableSlot != null)
            {
                reusableSlot.gameObject.SetActive(true);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load("Chapter/UI/ChapterSlotUI") as GameObject, contentBox.transform);
                ChapterSlotUI objSlot = obj.GetComponent<ChapterSlotUI>();
                objSlot.Chapter = chapterData;
                slots.Add(objSlot);
            }
        }
    }

    private void SetCategoryButtons()
    {
        for (int i = 0; i < GlobalDataTable.Instance.ChapterCategory.ChapterCategoryDic.Count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Chapter/UI/CategoryButton") as GameObject, categoryButtonBox.transform);
            obj.GetComponent<CategoryButton>().categoryID = i;
        }

    }

    private void OnBackButton()
    {
        gameObject.SetActive(false);
    }
}
