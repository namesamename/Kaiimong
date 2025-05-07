using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillBook : MonoBehaviour
{
    public ActiveSkillObject[] ActiveSkillList = new ActiveSkillObject[3];
    public List<PassiveSkillObjcet> passiveSkillObjcets = new List<PassiveSkillObjcet>();
    // public PassiveSkillObject[] PassiveSkillList = new PassiveSkillObject[3];
    private void Awake()
    {
        ActiveSkillList = GetComponentsInChildren<ActiveSkillObject>();
    }
   

    public void SkillSet(int ID, CharacterType type)
    {
        int Count = ID * 3;
        for (int i = 2; i >= 0; i--) 
        {
            ActiveSkillList[i].SetSkill(Count - i);
        }
        if(type == CharacterType.Friend)
        {
            CharacterSaveData saveData= SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, ID);
            for (int i = saveData.Recognition; i >= 0; i--)
            {
                //LoadPassiveObject(Count - i);
            }
        }
        else
        {

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
        foreach(PassiveSkillObjcet passiveSkill in passiveSkillObjcets)
        {
            passiveSkill.UseSkill(characters);
        }

        //여러가지 처리
        //마나, 쿨타임, 사용 가능한가 불가능 한가
        if (!IsCoolTime(skill))
            return;
        skill.UseSkill(characters);
    }


    //public bool IsFullCharage()
    //{

    //}

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
