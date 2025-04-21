using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GatchaManager;

public class GatchaManager : MonoBehaviour //ȭ�鿡�� ��í Ÿ���� ������ �ִ� �Ŵ��� 
{
    public int crystal;
    public int ticket;
    public int gatchaDrawCount = 0;
    public int lastDrawCount = 0;  //�ֱ� �õ��� ��í Ƚ�� 



    public enum GatchaType
    {
        Pickup, //�Ⱦ��̱⸦ ���� Ÿ��
        Standard // ��û̱⸦ ���� Ÿ��
    }
    public static GatchaManager Instance;

    public GatchaType currentGachaType;  //���� �ֱ��� ��í Ÿ���� ��������

    public int pickupSCharacterID =1;         // S �Ⱦ� ���
    public List<int> pickupACharacterIDs = new() {6,7};

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetGachaType(GatchaType.Pickup);
        Setting();
    }
    private void Start()
    {
        gatchaDrawCount = PlayerPrefs.GetInt("GatchaDrawCount", 0);
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
