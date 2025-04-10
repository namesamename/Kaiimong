using TMPro;
using UnityEngine;
using UnityEngine.UI;

//���� �ϳ� ����ϴ� ��ũ��Ʈ

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;                 // ���� ���� ������ �̹���
    [SerializeField] private TextMeshProUGUI countText;       //������ ���� �ؽ�Ʈ
    [SerializeField] private Outline outlineEffect;           // ���� �ܰ��� ȿ��


    private ItemData item;                    // ���� ���Կ� ����ִ� ������
    private int itemCount;                    // �ش� ������ ����

    public void SetSlot(ItemData newItem, int amount = 1)     // ���Կ� ������ ����
    {
        item = newItem;
        itemCount = amount;

        if (item != null)
        {
            iconImage.sprite = item.Icon;    // ������ ����
            iconImage.enabled = true;        // ������ ǥ��
            countText.text = itemCount > 1 ? itemCount.ToString() : ""; // 1���� �� ���� ���� ���� ����
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

    public bool TryConsume(int amount)      //������ ������ ����ŭ �Һ�
    {
        if (itemCount >= amount)
        {
            itemCount -= amount;                                        // ���� ����
            countText.text = itemCount > 0 ? itemCount.ToString() : ""; // ���� ������Ʈ
            {
                if (itemCount == 0)                                         // 0�� �Ǹ� ���� ����
                    ClearSlot();
            }
            return true;                      // �Һ� ����
        }
        else
        {
            Debug.Log("��� ������ �����մϴ�.");
            return false;                         // �Һ� ����
        }
    }
    public ItemData GetItem()               // ���� ������ ���� ��ȯ
    {
        return item;
    }

    public int GetCount()                   // ���� ������ ���� ��ȯ
    {
        return itemCount;
    }




    /* public void OnPointerClick(PointerEventData eventData)    // ���� Ŭ�� �� �˾� ȣ��
     {
         if (item == null) return;

         ItemInfoPopup popup = FindObjectOfType<ItemInfoPopup>();
         if (popup != null)
             popup.Show(item, itemCount);
     }*/
}

