using System.Collections.Generic;
using UnityEngine;

public class GatchaManager : Singleton<GatchaManager>
{
    private const string LatestSIDKey = "GatchaLatestSID"; // PlayerPrefs ���� Ű

    public int crystal;                     // ũ����Ż ������
    public int ticket;                      // Ƽ�� ������
    public int gatchaDrawCount = 0;         // ��í ���� Ƚ�� (������)
    public static GatchaManager Instance;

    public int LatestSID;                   // ���� �ֱٿ� ���� S��� ĳ���� ID
    public GatchaType currentGachaType;     // ���� �̱� ���� (Pickup, Standard ��)
    public int pickupSCharacterID = 1;      // �Ⱦ� S��� ĳ���� ID
    public List<int> pickupACharacterIDs = new() { 6, 7 }; // �Ⱦ� A��� ĳ���� ID ����Ʈ

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // �ʿ� �� �ּ� ����
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ����� ���� �ҷ�����
        gatchaDrawCount = PlayerPrefs.GetInt("GatchaDrawCount", 0);
        LatestSID = PlayerPrefs.GetInt(LatestSIDKey, 0); // S��� ID �ҷ����� (������ 0)
    }

    private void Start()
    {
        SetGachaType(GatchaType.Pickup); // �⺻ ��í Ÿ���� �Ⱦ�
        Setting();                       // Ƽ�� �� ũ����Ż ���� �ҷ�����
    }

    public void SaveLatestSID(int id)
    {
        LatestSID = id;
        PlayerPrefs.SetInt(LatestSIDKey, id);
        PlayerPrefs.Save();
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
