using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILocationItemValue : MonoBehaviour
{

    Transform Normal;
    Transform Consum;


    Action TryConsum;
    private void Awake()
    {
        Normal = transform.GetChild(0);
        Consum = transform.GetChild(1);
    }


    public void ConsumActionSet(Action action)
    {
        TryConsum += action;
    }
    public void ValueSettimg(EItemType Type, int ID)
    {
        if(Type == EItemType.Consume) 
        {
            Normal.gameObject.SetActive(false);
            Consum.gameObject.SetActive(true);

            TextMeshProUGUI[] text = Consum.GetComponentsInChildren<TextMeshProUGUI>();
            Button button = Consum.GetComponentInChildren<Button>();

            text[0].text = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, ID).Value.ToString();
            text[1].text = "TryConsum";

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => TryConsum());


        }
        else
        {
            Normal.gameObject.SetActive(true);
            Consum.gameObject.SetActive(false);

            TextMeshProUGUI text = Consum.GetComponentInChildren<TextMeshProUGUI>();
            text.text = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, ID).Value.ToString();
        }
      

    }


    

}
