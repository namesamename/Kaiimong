using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillBook : MonoBehaviour
{
    public ActiveSkillObject[] ActiveSkillList = new ActiveSkillObject[3];
    public List<PassiveSkillObjcet> passiveSkillObjcets = new List<PassiveSkillObjcet>();

    int SkillGauge;
    int maxSkillGauge;
    // public PassiveSkillObject[] PassiveSkillList = new PassiveSkillObject[3];
    private void Awake()
    {
        ActiveSkillList = GetComponentsInChildren<ActiveSkillObject>();
    }
   

    public void SkillSet(int ID, CharacterType type)
    {
   
        if(type == CharacterType.Friend)
        {
            maxSkillGauge = 7;
            int Count = ID * 3;
            for (int i = 2; i >= 0; i--)
            {
                Debug.Log(Count - i);

                if ((Count - i) % 3 == 0)
                {
                    ActiveSkillList[2].SetSkill(Count - i);
                }
                else if ((Count - i) % 3 == 1)
                {
                    ActiveSkillList[0].SetSkill(Count - i);
                }
                else if ((Count - i) % 3 == 2)
                {
                    ActiveSkillList[1].SetSkill(Count - i);
                }
            }
            CharacterSaveData saveData= SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, ID);
            for (int i = saveData.Recognition; i >= 0; i--)
            {
                //LoadPassiveObject(Count - i);
            }
        }
        else
        {

            ActiveSkillList[0].SetSkill(GlobalDataTable.Instance.character.GetEnemyToID(ID).SkillID);
            ActiveSkillList[1].SetSkill(GlobalDataTable.Instance.character.GetEnemyToID(ID).SecondSkillID);


        }
    }

    //public async void LoadPassiveObject(int ID)
    //{
    //    GameObject Passive = await  AddressableManager.Instance.LoadAsset<GameObject>(AddreassablesType.Passive, ID);
    //    GameObject obj = Instantiate(Passive);
    //    obj.gameObject.transform.SetParent(transform);

    //    passiveSkillObjcets.Add(obj.GetComponent<PassiveSkillObjcet>());
    //    obj.GetComponent<PassiveSkillObjcet>().SetSkill(ID);
    //}

    //public void PassiveSkillOn(List<CharacterCarrier> characters)
    //{
    //    for (int i = 0; i < PassiveSkillList.Length; i++)
    //    {
    //        PassiveSkillList[i].UseSkill(characters);
    //    }
    //}
    public void ActiveSkillUsing(ActiveSkillObject skill, List<CharacterCarrier> characters)
    {
        foreach (PassiveSkillObjcet passiveSkill in passiveSkillObjcets)
        {
            passiveSkill.UseSkill(characters);
        }



        //여러가지 처리
        //마나, 쿨타임, 사용 가능한가 불가능 한가
        if (!IsCoolTime(skill))
            return;
        //if(!IsFullCharage(skill)) 
        //    return;


        skill.UseSkill(characters);
    }


    public void SetSkillGauge(ActiveSkillObject activeSkill , List<CharacterCarrier> characters)
    {
        if(activeSkill.SkillSO.ID % 3 == 0)
        {
            SkillGauge = 0;
        }
        else
        {
            if (characters.Count == 4)
            {
                SkillGauge += 2;
            }
            else
            {
                SkillGauge += 1;
            }
         


            if (SkillGauge > maxSkillGauge)
            {
                SkillGauge = maxSkillGauge;
            }
        }

   
   
    }

    public int GetSkillGauge()
    {
        return SkillGauge;
    }


    public bool IsFullCharage(ActiveSkillObject skill)
    {
        if(skill.SkillSO.ID % 3 == 0)
        {
            if(SkillGauge == maxSkillGauge)
            {
                SkillGauge = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
 

    }

    public bool IsCoolTime(ActiveSkillObject skill)
    {
        if (skill.GetCoolTime() == 0)
        { 
            return true;
        }
        else
        {
            return false;
        }
        
    }


    public void SetCoolTimeDown()
    {
        for (int i = 0; i < ActiveSkillList.Length; i++)
        {
            ActiveSkillList[i].CoolTimeDown();
        }
    }

}
