using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour  //뽑기 결과연출을 보여주는 스크립트
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private Image background;

    public void Setup(Character character) //프리펩의 이름,등급 보여주는 부분
    {
        nameText.text = character.Name;
        gradeText.text = character.Grade.ToString();

        switch (character.Grade)
        {
            case Grade.S:
                background.color = new Color(1f, 0.8f, 0.2f); // 금색
                AnimateSGradeS(); // 추가 애니메이션
                break;

            case Grade.A:
                background.color = new Color(0.6f, 0.8f, 1f); // 파랑
                break;

            case Grade.B:
                background.color = new Color(0.85f, 0.85f, 0.85f); // 회색
                break;
        }
    }

    private void AnimateSGradeS() // S등급이 나왔을 때 동작하는 부분 
    {
        // 크기 조절
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        // 배경 반짝이기
        background.DOFade(1f, 0.2f).From(0.3f).SetLoops(2, LoopType.Yoyo);
        // 텍스트 흔들리기
        gradeText.rectTransform.DOShakePosition(0.5f, 10f, 20, 90);
    }
}
