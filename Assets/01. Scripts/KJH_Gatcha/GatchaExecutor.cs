using UnityEngine;
using TMPro;
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

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 160;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("need more crystal");
            return;
        }

        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;


        Debug.Log($"{mgr.currentGachaType} Gatcha {count}time sucess!");
        Debug.Log($"¼ÒºñµÈ Æ¼ÄÏ: {useTicket}, Å©¸®½ºÅ»: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            var result = GatchaTable.GetRandomCharacter();
            Debug.Log($" [{result.grade}] {result.name} È¹µæ!");
        }

        // TODO: ½ÇÁ¦ »Ì±â °á°ú ·ÎÁ÷ ¿¬°á
    }
}
