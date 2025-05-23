using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIBattleHealthBar : MonoBehaviour
{

    Slider Slider;
    Image[] UtiGauge;
   RectTransform rectTransform;


    Transform TargetTransform;



    private void Awake()
    {
        Slider = GetComponentInChildren<Slider>();
        UtiGauge= GetComponentsInChildren<Image>();
        rectTransform = GetComponent<RectTransform>();
    }
 
    public void SetUI(CharacterCarrier character, CharacterType type)
    {
        TargetTransform= character.transform;
        character.SetType(type);

        if (type == CharacterType.Friend)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(character.transform.position + Vector3.up * 2f);
            rectTransform.position = screenPos;
            Slider.value = character.stat.healthStat.CurHealth / 1f;
            for (int i = 0; i < character.skillBook.GetSkillGauge(); i++)
            {
                UtiGauge[2 + i].color = Color.yellow;
            }


        }
        else
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(character.transform.position + Vector3.up * 1f);
            rectTransform.position = screenPos;
            Slider.value = character.stat.healthStat.CurHealth / character.stat.healthStat.Value;
            for (int i = 0; i < 7; i++)
            {
                UtiGauge[2 + i].enabled = false;
            }
        }
    }


    public void UpdataHealthBar()
    {
        if(TargetTransform != null)
        {
            if (CharacterType.Friend == TargetTransform.GetComponent<CharacterCarrier>().GetCharacterType())
            {
                Slider.value = TargetTransform.GetComponent<CharacterCarrier>().stat.healthStat.CurHealth / TargetTransform.GetComponent<CharacterCarrier>().stat.healthStat.Value;
                for (int i = 0; i < TargetTransform.GetComponent<CharacterCarrier>().skillBook.GetSkillGauge(); i++)
                {
                    UtiGauge[2 + i].color = Color.yellow;
                }
                for (int i = TargetTransform.GetComponent<CharacterCarrier>().skillBook.GetSkillGauge(); i < UtiGauge.Length - 2; i++)
                {
                    UtiGauge[2 + i].color = Color.white;
                }
            }
            else
            {
                Slider.value = TargetTransform.GetComponent<CharacterCarrier>().stat.healthStat.CurHealth / TargetTransform.GetComponent<CharacterCarrier>().stat.healthStat.Value;
            }
        }
    }


    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }


}
