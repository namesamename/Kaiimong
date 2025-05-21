using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBattleSlotSpawner : MonoBehaviour
{
    GameObject Prefabs;
    List<CharacterSaveData> saveDatas = new List<CharacterSaveData>();
    List<GameObject> Slots = new List<GameObject>();
    public List<CharacterBattleSlot> BattleSlots = new List<CharacterBattleSlot>();
 

    private void Awake()
    {
        Prefabs = Resources.Load<GameObject>("CharacterSlot/CharacterSlot");
        CreatSlot();
    }
    public List<GameObject> GetSlots()
    {
        return Slots;
    }
    public void CreatSlot()
    {
        List<CharacterSaveData> list = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        List<Character> list2 = new List<Character>();
        foreach(CharacterSaveData saveData in list) 
        {
            list2.Add(GlobalDataTable.Instance.character.GetCharToID(saveData.ID));
            
        }
        list2 = list2.OrderBy(item => item.Grade).ToList();
        list.Clear();
        foreach (Character saveData in list2)
        {
            list.Add(SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character,saveData.ID));
        }

        if (Slots.Count >= list.Count)
        {
            int index = 0;
            foreach(GameObject gameObject in Slots)
            {
                gameObject.SetActive(true);
                gameObject.GetComponent<CharacterBattleSlot>().SetComponent();
                gameObject.GetComponent<CharacterBattleSlot>().SetSlot(saveDatas[index].ID);
                index++;
            }
        }
        else
        {
            foreach (CharacterSaveData data in list)
            {
                if(data.IsEquiped)
                saveDatas.Add(data);
            }
            for (int i = 0; i < saveDatas.Count; i++)
            {
                GameObject slot = Instantiate(Prefabs, transform);
                Slots.Add(slot);
                slot.GetComponent<CharacterBattleSlot>().SetComponent();
                slot.GetComponent<CharacterBattleSlot>().SetSlot(saveDatas[i].ID);
            }
        }
    }

    public void SelectSlot(CharacterBattleSlot slot)
    {
        if (!slot.IsSeted)
        {
            slot.IsSeted = true;
            BattleSlots.Add(slot);
        }
        else
        {
            slot.IsSeted = false;
            BattleSlots.Remove(slot);
        }
    }


    public void SlotIdexSet()
    {
 
        foreach (GameObject gameObject in Slots)
        {
            gameObject.GetComponent<CharacterBattleSlot>().TurnDown();
        }


        for (int i = 0; i < BattleSlots.Count; i++)
        {
            BattleSlots[i].Turnon(i);
        }
    }
}
