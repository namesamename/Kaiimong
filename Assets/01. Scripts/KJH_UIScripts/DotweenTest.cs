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

        // 2. 위치 & 크기 초기화 (초기 상태 = 숨겨진 상태)
        RectTransform rect = newPopup.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -1000f);  // 아래에서 등장
        rect.localScale = Vector3.zero; // 작게 시작

        // 3. DOTween 연출 적용 (위로 올라오면서 커짐)
        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPos(Vector2.zero,1f).SetEase(Ease.OutCubic));
        seq.Join(rect.DOScale(Vector3.one, 1).SetEase(Ease.OutBack));
    }

}
