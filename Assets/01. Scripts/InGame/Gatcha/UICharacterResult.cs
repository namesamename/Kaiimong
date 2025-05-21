using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour  //뽑기 결과연출을 보여주는 스크립트
{
[SerializeField] private Image background;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI gradeText;


    public async void Setup(Character character)
    {
        nameText.text = character.Name;
        gradeText.text = character.Grade.ToString();

        // 배경 색상 (등급에 따라)
        switch (character.Grade)
        {
            case Grade.S:
                background.color = Color.yellow;
                AnimateSGradeS();
                break;
            case Grade.A:
                background.color = Color.cyan;
                break;
            case Grade.B:
                background.color = Color.gray;
                break;
        }

        // 스프라이트 로드: ID 1~4만 존재
        if (character.ID >= 1 && character.ID <= 12)
        {
            Sprite loadedSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CharacterIcon, character.ID);

            if (loadedSprite != null)
            {
                characterImage.sprite = loadedSprite;
                characterImage.color = Color.white;
            }
            else
            {
                characterImage.color = Color.clear;
            }
        }
        else
        {
            // ID 범위 밖 → 이미지 비활성화 or 기본 이미지 처리
            characterImage.color = Color.clear;
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
