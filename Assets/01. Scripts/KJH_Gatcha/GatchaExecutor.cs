using System.Collections.Generic;
using UnityEngine;
using static GatchaManager;

public class GatchaExecutor : MonoBehaviour
{
    public void TryDrawOnce()
    {
        string message = GetConfirmMessage(1);
        ShowConfirmPopup<PopupGatchaConfirm>(message, () => Draw(1));
    }

    public void TryDrawTen()
    {
        string message = GetConfirmMessage(10);
        ShowConfirmPopup<PopupGatchaConfirm>(message, () => Draw(10));
    }

    private string GetConfirmMessage(int count)
    {
        var mgr = GatchaManager.Instance;

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 160;

        return $"{mgr.currentGachaType} Gatcha {count}time :\n" +
               $"ticket {useTicket} + crystal {needCrystal} use \nDo it?";
    }

    private void ShowConfirmPopup<T>(string message, System.Action confirmAction) where T : PopupGatchaConfirm
    {
        var popup = UIManager.Instance.ShowPopup<T>();
        if (popup != null)
        {
            popup.Setup(message, confirmAction);
        }
    }

    private void Draw(int count)
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;

        int useTicket = Mathf.Min(count, curManager.GetCurrency(CurrencyType.Gacha));
        int needCrystal = (count - useTicket) * 160;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning(" 크리스탈이 부족합니다!");
            return;
        }

        // 재화 차감
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;

        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        Debug.Log($" [{mgr.currentGachaType}] Gatcha {count}회 성공!");
        Debug.Log($" 소비된 티켓: {useTicket}, 크리스탈: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            mgr.gatchaDrawCount++;

            Grade grade = GetRandomGrade(mgr.currentGachaType, mgr.gatchaDrawCount);
            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);

            if (pool.Count == 0)
            {
                Debug.LogWarning($" 등급 {grade} 캐릭터 풀이 비어 있음");
                continue;
            }

            // 픽업 처리 (S or A, 50% 확률)
            if (mgr.currentGachaType == GatchaType.Pickup)
            {
                if (grade == Grade.S && Random.value < 0.5f)
                {
                    var pickupS = pool.Find(c => c.ID == mgr.pickupSCharacterID);
                    if (pickupS != null)
                    {
                        Debug.Log($" [PICKUP S] {pickupS.Name} (ID:{pickupS.ID}) 획득!");
                        continue;
                    }
                }

                if (grade == Grade.A && Random.value < 0.5f)
                {
                    var candidates = pool.FindAll(c => mgr.pickupACharacterIDs.Contains(c.ID));
                    if (candidates.Count > 0)
                    {
                        var pickupA = candidates[Random.Range(0, candidates.Count)];
                        Debug.Log($" [PICKUP A] {pickupA.Name} (ID:{pickupA.ID}) 획득!");
                        continue;
                    }
                }
            }

            // 일반 캐릭터
            var normal = pool[Random.Range(0, pool.Count)];
            Debug.Log($" 일반 획득: [{grade}] {normal.Name} (ID:{normal.ID})");
        }

        curManager.Save();
    }

    private Grade GetRandomGrade(GatchaType type, int drawCount)
    {
        if (drawCount > 0 && drawCount % 80 == 0)
        {
            Debug.Log("80회차 보정 발동! 무조건 S등급");
            GatchaManager.Instance.gatchaDrawCount = 0;
            return Grade.S;         
        }

        float rand = Random.Range(0f, 100f);

        switch (type)
        {
            case GatchaType.Pickup:
                if (rand < 6f) return Grade.S;
                else if (rand < 24f) return Grade.A;
                else return Grade.B;

            case GatchaType.Standard:
            default:
                if (rand < 3f) return Grade.S;
                else if (rand < 18f) return Grade.A;
                else return Grade.B;
        }
    }
}
