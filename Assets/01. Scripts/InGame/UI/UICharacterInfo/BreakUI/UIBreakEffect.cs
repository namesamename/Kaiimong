using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBreakEffect : MonoBehaviour, ISetPOPUp
{

    Image[] Images;
    TextMeshProUGUI[] texts;


    private void Awake()
    {
        Images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void Initialize()
    {
        //0���� �� ���� �̹���
        for (int i = 1; i < Images.Length; i++) 
        {
            //Images[i].sprite = ���»���� �߰�;
        }
        for(int i = 0; i < texts.Length; ++i) 
        {
            texts[i].text = i.ToString();
            texts[i].color = Color.black;
        }
        for (int i = texts.Length-1; i >= GlobalDataTable.Instance.DataCarrier.GetSave().Necessity; i--) 
        {
            texts[i].color = Color.gray;
        }


    }

  
}
