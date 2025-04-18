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
            Debug.LogWarning(" ũ����Ż�� �����մϴ�!");
            return;
        }

        // ��ȭ ����
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;

        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        Debug.Log($" {mgr.currentGachaType} Gatcha {count}ȸ ����!");
        Debug.Log($" �Һ�� Ƽ��: {useTicket}, ũ����Ż: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            Grade grade = GetRandomGrade(); // Ȯ�� ��� ��� ����
            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);

            if (pool.Count == 0)
            {
                Debug.LogWarning($"��� {grade} ĳ���Ͱ� �����ϴ�!");
                continue;
            }

            Character selected = pool[Random.Range(0, pool.Count)];
            Debug.Log($" [{grade}] ��� {selected.Name}ȹ��");
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
