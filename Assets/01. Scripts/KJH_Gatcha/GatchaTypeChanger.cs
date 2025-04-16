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
        UpdateBannerImage(_gatchaManager.currentGachaType); //��í �Ŵ������� Ÿ���� �޾� ����� 
    }

    public void GatchaTypeChangeByIndex(int index)
    {
        GatchaType type = (GatchaType)index; // 0: Pickup, 1: Standard
        _gatchaManager.SetGachaType(type);
        UpdateBannerImage(type); // ��ư ���� ������ �� ����
    }

    private void UpdateBannerImage(GatchaType type)
    {
        switch (type)
        {   //���̽��� ���.�̹����� ���� ��ü�� �� �ֵ��� �����ϱ�
            case GatchaType.Pickup:
                Banner.color = Color.red;
                Debug.Log("�Ⱦ� ��� �̹��� �Դϴ�");
                break;
            case GatchaType.Standard:
                Banner.color = Color.blue;
                Debug.Log("��� ��� �̹��� �Դϴ�");

                break;
            default:
                Banner.color = Color.gray;
                break;
        }
    }
}