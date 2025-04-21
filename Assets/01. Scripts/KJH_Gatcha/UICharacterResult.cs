using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private Image background;

    public void Setup(Character character) //프리펩의 이름,등급 정렬
    {
        nameText.text = character.Name;
        gradeText.text = character.Grade.ToString();

        switch (character.Grade)
        {
            case Grade.S:
                background.color = new Color(1f, 0.8f, 0.2f); // 금색
                AnimateSGrade(); // 추가 애니메이션
                break;

            case Grade.A:
                background.color = new Color(0.6f, 0.8f, 1f); // 파랑
                break;

            case Grade.B:
                background.color = new Color(0.85f, 0.85f, 0.85f); // 회색
                break;
        }
    }

    private void AnimateSGrade() //특별 연출
    {
        // 크기 팡!
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        // 배경 반짝이기
        background.DOFade(1f, 0.2f).From(0.3f).SetLoops(2, LoopType.Yoyo);

        // 텍스트 흔들리기
        gradeText.rectTransform.DOShakePosition(0.5f, 10f, 20, 90);
    }
}
