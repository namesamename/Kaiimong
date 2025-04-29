using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBreakEffect : MonoBehaviour, ISetPOPUp
{

    Image[] Images;
    TextMeshProUGUI[] Texts;


    private void Awake()
    {
        Images = GetComponentsInChildren<Image>();
        Texts = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void Initialize()
    {
        //0번은 뒷 보드 이미지
        for (int i = 1; i < Images.Length; i++) 
        {
            //Images[i].sprite = 에셋생기면 추가;
        }
        for(int i = 0; i < Texts.Length; ++i) 
        {
            Texts[i].text = i.ToString();
            Texts[i].color = Color.black;
        }
        for (int i = Texts.Length-1; i >= GlobalDataTable.Instance.DataCarrier.GetSave().Necessity; i--) 
        {
            Texts[i].color = Color.gray;
        }


    }

  
}
