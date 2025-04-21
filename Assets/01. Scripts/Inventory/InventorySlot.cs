using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

//���� �ϳ� ����ϴ� ��ũ��Ʈ

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;                 // ���� ���� ������ �̹���
    [SerializeField] private TextMeshProUGUI countText;       //������ ���� �ؽ�Ʈ
    [SerializeField] private Outline outlineEffect;           // ���� �ܰ��� ȿ��


    private ItemData item;                    // ���� ���Կ� ����ִ� ������
    private int itemCount;                    // �ش� ������ ����

    public void SetSlot(ItemData newItem, int amount = 1)     // ���Կ� �� ������ ���� ����
    {
        item = newItem;
        itemCount = amount;

        if (item != null)
        {
            iconImage.sprite = Resources.Load<Sprite>(item.IconPath);
            iconImage.enabled = true;        // ������  Ȱ��ȭ
            countText.text = itemCount.ToString();  // ���� ǥ��
            outlineEffect.enabled = false;   // ���� ���� �� �ܰ��� ��Ȱ��ȭ
        }
        else
        {
            ClearSlot();                    // �������� null�̸� ���� ����
        }
    }

    public void ClearSlot()                 // ���� ����
    {
        item = null;                        // ������ ����
        itemCount = 0;                      // ���� �ʱ�ȭ

        iconImage.sprite = null;            // �̹��� ����
        iconImage.enabled = false;          // �̹��� ����

    }

    public bool TryConsume(int amount)                // ������ ������ ������ŭ �Һ�
    {
        if (item.ItemType != EItemType.Consume)         // ������ Ÿ���� �Ҹ�ǰ�� �ƴϸ�
            return false;                             // �Һ� ���� (�Ҹ�ǰ�� ��� ����)

        if (itemCount >= amount)                       // ���� ������ �Һ��Ϸ��� �� �̻��̸�
        {
            itemCount -= amount;                      // ���� ����
            countText.text = itemCount > 0 ? itemCount.ToString() : "";  // ���� ǥ��, 0�̸� ǥ�� ����

            if (itemCount == 0)
                ClearSlot();                              // ���� 0�̸� ���� ����

            return true;                              // �Һ� ����
        }
        else
        {
            Debug.Log("��� ������ ����");
            return false;                             // �Һ� ����
        }
    }
    public ItemData GetItem()                         // ���� ������ ���� ��ȯ
    {
        return item;
    }

    public int GetCount()                             // ���� ������ ���� ��ȯ
    {
        return itemCount;
    }




    public void OnPointerClick(PointerEventData eventData)    // ���� Ŭ�� �� �˾� ȣ��
    {
        if (item == null) return;                             // �������� ��������� ����

        var popupObj = UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager�� ShowPopup �Լ� ȣ��

        if (popupObj != null)
        {
            popupObj.Show(item, itemCount);  // ������ ������ ����
        }
    }
}

