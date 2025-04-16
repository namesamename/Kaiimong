using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour //�ҷ��� �����鿡 ��ӽ�ų �͵�
{
    private void Awake()
    {
        PlayShowAnimation();
    }
    protected void PlayShowAnimation() //�������� ���ö� ȿ��
    {
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
    }

    public virtual void Hide() //�˾��� �����ϴ� ���
    {
        Destroy(gameObject);
    }
}


