using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattleSlots : MonoBehaviour
{
    GameObject Prefabs;
    List<CharacterSaveData> saveDatas = new List<CharacterSaveData>();

    private void Awake()
    {
        Prefabs = Resources.Load<GameObject>("CharacterSlot");
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
            slot.GetComponent<CharacterBattleSlot>().SetSlot(saveDatas[i].ID);
        }


    }
}
