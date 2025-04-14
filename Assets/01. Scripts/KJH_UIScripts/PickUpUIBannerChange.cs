using UnityEngine;
using UnityEngine.UI;

public class PickUpUIBannerChange : MonoBehaviour
{
    [SerializeField] private GameObject Banner;     // ��� ��� ������Ʈ
    [SerializeField] private Color targetColor;     // �ν����Ϳ��� ������ ����

    public void ShowPickUpBanner()
    {
        // Banner ������Ʈ���� Image ������Ʈ ã��
        Image image = Banner.GetComponent<Image>();

        if (image != null)
        {
            image.color = targetColor;  // ���� ����!
        }

    }
}
