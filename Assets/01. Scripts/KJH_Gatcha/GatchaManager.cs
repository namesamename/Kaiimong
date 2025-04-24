using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GatchaManager;

public class GatchaManager :Singleton<GatchaManager> //화면에서 가챠 타입 구분, 뽑기 횟수 저장 매니저 
{
    public int crystal;
    public int ticket;
    public int gatchaDrawCount = 0;



    public enum GatchaType
    {
        Pickup, //픽업뽑기를 위한 타입
        Standard // 상시뽑기를 위한 타입
    }
    public static GatchaManager Instance;
    public GatchaType currentGachaType;  //가장 최근의 가챠 타입을 가져오기
    public int pickupSCharacterID =1;         // S 픽업 대상
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
            Destroy(gameObject); // 중복 생성된 GatchaManager 제거
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
    public void SetGachaType(GatchaType type) //기본 타입을 픽업으로 설정
    {
        currentGachaType = type;
        Debug.Log($"GachaType 변경됨: {type}");
    }
    public void Setting()//티켓,보석 재화를 
    {
        ticket = CurrencyManager.Instance.GetCurrency(CurrencyType.Gacha);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
    }
}
