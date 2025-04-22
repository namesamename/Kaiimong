using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public int categoryID;

    private ChapterSelectUI chapterSelectUI;
    private Button categoryButton;

    private void Awake()
    {
        chapterSelectUI = GetComponentInParent<ChapterSelectUI>();
        categoryButton = GetComponent<Button>();
    }

    void Start()
    {
        categoryButton.onClick.AddListener(OnCategoryButton);
    }

    private void OnCategoryButton()
    {
        chapterSelectUI.SetChapterSlots(categoryID);
    }
}
