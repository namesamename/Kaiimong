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


    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        images = GetComponentsInChildren<Image>();

        button = GetComponentInChildren<Button>();
    }

                                 // 캐릭터 고유 ID 저장



    public void SetSlot(CharacterSaveData saveData, Character character)                
    {

        SetSlotColor(saveData, character);
        textMeshPros[1].text = character.Name;                // 캐릭터 이름 표시
        textMeshPros[0].text = $"Lv. {saveData.Level}";      // 캐릭터 레벨 표시

        button.onClick.RemoveAllListeners();

        GlobalDataTable.Instance.DataCarrier.SetSave(saveData);
        GlobalDataTable.Instance.DataCarrier.SetCharacter(character);


        button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.CharacterInfo));

        Debug.Log($"[SetSlot] 슬롯에 적용: {character.Name} (ID:{character.ID}) Lv.{saveData.Level} Grade:{character.Grade}");

       
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
        //images[0].enabled = true;
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