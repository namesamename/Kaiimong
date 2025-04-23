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
    private string GetConfirmMessage(int count) // �̱⸦ Ȯ���ϴ� �޼��� 
    {
        var mgr = GatchaManager.Instance;

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 180;

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

    private void Draw(int count)  //�̱� â���� �̱⸦ �����ϴ� �޼��� , �߿�
    {
        var mgr = GatchaManager.Instance;  //��í �Ŵ��� ��������
        var curManager = CurrencyManager.Instance; //��ȭ �Ŵ��� ��������
        results.Clear(); // ��� ����Ʈ �ʱ�ȭ

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 180;

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
            Grade grade = GetRandomGrade(mgr.currentGachaType, mgr.gatchaDrawCount); //��� ����

            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade); //�ش� ��� Ǯ ��������
            if (pool.Count == 0)
            {
                Debug.LogWarning($"��� {grade} ĳ���� Ǯ�� ��� ����");
                continue;
            }

            Character selected = null; // ���� ���� ĳ����

            // �Ⱦ� ó�� (S or A, 50% Ȯ��)
            if (mgr.currentGachaType == GatchaType.Pickup)
            {
                if (grade == Grade.S&& Random.value < 0.5f) // 50�ۼ�Ʈ��  S�Ͻ� ������ �Ⱦ�ĳ���� ȹ���ϱ�
                {
                    selected = pool.Find(c => c.ID == mgr.pickupSCharacterID);  //�Ⱦ� S
                }

                if (grade == Grade.A && selected == null && Random.value < 0.5f) //A �Ⱦ� �Ǵ� �κ�
                {
                    var candidates = pool.FindAll(c => mgr.pickupACharacterIDs.Contains(c.ID));  // candidates A�Ⱦ� ����Ʈ Ǯ�� ����
                    if (candidates.Count > 0)
                    {
                        selected = candidates[Random.Range(0, candidates.Count)]; //Ǯ �� �ϳ� ����
                    }
                }
            }

            // �Ϲ� ĳ���� ó��
            if (selected == null)
            {
                selected = pool[Random.Range(0, pool.Count)];  // �Ϲ� S/A/B ĳ���� ����
            }

            results.Add(selected);

            Debug.Log($"ȹ��: [{grade}] {selected.Name} (ID:{selected.ID})");

            // S����̸� ������ ī��Ʈ �ʱ�ȭ, �ƴϸ� ����
            if (grade == Grade.S)
            {
                mgr.gatchaDrawCount = 0;
            }
            else
            {
                mgr.gatchaDrawCount++;
            }

            //CharacterDuplicateCheck(selected.ID);
        }

        // ��ȭ �� ��í Ƚ�� ����
        curManager.Save();
        PlayerPrefs.SetInt("GatchaDrawCount", mgr.gatchaDrawCount);
        PlayerPrefs.Save();

        // ��� ���� �� �� �̵�
        GatchaResultHolder.results = results;  //Ȧ���� ����� ��Ƶα�

        GatchaResultHolder.session = new GatchaSessionData //�ֱ� �̱� ���� ���
        {
            gatchaType = mgr.currentGachaType,
            drawCount = count,
            pickupSId = mgr.pickupSCharacterID,
            pickupAIds = new List<int>(mgr.pickupACharacterIDs)
        };

        UnityEngine.SceneManagement.SceneManager.LoadScene("GatchaResultScene");
    }

    public List<Character> DrawWithSession(GatchaSessionData session) //���â���� �����ϴ� �̱� 
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;
        List<Character> results = new();

        int useTicket = Mathf.Min(session.drawCount,mgr.ticket);
        int needCrystal = (session.drawCount - useTicket) * 180;

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
            Grade grade = GetRandomGrade(session.gatchaType, mgr.gatchaDrawCount);

            // S��� �������� ī���� �ʱ�ȭ
            if (grade == Grade.S)
            {
                mgr.gatchaDrawCount = 0;
            }
            else
            {
                mgr.gatchaDrawCount++; // S�� �ƴ� ���� ����
            }
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
            if (grade == Grade.S && Random.value < 0.5f)//�ϴ� ���â���� �̱⸦ �Ҷ� �Ⱦ��̸� ������ S �Ⱦ� ���� ĳ���� ������
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
            SaveDataBase.Instance.SaveSingleData(chracterSave);
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
