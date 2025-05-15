using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelect : MonoBehaviour
{
    [Header("TabButton")]
    [SerializeField] private Image sliderBannerTab;
    [SerializeField] private Image package;
    [SerializeField] private Image skin;
    [SerializeField] private Image currency;

    [Header("Panel")]
    [SerializeField] private GameObject SlideBannerTab;
    [SerializeField] private GameObject RecommendtPackage;
    // �ʿ��� ��� �߰� ����

    [Header("Alpha")]
    [SerializeField] private float activeAlpha = 1f;
    [SerializeField] private float inactiveAlpha = 0f;

    private void Start()
    {
        ClickToChageBanner(1);
    }
    public void ClickToChageBanner(int tab)
    {
        // �г� Ȱ��ȭ ó��
        switch (tab)
        {
            case 1:
                SlideBannerTab.SetActive(true);
                RecommendtPackage.SetActive(false);
                break;

            case 2:
                SlideBannerTab.SetActive(false);
                RecommendtPackage.SetActive(true);
                break;

                // case 3, 4 �� �ʿ��ϸ� �߰� ����
        }

        // ��ư �̹��� ���İ� ó��
        Image[] images = { sliderBannerTab, package, skin, currency };

        for (int i = 0; i < images.Length; i++)
        {
            Color color = images[i].color;
            color.a = (i + 1 == tab) ? activeAlpha : inactiveAlpha;
            images[i].color = color;
        }
    }
}
