using UnityEngine;
using UnityEngine.UI;

public class PickUpUIBannerChange : MonoBehaviour
{
    [SerializeField] private GameObject Banner;     // 대상 배너 오브젝트
    [SerializeField] private Color targetColor;     // 인스펙터에서 지정할 색상

    public void ShowPickUpBanner()
    {
        // Banner 오브젝트에서 Image 컴포넌트 찾기
        Image image = Banner.GetComponent<Image>();

        if (image != null)
        {
            image.color = targetColor;  // 색상 변경!
        }

    }
}
