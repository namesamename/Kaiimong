using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectUI : MonoBehaviour
{
    public ChapterCategory CurChapterCategory;
    [SerializeField] private GameObject contentBox;
    [SerializeField] private GameObject categoryButtonBox;
    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(OnBackButton);
    }

    void Update()
    {
        
    }

    private void InitChapter()
    {
        CurChapterCategory = ChapterManager.Instance.CategoryDataTable.ChapterCategoryDic[0];
    }

    private void SetChapterSlots()
    {
        for(int i = 0; i < CurChapterCategory.ChaptersID.Length; i++)
        {
            Instantiate(Resources.Load("Chapter/UI/ChapterSlotUI") as GameObject, contentBox.transform);
        }
    }

    private void SetCategoryButtons()
    {
        
        GameObject obj = Instantiate(Resources.Load("Chapter/UI/CategoryButton") as GameObject, categoryButtonBox.transform);
        obj.GetComponent<CategoryButton>().categoryID = i;
    }

    private void OnBackButton()
    {
        gameObject.SetActive(false);
    }
}
