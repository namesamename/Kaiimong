using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour  //뽑기 결과연출을 보여주는 스크립트
{
    [SerializeField] private Image background;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject Particle;




    public async void Setup(Character character)
    {
        Particle.SetActive(false);

        // 배경 색상 (등급에 따라)
        switch (character.Grade)
        {
            case Grade.S:
                background.color = new Color32(0xFF, 0xE4, 0x00, 0xFF);
                Particle.SetActive(true);

                AnimateSGradeS();
                break;
            case Grade.A:
                background.color = new Color32(0x6C, 0x00, 0xFF, 0xFF);
                break;
            case Grade.B:
                background.color = new Color32(0x00, 0x71, 0xC8, 0xFF);
                break;
        }

        // 스프라이트 로드: ID 1~4만 존재

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

    private void AnimateSGradeS() // S등급이 나왔을 때 동작하는 부분 
    {
        // 크기 조절
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        // 배경 반짝이기
        background.DOFade(1f, 0.2f).From(0.3f).SetLoops(2, LoopType.Yoyo);
    }
}
