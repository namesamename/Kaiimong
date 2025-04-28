using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [Header("UI Reference")]
    TextMeshProUGUI[] textMeshPros;
    Image[] images;
    Button button;

    CharacterSaveData characterSaveData;
    Character Character;

    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        images = GetComponentsInChildren<Image>();
        button = GetComponentInChildren<Button>();
    }

                                 // ĳ���� ���� ID ����



    public void SetSlot(CharacterSaveData saveData, Character character)                
    {
        characterSaveData = saveData;
        Character = character;
        SetSlotColor(saveData, character);
        textMeshPros[1].text = character.Name;                // ĳ���� �̸� ǥ��
        textMeshPros[0].text = $"Lv. {saveData.Level}";      // ĳ���� ���� ǥ��

        button.onClick.RemoveAllListeners();

       button.onClick.AddListener(ChangeScene);




        Debug.Log($"[SetSlot] ���Կ� ����: {character.Name} (ID:{character.ID}) Lv.{saveData.Level} Grade:{character.Grade}");

       
    }
    public void ChangeScene()
    {

        GlobalDataTable.Instance.DataCarrier.SetSave(characterSaveData);
        GlobalDataTable.Instance.DataCarrier.SetCharacter(Character);
        if(SceneLoader.Instance.GetPre()  == SceneState.ProfileScene)
        {

            SceneLoader.Instance.ChangeScene(SceneState.ProfileScene);
        }
        else
        {
            SceneLoader.Instance.ChangeScene(SceneState.CharacterInfo);
        }

    }



    public void SetSlotColor(CharacterSaveData saveData, Character character)
    {

        for (int i = 0; i < images.Length; i++) 
        {
            images[i].enabled = false;
        }


        switch (character.Grade)
        {
            case Grade.S:
                images[0].enabled = true;
                images[0].color = new Color(229f / 255f, 156f / 255f, 42f / 255f);
                break;
            case Grade.A:
                images[0].enabled = true;
                images[0].color = new Color(164f / 255f, 68f / 255f, 217f / 255f);
                break;
            case Grade.B:
                images[0].enabled = true;
                images[0].color = new Color(34f / 255f, 111f / 255f, 236f / 255f);
                break;
        }
        images[0].enabled = true;
        images[1].enabled = true;
        //images[1].sprite = Resources.Load<Sprite>(character.icon);

        for (int i = 0;i < saveData.Recognition; i++) 
        {
            images[i+2].enabled = true;
        }
        for (int i = 0; i < saveData.Necessity; i++)
        {
            images[i + 5].enabled = true;
        }



    }





}