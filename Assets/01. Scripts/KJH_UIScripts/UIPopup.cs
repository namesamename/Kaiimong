using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour //불러올 프리펩에 상속시킬 것들
{
    private void Awake()
    {
        PlayShowAnimation();
    }
    protected void PlayShowAnimation() //프리펩이 나올때 효과
    {
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
    }

    public virtual void Hide() //팝업을 제거하는 기능
    {
        Destroy(gameObject);
    }
}


