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

    [Header("������ ����")]
    [SerializeField] private Sprite goldIcon;
    [SerializeField] private Sprite crystalIcon;

    public void Setup(PackageGroup group, Shop shop)
    {
        titleText.text = $"��Ű�� {group.ShopID}";

        // ���� ���� ����
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // ������ ���� ����
        foreach (var package in group.Packages)
        {
            var slotObj = Instantiate(itemSlotPrefab, itemContainer);
            var slotUI = slotObj.GetComponent<ItemSlotUI>();
            slotUI.Setup(package);
        }

        // ���� �� ȭ�� ������ ����
        priceText.text = shop.Price.ToString();
        priceIcon.sprite = (shop.MoneyID == 1 ? goldIcon : crystalIcon);

        // ���� ��ư�� ������ �ƹ� ���� ����
        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = false; // �Ǵ� ��Ȱ��ȭ���� �ʰ� �׳� ����α�
    }
}
