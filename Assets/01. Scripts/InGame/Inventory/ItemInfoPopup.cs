using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPopup : UIPOPUP
{
    [Header("UI References")]
    [SerializeField] private Image itemImage;                      // ������ ������
    [SerializeField] private TextMeshProUGUI itemCountText;        // ����
    [SerializeField] private TextMeshProUGUI itemNameText;         // �̸�
    [SerializeField] private TextMeshProUGUI itemDescriptionText;  // ����
    [SerializeField] private TextMeshProUGUI itemObtainText;       // ȹ�� ���

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
        //itemImage.sprite = Resources.Load<Sprite>(data.IconPath);                              // ������ �̹��� ����
        itemCountText.text = $"x{count}";                          // ���� ǥ��
        itemNameText.text = data.Name;                             // �̸� �ؽ�Ʈ ����
        itemDescriptionText.text = data.Description;              // ���� �ؽ�Ʈ ����
        itemObtainText.text = $"ȹ�� ���: {data.ObtainLocation}"; // ȹ�� ��� �ؽ�Ʈ ����

        gameObject.SetActive(true);                                // �˾� Ȱ��ȭ
    }


}
