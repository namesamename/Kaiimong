using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GatchaManager;

public class GatchaManager : MonoBehaviour //ȭ�鿡�� ��í Ÿ���� ������ �ִ� �Ŵ��� 
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

        SetGachaType(GatchaType.Pickup);
        Setting();
    }
    public void SetGachaType(GatchaType type) //�⺻ Ÿ���� �Ⱦ����� ����
    {
        currentGachaType = type;
        Debug.Log($"GachaType �����: {type}");
    }
    public void Setting()//Ƽ��,���� ��ȭ�� 
    {
        ticket = CurrencyManager.Instance.GetCurrency(CurrencyType.Gacha);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
    }
}
