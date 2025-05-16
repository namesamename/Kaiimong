using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIEffect : MonoBehaviour
{
    [SerializeField] private RectTransform[] rectTransforms;
    [SerializeField] private float posY;

    [SerializeField] private float startYOffset = -200f;      // �Ʒ����� �����ϴ� ������
    [SerializeField] private float bounceHeight = 40f;        // Ƣ������� ����
    [SerializeField] private float bounceUpDuration = 0.2f;
    [SerializeField] private float settleDuration = 0.3f;

    private float[] originalYPositions;

    private void Awake()
    {
        originalYPositions = new float[rectTransforms.Length];
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            originalYPositions[i] = rectTransforms[i].anchoredPosition.y;
        }
    }

    private void Start()
    {
        StageManager.Instance.BattleSystem.BattleUI.CharacterUI.ResetIconY += ResetPosition;
    }

    public void BounceEffect()
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            RectTransform rt = rectTransforms[i];

            float startY = posY + startYOffset;
            float bounceY = posY + bounceHeight;
            float finalY = posY;

            // ���� anchoredPosition�� ��������, Y�� ����
            Vector2 startPos = new Vector2(rt.anchoredPosition.x, startY);
            Vector2 bouncePos = new Vector2(rt.anchoredPosition.x, bounceY);
            Vector2 finalPos = new Vector2(rt.anchoredPosition.x, finalY);

            rt.anchoredPosition = startPos;

            Sequence seq = DOTween.Sequence();
            seq.Append(rt.DOAnchorPosY(bounceY, bounceUpDuration).SetEase(Ease.OutQuad));
            seq.Append(rt.DOAnchorPosY(finalY, settleDuration).SetEase(Ease.OutBack));
        }
    }

    public void ResetPosition()
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            RectTransform rt = rectTransforms[i];
            float originalY = originalYPositions[i];
            rt.DOAnchorPosY(originalY, 1f);
        }
    }
}
