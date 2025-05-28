using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
public class DamagePOPUP : UIPOPUP
{
    TextMeshPro damageText;


    private void Awake()
    {
        damageText =GetComponentInChildren<TextMeshPro>();
        //DamageRectTransform = GetComponent<RectTransform>();
    }

    public void SetPOPUP(float Damage, DamageType damage, CharacterCarrier character)
    {
        if(Damage < 0f)
        {
            damageText.text = 1f.ToString();
        }else
        {
            damageText.text = Damage.ToString();
        }

        

        switch (damage)
        {
            case DamageType.CriAndWeek:
                damageText.text = Damage.ToString() + "!";
                damageText.fontStyle = FontStyles.Bold;
                damageText.color = Color.red;
                break;
            case DamageType.Cri:
                damageText.text = Damage.ToString() + "!";
                damageText.color = Color.red;
                break;
            case DamageType.Week:
                damageText.fontStyle = FontStyles.Bold;
                damageText.color = Color.yellow;
                break;
            case DamageType.Basic:
                damageText.color = Color.black;
                break;
        }




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
