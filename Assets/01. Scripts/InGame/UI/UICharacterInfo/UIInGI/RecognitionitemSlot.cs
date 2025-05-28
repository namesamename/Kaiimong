using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecognitionitemSlot : MonoBehaviour, IPointerClickHandler
{
    private Image[] iconImage;                 // ���� ���� ������ �̹���
    private TextMeshProUGUI countText;       //������ ���� �ؽ�Ʈ
    private Outline outlineEffect;           // ���� �ܰ��� ȿ��

    private ItemData item;                    // ���� ���Կ� ����ִ� ������
    private int itemCount;                    // �ش� ������ ����

    private void Awake()
    {
        iconImage = GetComponentsInChildren<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public async void SetSlot(ItemData newItem, int NeedCount)     // ���Կ� �� ������ ���� ����
    {
        item = newItem;

        ItemSavaData itemSava = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, item.ID);
        SetColor(newItem);
        if (item != null)
        {
            iconImage[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.ItemIcon, item.ID);
            iconImage[1].enabled = true;        // ������  Ȱ��ȭ

            if (itemSava != null)
            {

                countText.text = $"{itemSava.Value}/{NeedCount}";
                if (itemSava.Value < NeedCount)
                {
                    countText.color = Color.red;
                }
                else
                {
                    countText.color = Color.black;
                }
            }
            else
            {
                countText.text = $"{0}/{NeedCount}";

                countText.color = Color.red;

            }


            //outlineEffect.enabled = false;   // ���� ���� �� �ܰ��� ��Ȱ��ȭ
        }
        else
        {
            ClearSlot();                    // �������� null�̸� ���� ����
        }
    }


    public  void SetLevel(int NeedCount)
    {
        itemCount = GlobalDataTable.Instance.DataCarrier.GetSave().Level;
        iconImage[0].color = Color.yellow;
       
        countText.text = $"{itemCount}/{NeedCount}";
        if (itemCount < NeedCount)
        {
            countText.color = Color.red;
        }
        else
        {
            countText.color = Color.black;
        }
    }

    public async void SetGold(int NeedCount)
    {
        itemCount = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        iconImage[0].color = Color.yellow;
        iconImage[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 1);
        countText.text = $"{itemCount.ToKNumber()}/{NeedCount.ToKNumber()}";
        if (itemCount < NeedCount)
        {
            countText.color = Color.red;
        }
        else
        {
            countText.color = Color.black;
        }
    }

    public void ClearSlot()                 // ���� ����
    {
        item = null;                        // ������ ����
        itemCount = 0;                      // ���� �ʱ�ȭ

        iconImage[1].sprite = null;            // �̹��� ����
        iconImage[1].enabled = false;          // �̹��� ����

    }
    public ItemData GetItem()                         // ���� ������ ���� ��ȯ
    {
        return item;
    }

    public int GetCount()                             // ���� ������ ���� ��ȯ
    {
        return itemCount;
    }

    public async  void OnPointerClick(PointerEventData eventData)    // ���� Ŭ�� �� �˾� ȣ��
    {
        if (item == null) return;                             // �������� ��������� ����

        var popupObj = await UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager�� ShowPopup �Լ� ȣ��

        if (popupObj != null)
        {
           popupObj.ShowItem(item);  // ������ ������ ����
        }
    }


    public void SetColor(ItemData itemData)
    {
        switch (itemData.Grade)
        {
            case ERarity.S:
                iconImage[0].color = Color.yellow;
                break;
            case ERarity.A:
                iconImage[0].color = Color.magenta;
                break;
            case ERarity.B:
                iconImage[0].color = Color.blue;
                break;
            case ERarity.C:
                iconImage[0].color = Color.green;
                break;
            case ERarity.D:
                iconImage[0].color = Color.gray;
                break;
        }

    }
}

