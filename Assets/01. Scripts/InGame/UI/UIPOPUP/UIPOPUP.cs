using DG.Tweening;
using UnityEngine;

public class UIPOPUP : MonoBehaviour //�ҷ��� �����鿡 ��ӽ�ų �͵�
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

    public void Hide()
    {
        gameObject.SetActive(false);                               // �˾� ��Ȱ��ȭ
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}


