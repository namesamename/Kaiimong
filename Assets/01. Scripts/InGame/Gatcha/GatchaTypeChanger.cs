using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GatchaTypeChanger : MonoBehaviour
{
    private GatchaManager _gatchaManager;

    [SerializeField] private Image Banner;
    [SerializeField] private GameObject PickUps;

    [SerializeField] private GameObject PickUpPointer;
    [SerializeField] private GameObject StandardPointer;

    [SerializeField] private Button button;

    private Dictionary<Transform, Vector3> originalPositions = new();

    private async void Start()
    {
        _gatchaManager = GatchaManager.Instance;
        SaveOriginalPositions();
        UpdateBannerImage(_gatchaManager.currentGachaType);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
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

    private async void UpdateBannerImage(GatchaType type)
    {
        Sprite PickUpSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.GatchaBanner, 1);
        Sprite StandardSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.GatchaBanner, 2);

        switch (type)
        {
            case GatchaType.Pickup:
                Banner.sprite = PickUpSprite;
                PickUps.SetActive(true);
                PickUpPointer.SetActive(true);
                StandardPointer.SetActive(false);
                PlayPickUpAnimation();
                UpdatePickUpImages();
                break;

            case GatchaType.Standard:
                Banner.sprite = StandardSprite;
                PickUpPointer.SetActive(false);
                StandardPointer.SetActive(true);
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
                child.localPosition = originalPositions[child] + new Vector3(-100f, 0f, 0f);
                child.DOLocalMoveX(originalPositions[child].x, 0.5f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(0.1f * child.GetSiblingIndex());
            }
        }
    }

    private async void UpdatePickUpImages()
    {
        Transform pickUpsTransform = PickUps.transform;

        if (pickUpsTransform.childCount > 0)
        {
            Transform sPickUp = pickUpsTransform.GetChild(0);
            Image sImage = sPickUp.GetComponent<Image>();

            if (sImage != null)
            {
                int sID = GatchaManager.Instance.pickupSCharacterID;
                Sprite sSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CharacterIcon, sID);
                if (sSprite != null) sImage.sprite = sSprite;
                else Debug.LogWarning($"S 픽업(ID: {sID}) 이미지 로드 실패");
            }
        }

        for (int i = 0; i < GatchaManager.Instance.pickupACharacterIDs.Count; i++)
        {
            int targetIndex = i + 1;
            if (pickUpsTransform.childCount <= targetIndex) break;

            Transform aPickUp = pickUpsTransform.GetChild(targetIndex);
            Image aImage = aPickUp.GetComponent<Image>();

            if (aImage != null)
            {
                int aID = GatchaManager.Instance.pickupACharacterIDs[i];
                Sprite aSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CharacterIcon, aID);
                if (aSprite != null) aImage.sprite = aSprite;
                else Debug.LogWarning($"A 픽업(ID: {aID}) 이미지 로드 실패");
            }
        }
    }
}
