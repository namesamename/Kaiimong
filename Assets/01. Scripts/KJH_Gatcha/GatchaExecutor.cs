using UnityEngine;



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

        Debug.Log($" {mgr.currentGachaType} Gatcha {count}회 성공!");
        Debug.Log($" 소비된 티켓: {useTicket}, 크리스탈: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            Grade grade = GetRandomGrade(); // 확률 기반 등급 선택
            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);

            if (pool.Count == 0)
            {
                Debug.LogWarning($"등급 {grade} 캐릭터가 없습니다!");
                continue;
            }

            Character selected = pool[Random.Range(0, pool.Count)];
            Debug.Log($" [{grade}] 등급 {selected.Name}획득");
        }

        curManager.Save();
    }
    private Grade GetRandomGrade()
    {
        float rand = Random.Range(0f, 100f);

        if (rand < 55f) return Grade.S;        // 5%
        else if (rand < 80f) return Grade.A;  // 15%
        else return Grade.B;                  // 80%
    }

}
