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


        foreach (GameObject slot in slots)
        {
            slot.GetComponent<CharacterBattleSlot>().TurnDown();
        }

      
        foreach (int id in Party.GetCharacterIDList())
        {
            SlotList[Index].GetComponent<CharacterBattleSlot>().SetComponent();
            SlotList[Index].GetComponent<CharacterBattleSlot>().SetSlot(id);

            // ID로 직접 접근하지 말고, 해당 ID를 가진 슬롯을 찾아서 활성화
            for (int i = 0; i < slots.Count; i++)
            {
                CharacterBattleSlot slotComponent = slots[i].GetComponent<CharacterBattleSlot>();
                if (slotComponent.GetCharacterID() == id)
                {
                    slotComponent.Turnon(Index);
                    break; 
                }
            }
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
