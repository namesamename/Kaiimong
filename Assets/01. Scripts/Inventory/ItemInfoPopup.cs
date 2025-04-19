using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPopup : UIPopup
{
    [Header("UI References")]
    [SerializeField] private Image itemImage;                      // ������ ������
    [SerializeField] private TextMeshProUGUI itemCountText;        // ����
    [SerializeField] private TextMeshProUGUI itemNameText;         // �̸�
    [SerializeField] private TextMeshProUGUI itemDescriptionText;  // ����
    [SerializeField] private TextMeshProUGUI itemObtainText;       // ȹ�� ���
    public void Show(ItemData data, int count)
    {
        itemImage.sprite = data.Icon;                              // ������ �̹��� ����
        itemCountText.text = $"x{count}";                          // ���� ǥ��
        itemNameText.text = data.Name;                             // �̸� �ؽ�Ʈ ����
        itemDescriptionText.text = data.Description;              // ���� �ؽ�Ʈ ����
        itemObtainText.text = $"ȹ�� ���: {data.ObtainLocation}"; // ȹ�� ��� �ؽ�Ʈ ����

        gameObject.SetActive(true);                                // �˾� Ȱ��ȭ
    }


}
