using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour  //�̱� ��������� �����ִ� ��ũ��Ʈ
{
    [SerializeField] private Image background;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject Particle;




    public async void Setup(Character character)
    {
        Particle.SetActive(false);

        // ��� ���� (��޿� ����)
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

        // ��������Ʈ �ε�: ID 1~4�� ����

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

    private void AnimateSGradeS() // S����� ������ �� �����ϴ� �κ� 
    {
        // ũ�� ����
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        // ��� ��¦�̱�
        background.DOFade(1f, 0.2f).From(0.3f).SetLoops(2, LoopType.Yoyo);
    }
}
