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
                Debug.Log("픽업 배너 이미지 입니다");
                PickUps.SetActive(true);

                PlayPickUpAnimation();
                break;

            case GatchaType.Standard:
                Banner.color = Color.blue;
                Debug.Log("상시 배너 이미지 입니다");
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
                // 1. 원래 위치로 리셋
                child.localPosition = originalPositions[child] + new Vector3(-100f, 0f, 0f);

                // 2. 부드럽게 제자리로 이동
                child.DOLocalMoveX(originalPositions[child].x, 0.5f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(0.1f * child.GetSiblingIndex());
            }
        }
    }
}
