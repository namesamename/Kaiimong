using System.Collections.Generic;
using UnityEngine;

public class UIPartyList : MonoBehaviour
{
    DataCarrier Party;
    GameObject Prefabs;
    List<GameObject> SlotList = new List<GameObject>();
    CharacterBattleSlotSpawner battleSlots;
    public int Index = 0;
    private void Awake()
    {
        Prefabs = Resources.Load<GameObject>("CharacterSlot/CharacterSlot");
        battleSlots =FindAnyObjectByType<CharacterBattleSlotSpawner>();
    }
    private void Start()
    {
        Party = GlobalDataTable.Instance.DataCarrier;
        Party.RemoveAll();

        for (int i = 0; i < 4; i++)
        {
            GameObject game = Instantiate(Prefabs, transform);
            game.GetComponent<CharacterBattleSlot>().SetComponent();
            game.GetComponent<CharacterBattleSlot>().IsSelected = true;
            game.GetComponent<CharacterBattleSlot>().SlotClear();
            SlotList.Add(game);
        }
        Partyset();
    }
    public void Partyset()
    {
        Index = 0;
        partyDown();

        List<GameObject> slots = battleSlots.GetSlots();
        
        foreach (int id in Party.GetCharacterIDList())
        {
            SlotList[Index].GetComponent<CharacterBattleSlot>().SetComponent();
            SlotList[Index].GetComponent<CharacterBattleSlot>().SetSlot(id);
            slots[id - 1].GetComponent<CharacterBattleSlot>().Turnon(Index);
            Index++;
        }
    }

    public void partyDown()
    {
        foreach(GameObject game in SlotList) 
        {
            game.GetComponent<CharacterBattleSlot>().SetComponent();
            game.GetComponent<CharacterBattleSlot>().SlotClear();
        }
    }





}
