using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterResult : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private Image background;

    public void Setup(Character character) //�������� �̸�,��� ����
    {
        nameText.text = character.Name;
        gradeText.text = character.Grade.ToString();

        switch (character.Grade)
        {
            case Grade.S:
                background.color = new Color(1f, 0.8f, 0.2f); // �ݻ�
                AnimateSGrade(); // �߰� �ִϸ��̼�
                break;

            case Grade.A:
                background.color = new Color(0.6f, 0.8f, 1f); // �Ķ�
                break;

            case Grade.B:
                background.color = new Color(0.85f, 0.85f, 0.85f); // ȸ��
                break;
        }
    }

    private void AnimateSGrade() //Ư�� ����
    {
        // ũ�� ��!
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        // ��� ��¦�̱�
        background.DOFade(1f, 0.2f).From(0.3f).SetLoops(2, LoopType.Yoyo);

        // �ؽ�Ʈ ��鸮��
        gradeText.rectTransform.DOShakePosition(0.5f, 10f, 20, 90);
    }
}
