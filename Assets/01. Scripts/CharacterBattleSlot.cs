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

    [SerializeField] GameObject Selected;
    UIPartyList partyList;


    int Index;

    private void Awake()
    {
        partyList= FindAnyObjectByType<UIPartyList>();
        images = GetComponentsInChildren<Image>();
        LV = GetComponentInChildren<TextMeshProUGUI>();
        INGI = new Image[3] { images[2], images[3], images[4] };
        BreakThrought = new Image[5] { images[5], images[6], images[7], images[8], images[9] };
    }


    public void SetSlot(int ID)
    {
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

        for(int i = 0; i < Save.Necessity; i++)
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
        Selected.SetActive(false);
        Index = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //나중에 바꿔야함 
        if (IsSeted)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                GlobalDataTable.Instance.DataCarrier.RemoveIndex(Index);
                TurnDown();
                partyList.Partyset();
            }
        }
        else
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Turnon(partyList.Index);
                GlobalDataTable.Instance.DataCarrier.AddCharacterID(Save.ID);
                partyList.Partyset();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SceneLoader.Instance.ChangeScene(SceneState.CharacterInfo);
        }
    }
}
