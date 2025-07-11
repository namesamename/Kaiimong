using System.Collections.Generic;
using UnityEngine;

public class GatchaManager : Singleton<GatchaManager>
{
    public int crystal;                     // 크리스탈 보유량
    public int ticket;                      // 티켓 보유량
    public int gatchaDrawCount = 0;         // 가챠 누적 횟수 (보정용)
    public static GatchaManager Instance;
    public int LatestSID;                   // 가장 최근에 뽑힌 S등급 캐릭터 ID
    public GatchaType currentGachaType;     // 현재 뽑기 종류 (Pickup, Standard 등)
    public int pickupSCharacterID = 1;      // 픽업 S등급 캐릭터 ID
    public List<int> pickupACharacterIDs = new() { 6, 7 }; // 픽업 A등급 캐릭터 ID 리스트

    private GatchaSaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 필요 시 주석 해제
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        LoadData();
    }

    private void Start()
    {
        SetGachaType(GatchaType.Pickup); // 기본 가챠 타입은 픽업
        Setting();                       // 티켓 및 크리스탈 정보 불러오기
    }


    private void LoadData()
    {
        saveData = SaveGatchaSystem.Load();
        gatchaDrawCount = saveData.GatchaDrawCount;
        LatestSID = saveData.LatestSID;
    }

    public void SaveData()
    {
        saveData.GatchaDrawCount = gatchaDrawCount;
        saveData.LatestSID = LatestSID;
        SaveGatchaSystem.Save(saveData);
    }
    public void SaveLatestSID(int id)
    {
        LatestSID = id;
        SaveData(); // 저장 동기화
    }

    public void SetGachaType(GatchaType type)
    {
        currentGachaType = type;
        Debug.Log($"GachaType 변경됨: {type}");
    }

    public void Setting()
    {
        ticket = CurrencyManager.Instance.GetCurrency(CurrencyType.Gacha);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
    }
}
