using System.Collections;
using System.Collections.Generic;
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
    }

    void Update()
    {
        
    }

    private void OnChapterButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);
    }
}
