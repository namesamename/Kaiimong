using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


public class DamagePOPUP : UIPopup
{
    TextMeshProUGUI DamageText;


    private void Awake()
    {
        DamageText =GetComponentInChildren<TextMeshProUGUI>();
        DamageRectTransform = GetComponent<RectTransform>();
    }

    public void SetPOPUP(float Damage, bool IsCri, CharacterCarrier character)
    {
        DamageText.text = Damage.ToString();
        DamageText.color = IsCri ? Color.red : Color.black;

      
        StartCoroutine(SetUP());
    }


    public IEnumerator SetUP()
    {
        transform.DOLocalMoveY(2f, 2f);
        yield return new WaitForSeconds(2f);
        Destroy();
    }

}
