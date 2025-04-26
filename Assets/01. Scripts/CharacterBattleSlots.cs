using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterBattleSlots : MonoBehaviour
{
    GameObject Prefabs;
    List<CharacterSaveData> saveDatas = new List<CharacterSaveData>();
    List<GameObject> Slots = new List<GameObject>();
    int index = 0;
    private void Awake()
    {
        Prefabs = Resources.Load<GameObject>("CharacterSlot/CharacterSlot");
    }

    private void Start()
    {
        CreatSlot();
    }


    public void CreatSlot()
    {
        List<CharacterSaveData> list = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        foreach (CharacterSaveData data in list) 
        {
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



    public void SlotIdexSet()
    {
        index = 0;

        foreach (GameObject gameObject in Slots)
        {
            if(gameObject.GetComponent<CharacterBattleSlot>().IsSeted)
            {
                gameObject.GetComponent<CharacterBattleSlot>().Turnon(index);
                index++;
            }
            else
            {
                gameObject.GetComponent<CharacterBattleSlot>().TurnDown();
            }
        }




    }
}
