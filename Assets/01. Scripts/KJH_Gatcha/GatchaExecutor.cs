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

    private List<Character> results = new();

    private void Draw(int count)
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;
        mgr.lastDrawCount = count; 
        results.Clear(); // ��� ����Ʈ �ʱ�ȭ

        int useTicket = Mathf.Min(count, curManager.GetCurrency(CurrencyType.Gacha));
        int needCrystal = (count - useTicket) * 160;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("ũ����Ż�� �����մϴ�!");
            return;
        }

        // ��ȭ ����
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;

        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        Debug.Log($"[{mgr.currentGachaType}] Gatcha {count}ȸ ����!");
        Debug.Log($"�Һ�� Ƽ��: {useTicket}, ũ����Ż: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            mgr.gatchaDrawCount++;

            Grade grade = GetRandomGrade(mgr.currentGachaType, mgr.gatchaDrawCount);
            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);

            if (pool.Count == 0)
            {
                Debug.LogWarning($"��� {grade} ĳ���� Ǯ�� ��� ����");
                continue;
            }

            // �Ⱦ� ó�� (S or A, 50% Ȯ��)
            if (mgr.currentGachaType == GatchaType.Pickup)
            {
                if (grade == Grade.S && Random.value < 0.5f)
                {
                    var pickupS = pool.Find(c => c.ID == mgr.pickupSCharacterID);
                    if (pickupS != null)
                    {

                        results.Add(pickupS);
                        Debug.Log($"[PICKUP S] {pickupS.Name} (ID:{pickupS.ID}) ȹ��!");

                        //CharacterDuplicateCheck(pickupS.ID);

                        continue;
                    }
                }

                if (grade == Grade.A && Random.value < 0.5f)
                {
                    var candidates = pool.FindAll(c => mgr.pickupACharacterIDs.Contains(c.ID));
                    if (candidates.Count > 0)
                    {
                        var pickupA = candidates[Random.Range(0, candidates.Count)];

                        results.Add(pickupA);
                        Debug.Log($"[PICKUP A] {pickupA.Name} (ID:{pickupA.ID}) ȹ��!");
                        //CharacterDuplicateCheck(pickupA.ID);

                        continue;
                    }
                }
            }

            // �Ϲ� ĳ����
            var normal = pool[Random.Range(0, pool.Count)];

            results.Add(normal);
            Debug.Log($"�Ϲ� ȹ��: [{grade}] {normal.Name} (ID:{normal.ID})");

            //CharacterDuplicateCheck(normal.ID);
        }

        // ��ȭ �� ��í Ƚ�� ����
        curManager.Save();
        PlayerPrefs.SetInt("GatchaDrawCount", mgr.gatchaDrawCount);
        PlayerPrefs.Save();

        // ��� ���� �� �� �̵�
        GatchaResultHolder.results = results;

        GatchaResultHolder.session = new GatchaSessionData //�ֱ� �̱� ���� ���
        {
            gatchaType = mgr.currentGachaType,
            drawCount = count,
            pickupSId = mgr.pickupSCharacterID,
            pickupAIds = new List<int>(mgr.pickupACharacterIDs)
        };


        UnityEngine.SceneManagement.SceneManager.LoadScene("GatchaResultScene");
    }
    public List<Character> DrawWithSession(GatchaSessionData session)
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;
        List<Character> results = new();

        int useTicket = Mathf.Min(session.drawCount, curManager.GetCurrency(CurrencyType.Gacha));
        int needCrystal = (session.drawCount - useTicket) * 160;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("ũ����Ż�� �����մϴ�!");
            return results;
        }

        // ��ȭ ����
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;
        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        for (int i = 0; i < session.drawCount; i++)
        {
            mgr.gatchaDrawCount++;

            Grade grade = GetRandomGrade(session.gatchaType, mgr.gatchaDrawCount);
            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);

            Character selected = SelectCharacterByPickup(grade, pool, session);
            results.Add(selected);
            Debug.Log($"[DrawMore] ȹ��: [{grade}] {selected.Name}");
        }

        curManager.Save();
        PlayerPrefs.SetInt("GatchaDrawCount", mgr.gatchaDrawCount);
        PlayerPrefs.Save();

        return results;
    }

    private Character SelectCharacterByPickup(Grade grade, List<Character> pool, GatchaSessionData session)
    {
        if (session.gatchaType == GatchaType.Pickup)
        {
            if (grade == Grade.S && Random.value < 0.5f)
            {
                var pickupS = pool.Find(c => c.ID == session.pickupSId);
                if (pickupS != null) return pickupS;
            }

            if (grade == Grade.A && Random.value < 0.5f)
            {
                var candidates = pool.FindAll(c => session.pickupAIds.Contains(c.ID));
                if (candidates.Count > 0)
                {
                    return candidates[Random.Range(0, candidates.Count)];
                }
            }
        }

        return pool[Random.Range(0, pool.Count)];
    }

    public void CharacterDuplicateCheck(int Id)
    {
        var Character = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, Id);

        if (Character != null)
        {

            if (Character.IsEquiped)
            {
                ItemManager.Instance.SetitemCount(GlobalDataTable.Instance.character.GetCharToID(Character.ID).CharacterItem, 1);
            }
            else
            {
                Character.IsEquiped = true;
            }
            SaveDataBase.Instance.SaveSingleData(Character);
        }
        else
        {
            CharacterSaveData chracterSave = new CharacterSaveData()
            {
                ID = Id,
                Savetype = SaveType.Character,
                CumExp = 0,
                IsEquiped = true,
                Level = 1,
                Recognition = 0,
                Necessity = 0,
                Love = 0,
             };
            SaveDataBase.Instance.SaveSingleData(chracterSave);
        }
    }

    private Grade GetRandomGrade(GatchaType type, int drawCount)
    {
        if (drawCount > 0 && drawCount % 70 == 0)
        {
            Debug.Log("70ȸ�� ���� �ߵ�! ������ S���");
            return Grade.S;
        }

        float sRate = 3f;
        float aRate = 17f;

        if (drawCount >= 60)
        {
            sRate = 15f;
        }
        else if (drawCount >= 50)
        {
            sRate = 8f;
        }

        float rand = Random.Range(0f, 100f);
        if (rand < sRate) return Grade.S;
        else if (rand < sRate + aRate) return Grade.A;
        else return Grade.B;
    }
}
