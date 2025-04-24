using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GatchaManager;

public class GatchaManager :Singleton<GatchaManager> //ȭ�鿡�� ��í Ÿ�� ����, �̱� Ƚ�� ���� �Ŵ��� 
{
    public int crystal;
    public int ticket;
    public int gatchaDrawCount = 0;



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
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �ߺ� ������ GatchaManager ����
            return;
        }

        //SetGachaType(GatchaType.Pickup);
        //Setting();
    }
    private void Start()
    {
        SetGachaType(GatchaType.Pickup);
        Setting();
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
