using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            iconImage.sprite = item.Icon;    // ������ �̹��� ����
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

    public bool TryConsume()                // �Һ��ϴ� ���
    {

        // �Һ� ���� ���� Ȯ��
        if (itemCount >= 1)
    {
        itemCount -= 1;                         // 1�� �Һ�
        countText.text = itemCount.ToString();  // ���� �ؽ�Ʈ ����

        if (itemCount == 0)
            ClearSlot();                        // ���� 0�̸� ���� ����

            return true;                                     // �Һ� ����
        }
        else
        {
            Debug.Log("��� ������ ����");
            return false;                                    // �Һ� ����
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




    public void OnPointerClick(PointerEventData eventData)    // ���� Ŭ�� �� �˾� ȣ��
    {
        if (item == null) return;                             // �������� ��������� ����

        GameObject popupObj = UIManager.Instance.ShowPopup("ItemInfoPopup");    // UIManager�� ShowPopup �Լ� ȣ��

    }
}

