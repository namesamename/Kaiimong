using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


public class DamagePOPUP : UIPopup
{
    TextMeshPro DamageText;


    private void Awake()
    {
        DamageText =GetComponentInChildren<TextMeshPro>();
        //DamageRectTransform = GetComponent<RectTransform>();
    }

    public void SetPOPUP(float Damage, bool IsCri, CharacterCarrier character)
    {
        DamageText.text = Damage.ToString();
        DamageText.color = IsCri ? Color.red : Color.black;

        float ran = Random.Range(-1f, 1f);

        Vector3 pos = new Vector3(character.transform.position.x+ ran, character.transform.position.y + 2f+ ran, character.transform.position.z);

        transform.position = pos;
        StartCoroutine(SetUP(ran));
    }


    public IEnumerator SetUP(float ran)
    {
        transform.DOMoveY(ran + 2.5f, 2.7f);
        //transform.DOLocalMoveY(3f, 2.7f);
     
        yield return new WaitForSeconds(3f);
        DOTween.Kill(transform);
        Destroy();
    }

}
