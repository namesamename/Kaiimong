using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GatchaManager;

public class GatchaManager : MonoBehaviour
{
    public int crystal;
    public int ticket;

    public enum GatchaType
    {
        Pickup, //�Ⱦ��̱⸦ ���� Ÿ��
        Standard // ��û̱⸦ ���� Ÿ��
    }
    public static GatchaManager Instance;

    public GatchaType currentGachaType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetGachaType(GatchaType.Pickup); //�⺻ Ÿ���� �Ⱦ����� ����
        Setting();
    }

    public void SetGachaType(GatchaType type)
    {
        currentGachaType = type;
        Debug.Log($"GachaType �����: {type}");
    }
    public void Setting()
    {
        ticket = CurrencyManager.Instance.GetCurrency(CurrencyType.Gacha);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
    }
}
