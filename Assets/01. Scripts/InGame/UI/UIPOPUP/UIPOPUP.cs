using DG.Tweening;
using UnityEngine;

public class UIPOPUP : MonoBehaviour //불러올 프리펩에 상속시킬 것들
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

    public void Hide()
    {
        gameObject.SetActive(false);                               // 팝업 비활성화
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}


