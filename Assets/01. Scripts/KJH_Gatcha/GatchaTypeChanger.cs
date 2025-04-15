using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static GatchaManager;

public class GatchaTypeChanger : MonoBehaviour
{
    private GatchaManager _gatchaManager;

    [SerializeField] private Image Banner;

    void Start()
    {
        _gatchaManager = GatchaManager.Instance;
        UpdateBannerColor(_gatchaManager.currentGachaType); // 초기 색 설정
    }

    public void GatchaTypeChangeByIndex(int index)
    {
        GatchaType type = (GatchaType)index; // 0: Pickup, 1: Standard
        _gatchaManager.SetGachaType(type);
        UpdateBannerColor(type); // 버튼 누를 때마다 색 변경
    }

    private void UpdateBannerColor(GatchaType type)
    {
        switch (type)
        {   //케이스에 배너.이미지를 통해 교체할 수 있도록 수정하기
            case GatchaType.Pickup:
                Banner.color = Color.red;
                Debug.Log("픽업 배너 이미지 입니다");
                break;
            case GatchaType.Standard:
                Banner.color = Color.blue;
                Debug.Log("상시 배너 이미지 입니다");

                break;
            default:
                Banner.color = Color.gray;
                break;
        }
    }
}