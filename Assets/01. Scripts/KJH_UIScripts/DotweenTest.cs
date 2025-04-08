using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform parentPanel;

    public void CreatePopup()
    {
       
        GameObject newPopup = Instantiate(popupPrefab, parentPanel);

        // 2. ��ġ & ũ�� �ʱ�ȭ (�ʱ� ���� = ������ ����)
        RectTransform rect = newPopup.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -1000f);  // �Ʒ����� ����
        rect.localScale = Vector3.zero; // �۰� ����

        // 3. DOTween ���� ���� (���� �ö���鼭 Ŀ��)
        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPos(Vector2.zero,1f).SetEase(Ease.OutCubic));
        seq.Join(rect.DOScale(Vector3.one, 1).SetEase(Ease.OutBack));
    }

}
