using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GatchaManager;
public class GatchaExecutor : MonoBehaviour
{
    public void TryDrawOnce()
    {
        string message = GetConfirmMessage(1);
        ShowConfirmPopupAsync<PopupGatchaConfirm>(message, () => Draw(1));
    }

    public void TryDrawTen()
    {
        string message = GetConfirmMessage(10);
        ShowConfirmPopupAsync<PopupGatchaConfirm>(message, () => Draw(10));
    }
    private string GetConfirmMessage(int count) // �̱⸦ Ȯ���ϴ� �޼��� 
    {
        var mgr = GatchaManager.Instance;

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 180;

        return $"{mgr.currentGachaType} {count}ȸ �̱�:\n" +
               $"����  {useTicket}��,  ��������  {needCrystal}���� �Ҹ��մϴ� \n�����ϰڽ��ϱ�?";
    }
    private async void ShowConfirmPopupAsync<T>(string message, System.Action confirmAction) where T : PopupGatchaConfirm
    {
        var popup =  await UIManager.Instance.ShowPopup<T>();
        if (popup != null)
        {
            popup.Setup(message, confirmAction);
        }


    }




    private List<Character> results = new();

    private async void Draw(int count)
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;
        results.Clear();

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 180;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("ũ����Ż�� �����մϴ�!");
            await UIManager.Instance.ShowPopup<PopupCurrencyLack>();
            return;
        }

        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;

        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        QuestManager.Instance.QuestTypeValueUP(count, QuestType.Gacha);

        Debug.Log($"[{mgr.currentGachaType}] Gatcha {count}ȸ ����!");
        Debug.Log($"�Һ�� Ƽ��: {useTicket}, ũ����Ż: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            Grade grade;

            if (count == 10 && i == count - 1)
            {
                grade = GetGuaranteedAorSGrade(mgr.gatchaDrawCount);
            }
            else
            {
                grade = GetRandomGrade(mgr.currentGachaType, mgr.gatchaDrawCount);
            }

            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);
            if (pool.Count == 0)
            {
                Debug.LogWarning($"��� {grade} ĳ���� Ǯ�� ��� ����");
                continue;
            }

            Character selected = null;

            if (mgr.currentGachaType == GatchaType.Pickup)
            {
                if (grade == Grade.S)
                {
                    if (mgr.LatestSID != 0 && mgr.LatestSID != mgr.pickupSCharacterID)
                    {
                        selected = pool.Find(c => c.ID == mgr.pickupSCharacterID);
                        Debug.Log($"���� �۵�! ���� S(ID:{mgr.LatestSID}) �� �Ⱦ� S(ID:{mgr.pickupSCharacterID}), �Ⱦ� S ���� ����");
                    }
                    else if (Random.value < 0.5f)
                    {
                        selected = pool.Find(c => c.ID == mgr.pickupSCharacterID);
                        Debug.Log($"50% Ȯ���� �Ⱦ� S ���� �õ� �� {(selected != null ? "����" : "����")}");
                    }
                }

                if (grade == Grade.A && selected == null && Random.value < 0.5f)
                {
                    var candidates = pool.FindAll(c => mgr.pickupACharacterIDs.Contains(c.ID));
                    if (candidates.Count > 0)
                    {
                        selected = candidates[Random.Range(0, candidates.Count)];
                    }
                }
            }

            if (selected == null)
            {
                selected = pool[Random.Range(0, pool.Count)];
            }

            results.Add(selected);
            GatchaHistoryManager.Instance.AddEntry(new GatchaHistoryEntry(
                selected.Name, grade, mgr.currentGachaType, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Debug.Log($"ȹ��: [{grade}] {selected.Name} (ID:{selected.ID})");

            if (grade == Grade.S)
            {
                mgr.LatestSID = selected.ID;
                mgr.SaveLatestSID(selected.ID);
                mgr.gatchaDrawCount = 0;
            }
            else
            {
                mgr.gatchaDrawCount++;
            }

            CharacterDuplicateCheck(selected.ID);
        }

        curManager.Save();
        mgr.SaveData();

        GatchaResultHolder.results = results;

        GatchaResultHolder.session = new GatchaSessionData
        {
            gatchaType = mgr.currentGachaType,
            drawCount = count,
            pickupSId = mgr.pickupSCharacterID,
            pickupAIds = new List<int>(mgr.pickupACharacterIDs)
        };

        UnityEngine.SceneManagement.SceneManager.LoadScene("GatchaResultScene");
    }

    private Grade GetGuaranteedAorSGrade(int drawCount)
    {
        float sRate = 3f;

        if (drawCount >= 60)
            sRate = 15f;
        else if (drawCount >= 50)
            sRate = 8f;

        float rand = Random.Range(0f, 100f);
        if (rand < sRate) return Grade.S;
        else return Grade.A;  // A �Ǵ� S�� ��ȯ��
    }

    public async Task<List<Character>> DrawWithSession(GatchaSessionData session)
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;
        List<Character> results = new();

        int useTicket = Mathf.Min(session.drawCount, curManager.GetCurrency(CurrencyType.Gacha));
        int needCrystal = (session.drawCount - useTicket) * 180;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("ũ����Ż�� �����մϴ�!");
            await UIManager.Instance.ShowPopup<PopupCurrencyLack>();
            return results;
        }

        // ��ȭ ����
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;
        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        for (int i = 0; i < session.drawCount; i++)
        {
            Grade grade;

            // 10��° ���� ó��
            if (session.drawCount == 10 && i == session.drawCount - 1)
            {
                grade = GetGuaranteedAorSGrade(mgr.gatchaDrawCount);
            }
            else
            {
                grade = GetRandomGrade(session.gatchaType, mgr.gatchaDrawCount);
            }

            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);
            if (pool.Count == 0)
            {
                Debug.LogWarning($"��� {grade} ĳ���� Ǯ�� ��� ����");
                continue;
            }

            Character selected = SelectCharacterByPickup(grade, pool, session);
            results.Add(selected);

            GatchaHistoryManager.Instance.AddEntry(new GatchaHistoryEntry(
                selected.Name, grade, mgr.currentGachaType, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Debug.Log($"[DrawMore] ȹ��: [{grade}] {selected.Name} (ID:{selected.ID})");

            // S����̸� LatestSID ��� + ī��Ʈ �ʱ�ȭ
            if (grade == Grade.S)
            {
                mgr.LatestSID = selected.ID;
                mgr.SaveLatestSID(selected.ID);
                mgr.gatchaDrawCount = 0;
            }
            else
            {
                mgr.gatchaDrawCount++;
            }

            CharacterDuplicateCheck(selected.ID);
        }

        curManager.Save();
        mgr.SaveData();

        var currencyText = FindObjectOfType<GatchaCurrenyText>();
        if (currencyText != null)
        {
            currencyText.SettingCurrency();
        }

        return results;
    }


    private Character SelectCharacterByPickup(Grade grade, List<Character> pool, GatchaSessionData session)
    {
        var mgr = GatchaManager.Instance;

        if (session.gatchaType == GatchaType.Pickup)
        {
            if (grade == Grade.S)
            {
                if (mgr.LatestSID != 0 && mgr.LatestSID != session.pickupSId)
                {
                    var forcedPickupS = pool.Find(c => c.ID == session.pickupSId);
                    if (forcedPickupS != null) return forcedPickupS;
                }
                else if (Random.value < 0.5f)
                {
                    var pickupS = pool.Find(c => c.ID == session.pickupSId);
                    if (pickupS != null) return pickupS;
                }
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

    public void CharacterDuplicateCheck(int Id) //ĳ���Ͱ� �ߺ����� ������ �ִ����� ���� �޼���
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
                IsEquiped = true,
                Level = 1,
                Recognition = 0,
                Necessity = 0,
                Love = 0,
             };
            CharacterManager.Instance.SaveCharacterSaveData(chracterSave);
        }
    }

    private Grade GetRandomGrade(GatchaType type, int drawCount) //�õ� Ƚ���� ���� Ȯ�� ��ȭ
    {
        // �ϵ� õ��: 70ȸ���� ������ S
        if (drawCount > 0 && drawCount % 69 == 0)  //69���� ���� ����  - �� ���� 70�� õ������ S Ȯ�� 
        {
            Debug.Log("70ȸ�� ���� �ߵ�! ������ S���");
            GatchaManager.Instance.gatchaDrawCount = 0;
            return Grade.S;
        }

        // �⺻ Ȯ��
        float sRate = 3f;
        float aRate = 17f;

        // ���� Ƚ���� ���� ���� (����)
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
