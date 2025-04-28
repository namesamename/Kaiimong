using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GatchaManager;

public class GatchaTypeChanger : MonoBehaviour
{
    private GatchaManager _gatchaManager;

    [SerializeField] private Image Banner;
    [SerializeField] private GameObject PickUps;

    private Dictionary<Transform, Vector3> originalPositions = new();

    private void Start()
    {
        _gatchaManager = GatchaManager.Instance;
        SaveOriginalPositions();
        UpdateBannerImage(_gatchaManager.currentGachaType);
    }

    private void SaveOriginalPositions()
    {
        foreach (Transform child in PickUps.transform)
        {
            originalPositions[child] = child.localPosition;
        }
    }

    public void GatchaTypeChangeByIndex(int index)
    {
        GatchaType type = (GatchaType)index;
        _gatchaManager.SetGachaType(type);
        UpdateBannerImage(type);
    }

    private void UpdateBannerImage(GatchaType type)
    {
        switch (type)
        {
            case GatchaType.Pickup:
                Banner.color = Color.red;
                Debug.Log("�Ⱦ� ��� �̹��� �Դϴ�");
                PickUps.SetActive(true);

                PlayPickUpAnimation();
                break;

            case GatchaType.Standard:
                Banner.color = Color.blue;
                Debug.Log("��� ��� �̹��� �Դϴ�");
                PickUps.SetActive(false);
                break;

            default:
                Banner.color = Color.gray;
                PickUps.SetActive(false);
                break;
        }
    }

    private void PlayPickUpAnimation()
    {
        foreach (Transform child in PickUps.transform)
        {
            if (originalPositions.ContainsKey(child))
            {
                // 1. ���� ��ġ�� ����
                child.localPosition = originalPositions[child] + new Vector3(-100f, 0f, 0f);

                // 2. �ε巴�� ���ڸ��� �̵�
                child.DOLocalMoveX(originalPositions[child].x, 0.5f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(0.1f * child.GetSiblingIndex());
            }
        }
    }
}
