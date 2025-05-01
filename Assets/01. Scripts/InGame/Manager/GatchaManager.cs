using System.Collections.Generic;
using UnityEngine;

public class GatchaManager :Singleton<GatchaManager> //ȭ�鿡�� ��í Ÿ�� ����, �̱� Ƚ�� ���� �Ŵ��� 
{
    public int crystal;
    public int ticket;
    public int gatchaDrawCount = 0;
    public static GatchaManager Instance;
    public GatchaType currentGachaType;  //���� �ֱ��� ��í Ÿ���� ��������
    public int pickupSCharacterID =1;         // S �Ⱦ� ���
    public List<int> pickupACharacterIDs = new() {6,7};

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �ߺ� ������ GatchaManager ����
            return;
        }
        gatchaDrawCount = PlayerPrefs.GetInt("GatchaDrawCount", 0);
    }
    private void Start()
    {
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
