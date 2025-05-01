using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPopup : UIPOPUP
{
    [Header("UI References")]
    [SerializeField] private Image itemImage;                      // 아이템 아이콘
    [SerializeField] private TextMeshProUGUI itemCountText;        // 수량
    [SerializeField] private TextMeshProUGUI itemNameText;         // 이름
    [SerializeField] private TextMeshProUGUI itemDescriptionText;  // 설명
    [SerializeField] private TextMeshProUGUI itemObtainText;       // 획득 경로

    Button Button;
    private void Awake()
    {
        Button = GetComponentInChildren<Button>();
    }
    private void Start()
    {
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(Destroy);
    }
    public void Show(ItemData data, int count)
    {
        //itemImage.sprite = Resources.Load<Sprite>(data.IconPath);                              // 아이콘 이미지 설정
        itemCountText.text = $"x{count}";                          // 수량 표시
        itemNameText.text = data.Name;                             // 이름 텍스트 설정
        itemDescriptionText.text = data.Description;              // 설명 텍스트 설정
        itemObtainText.text = $"획득 경로: {data.ObtainLocation}"; // 획득 경로 텍스트 설정

        gameObject.SetActive(true);                                // 팝업 활성화
    }


}
