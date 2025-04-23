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
    private string GetConfirmMessage(int count) // 뽑기를 확인하는 메세지 
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

    private void Draw(int count)  //뽑기 창에서 뽑기를 실행하는 메서드 , 중요
    {
        var mgr = GatchaManager.Instance;  //가챠 매니저 가져오기
        var curManager = CurrencyManager.Instance; //재화 매니져 가져오기
        results.Clear(); // 결과 리스트 초기화

        int useTicket = Mathf.Min(count, mgr.ticket);
        int needCrystal = (count - useTicket) * 180;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("크리스탈이 부족합니다!");
            return;
        }

        // 재화 차감
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;

        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        Debug.Log($"[{mgr.currentGachaType}] Gatcha {count}회 성공!");
        Debug.Log($"소비된 티켓: {useTicket}, 크리스탈: {needCrystal}");

        for (int i = 0; i < count; i++)
        {
            Grade grade = GetRandomGrade(mgr.currentGachaType, mgr.gatchaDrawCount); //등급 추출

            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade); //해당 등급 풀 가져오기
            if (pool.Count == 0)
            {
                Debug.LogWarning($"등급 {grade} 캐릭터 풀이 비어 있음");
                continue;
            }

            Character selected = null; // 최종 선택 캐릭터

            // 픽업 처리 (S or A, 50% 확률)
            if (mgr.currentGachaType == GatchaType.Pickup)
            {
                if (grade == Grade.S&& Random.value < 0.5f) // 50퍼센트로  S일시 지정된 픽업캐릭터 획득하기
                {
                    selected = pool.Find(c => c.ID == mgr.pickupSCharacterID);  //픽업 S
                }

                if (grade == Grade.A && selected == null && Random.value < 0.5f) //A 픽업 되는 부분
                {
                    var candidates = pool.FindAll(c => mgr.pickupACharacterIDs.Contains(c.ID));  // candidates A픽업 리스트 풀을 가짐
                    if (candidates.Count > 0)
                    {
                        selected = candidates[Random.Range(0, candidates.Count)]; //풀 중 하나 선택
                    }
                }
            }

            // 일반 캐릭터 처리
            if (selected == null)
            {
                selected = pool[Random.Range(0, pool.Count)];  // 일반 S/A/B 캐릭터 선택
            }

            results.Add(selected);

            Debug.Log($"획득: [{grade}] {selected.Name} (ID:{selected.ID})");

            // S등급이면 무조건 카운트 초기화, 아니면 증가
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

        // 재화 및 가챠 횟수 저장
        curManager.Save();
        PlayerPrefs.SetInt("GatchaDrawCount", mgr.gatchaDrawCount);
        PlayerPrefs.Save();

        // 결과 전달 및 씬 이동
        GatchaResultHolder.results = results;  //홀더의 결과에 담아두기

        GatchaResultHolder.session = new GatchaSessionData //최근 뽑기 형식 기억
        {
            gatchaType = mgr.currentGachaType,
            drawCount = count,
            pickupSId = mgr.pickupSCharacterID,
            pickupAIds = new List<int>(mgr.pickupACharacterIDs)
        };

        UnityEngine.SceneManagement.SceneManager.LoadScene("GatchaResultScene");
    }

    public List<Character> DrawWithSession(GatchaSessionData session) //결과창에서 진행하는 뽑기 
    {
        var mgr = GatchaManager.Instance;
        var curManager = CurrencyManager.Instance;
        List<Character> results = new();

        int useTicket = Mathf.Min(session.drawCount,mgr.ticket);
        int needCrystal = (session.drawCount - useTicket) * 180;

        if (mgr.crystal < needCrystal)
        {
            Debug.LogWarning("크리스탈이 부족합니다!");
            return results;
        }

        // 재화 차감
        mgr.ticket -= useTicket;
        mgr.crystal -= needCrystal;
        curManager.SetCurrency(CurrencyType.Gacha, -useTicket);
        curManager.SetCurrency(CurrencyType.Dia, -needCrystal);

        for (int i = 0; i < session.drawCount; i++)
        {
            Grade grade = GetRandomGrade(session.gatchaType, mgr.gatchaDrawCount);

            // S등급 뽑혔으면 카운터 초기화
            if (grade == Grade.S)
            {
                mgr.gatchaDrawCount = 0;
            }
            else
            {
                mgr.gatchaDrawCount++; // S가 아닐 때만 증가
            }
            var pool = GatchaCharacterPool.Instance.GetCharactersByGrade(grade);
            Character selected = SelectCharacterByPickup(grade, pool, session);
            results.Add(selected);

            Debug.Log($"[DrawMore] 획득: [{grade}] {selected.Name}");
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
            if (grade == Grade.S && Random.value < 0.5f)//일단 결과창에서 뽑기를 할때 픽업이면 무조건 S 픽업 지정 캐릭터 나오게
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

    public void CharacterDuplicateCheck(int Id) //캐릭터가 중복으로 가지고 있는지를 보는 메서드
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

    private Grade GetRandomGrade(GatchaType type, int drawCount) //시도 횟수에 따른 확률 변화
    {
        // 하드 천장: 70회차는 무조건 S
        if (drawCount > 0 && drawCount % 69 == 0)  //69번이 뽑힌 상태  - 그 이후 70번 천장으로 S 확정 
        {
            Debug.Log("70회차 보정 발동! 무조건 S등급");
            GatchaManager.Instance.gatchaDrawCount = 0;
            return Grade.S;
        }

        // 기본 확률
        float sRate = 3f;
        float aRate = 17f;

        // 누적 횟수에 따른 보정 (공통)
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
