using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageGroupItemUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Image priceIcon;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    [Header("아이콘 설정")]
    [SerializeField] private Sprite goldIcon;
    [SerializeField] private Sprite crystalIcon;

    public void Setup(PackageGroup group, Shop shop)
    {
        titleText.text = $"패키지 {group.ShopID}";

        // 기존 슬롯 제거
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // 아이템 슬롯 생성
        foreach (var package in group.Packages)
        {
            var slotObj = Instantiate(itemSlotPrefab, itemContainer);
            var slotUI = slotObj.GetComponent<ItemSlotUI>();
            slotUI.Setup(package);
        }

        // 가격 및 화폐 아이콘 설정
        priceText.text = shop.Price.ToString();
        priceIcon.sprite = (shop.MoneyID == 1 ? goldIcon : crystalIcon);

        // 구매 버튼은 눌러도 아무 동작 안함
        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = false; // 또는 비활성화하지 않고 그냥 비워두기
    }
}
