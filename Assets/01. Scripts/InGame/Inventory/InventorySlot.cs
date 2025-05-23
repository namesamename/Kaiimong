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


    Image[] images;

    private ItemData item;                    // ���� ���Կ� ����ִ� ������
    private int itemCount;                    // �ش� ������ ����

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }
    public void SetSlot(ItemData newItem, int amount = 1)     // ���Կ� �� ������ ���� ����
    {
    
        item = newItem;
        itemCount = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, item.ID).Value;

        if (item != null)
        {
            SetSlotColor(newItem);
            iconImage.sprite = Resources.Load<Sprite>(item.IconPath);
            iconImage.enabled = true;        // ������  Ȱ��ȭ
            if (itemCount > 1)
            {
                countText.text = itemCount.ToString();
            }
            else
            {
                countText.text = string.Empty;
            }

            outlineEffect.enabled = false;   // ���� ���� �� �ܰ��� ��Ȱ��ȭ
        }
        else
        {
            ClearSlot();                    // �������� null�̸� ���� ����
        }
    }


    public void SetSlotColor(ItemData item)
    {
        switch (item.Grade)
        {
            case ERarity.S:
                images[0].color = Color.yellow;
                break;
            case ERarity.A:
                images[0].color = Color.magenta;
                break;
            case ERarity.B:
                images[0].color = Color.blue;
                break;
            case ERarity.C:
                images[0].color = Color.green;
                break;
            case ERarity.D:
                images[0].color = Color.gray;
                break;
        }

    }


    public  async void SetSlotCurrency(CurrencyType currency, int Count)
    {


        iconImage.enabled = true;        // ������  Ȱ��ȭ
        if (Count > 1)
        {
            countText.text = Count.ToString();
        }
        else
        {
            countText.text = string.Empty;
        }

        switch (currency) 
        {
            case CurrencyType.UserEXP:
                
                break;
            case CurrencyType.Gold:
                iconImage.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 1);
                break;
            case CurrencyType.Gacha:
                break;
            case CurrencyType.Activity:
                break;
            case CurrencyType.Dia:
                break;
            case CurrencyType.CharacterEXP:
                iconImage.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 2);
                break;


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




    public  async void OnPointerClick(PointerEventData eventData)    // ���� Ŭ�� �� �˾� ȣ��
    {
        if (item == null) return;                             // �������� ��������� ����

        var popupObj = await UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager�� ShowPopup �Լ� ȣ��

        if (popupObj != null)
        {
            popupObj.ShowItem(item);  // ������ ������ ����
        }
    }

    /* public void OnPointerClick(PointerEventData eventData)      // ���� Ŭ�� �� �˾� ȣ��
     {
         if (item == null) return;                                 // ���Կ� �������� ������ ���ư���

         ItemInfoPopup popup = FindObjectOfType<ItemInfoPopup>();  // ������ ItemInfoPopup ������Ʈ
         if (popup != null)
             popup.Show(item, itemCount);
     }*/
}

