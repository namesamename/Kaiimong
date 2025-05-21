using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour  //�̱� ��������� �����ִ� ��ũ��Ʈ
{
[SerializeField] private Image background;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI gradeText;


    public async void Setup(Character character)
    {
        nameText.text = character.Name;
        gradeText.text = character.Grade.ToString();

        // ��� ���� (��޿� ����)
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

        // ��������Ʈ �ε�: ID 1~4�� ����
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
            // ID ���� �� �� �̹��� ��Ȱ��ȭ or �⺻ �̹��� ó��
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
        // �ؽ�Ʈ ��鸮��
        gradeText.rectTransform.DOShakePosition(0.5f, 10f, 20, 90);
    }
}
