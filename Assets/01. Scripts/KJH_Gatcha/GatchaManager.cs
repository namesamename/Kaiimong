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

        SetGachaType(GatchaType.Pickup); //기본 타입을 픽업으로 설정
        Setting();
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
