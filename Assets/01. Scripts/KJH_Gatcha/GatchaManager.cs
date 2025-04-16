using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GatchaManager;

public class GatchaManager : MonoBehaviour //화면에서 가챠 타입을 구분해 주는 매니저 
{
    public int crystal;
    public int ticket;

    public enum GatchaType
    {
        Pickup, //픽업뽑기를 위한 타입
        Standard // 상시뽑기를 위한 타입
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
