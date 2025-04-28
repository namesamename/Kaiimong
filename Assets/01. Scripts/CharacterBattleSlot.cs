using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class CharacterBattleSlot : MonoBehaviour, IPointerClickHandler
{
    Image[] images;
    Image[] INGI;
    Image[] BreakThrought;

    TextMeshProUGUI LV;
    Character character;
    CharacterSaveData Save;

    public bool IsSeted = false;
    public bool IsSelected = false;

    [SerializeField] GameObject Selected;
    UIPartyList partyList;
    CharacterBattleSlots battleSlots;

    int CharacterID = 0;
    int Index;


    public void SetComponent()
    {
        partyList = FindAnyObjectByType<UIPartyList>();
        battleSlots = GetComponentInParent<CharacterBattleSlots>();
        images = GetComponentsInChildren<Image>();
        LV = GetComponentInChildren<TextMeshProUGUI>();
        INGI = new Image[3] { images[2], images[3], images[4] };
        BreakThrought = new Image[5] { images[5], images[6], images[7], images[8], images[9] };
    }


    public void SetSlot(int ID)
    {
        CharacterID = ID;
        Button[] buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
        }
        for (int i = 1; i < images.Length; i++)
        {
            images[i].enabled = true;
        }
        character = GlobalDataTable.Instance.character.GetCharToID(ID);
        Save = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, ID);

        SetSlotColorAndCharacterImage(character);
        SetINGIAndBraekAndLV(Save);
    }

    public void SlotClear()
    {
        character = null;
        Save = null;

        Button[] buttons = GetComponentsInChildren<Button>();
        images[0].color = Color.white;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }
        for (int i = 1; i < images.Length; i++)
        {
            images[i].enabled = false;
        }
    }

    public void SetSlotColorAndCharacterImage(Character character)
    {
        //images[1].sprite = Resources.Load<Sprite>(character.Icon);
        Grade grade = character.Grade;
        switch (grade)
        {
            case Grade.S:
                images[0].color = Color.magenta;
                break;
            case Grade.A:
                images[0].color = Color.blue;
                break;
            case Grade.B:
                images[0].color = Color.red;
                break;
        }
    }

    public void SetINGIAndBraekAndLV(CharacterSaveData Save)
    {

        LV.text = $"LV.{Save.Level}";

        for (int i = 0; i < INGI.Length; i++)
        {
            INGI[i].enabled = false;
        }
        for (int i = 0; i < BreakThrought.Length; i++)
        {
            BreakThrought[i].enabled = false;
        }
        for (int i = 0; i < Save.Recognition; i++)
        {
            INGI[i].enabled = true;

        }

        for (int i = 0; i < Save.Necessity; i++)
        {
            BreakThrought[i].enabled = true;
        }
    }


    public void Turnon(int index)
    {
        Selected.SetActive(true);
        TextMeshProUGUI text = Selected.GetComponentInChildren<TextMeshProUGUI>();
        text.text = (index + 1).ToString();
        Index = index;
    }

    public void TurnDown()
    {
        TextMeshProUGUI text = Selected.GetComponentInChildren<TextMeshProUGUI>();
        text.text = string.Empty;
        Index = 0;
        Selected.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsSelected)
        { 
            if (IsSeted)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    GlobalDataTable.Instance.DataCarrier.RemoveIndex(Index);
                    battleSlots.SelectSlot(this);
                    battleSlots.SlotIdexSet();
                    partyList.Partyset();
                }
            }
            else if (battleSlots.BattleSlots.Count < 4 && !IsSeted)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    //Turnon(partyList.Index);
                    GlobalDataTable.Instance.DataCarrier.AddCharacterID(Save.ID);

                    battleSlots.SelectSlot(this);
                    battleSlots.SlotIdexSet();
                    partyList.Partyset();
                }
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GlobalDataTable.Instance.DataCarrier.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(CharacterID));
            GlobalDataTable.Instance.DataCarrier.SetSave(SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, CharacterID));

            SceneLoader.Instance.ChangeScene(SceneState.CharacterInfo);
        }
    }
}
