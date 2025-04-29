using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum CharacterInfoType
{
    Back,
    Main,
    Skin,
    CharacterImage,
    Name,
    Stat,
    Dolpa,
    Levelup,
    IngiUp,
    ActiveSkill,
    PassiveSkill,

}


public class CharacterInfoHUD : MonoBehaviour
{
    public CharacterInfoType CharacterInfoType;

    public void InitialIze(Character character , CharacterSaveData saveData)
    {

        if(character == null)
        {
            Debug.Log("Characternull");
        }
        if(saveData == null)
        {
            Debug.Log("saveDatanull");
        }

        switch (CharacterInfoType)
        {

            case CharacterInfoType.Back:
                Button Backbutton = GetComponentInChildren<Button>();
                Backbutton.onClick.RemoveAllListeners();
                Backbutton.onClick.AddListener(() => SceneLoader.Instance.ChanagePreScene());
          
                break;
            case CharacterInfoType.Main:
                Button button = GetComponentInChildren<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
                break;
            case CharacterInfoType.Skin:
                
                break;
            case CharacterInfoType.CharacterImage:
                Image images = GetComponent<Image>();
                //images.sprite = 이미지가 나와야함
                break;
            case CharacterInfoType.Name:
                TextMeshProUGUI[] textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
                Slider slider = GetComponentInChildren<Slider>();
                textMeshPros[0].text = GlobalDataTable.Instance.character.GetCharToID(saveData.ID).Name;
                textMeshPros[1].text = GlobalDataTable.Instance.character.GetCharToID(saveData.ID).Grade.ToString();
                textMeshPros[2].text = saveData.Love.ToString("N1");
                slider.value = saveData.Love / 100f;
                break;
            case CharacterInfoType.Stat:
                TextMeshProUGUI[] textMeshPross = GetComponentsInChildren<TextMeshProUGUI>();
                Debug.Log("TextMeshProUGUI Count: " + textMeshPross.Length);
                textMeshPross[1].text = (character.Health + saveData.Level).ToString();
                textMeshPross[3].text = (character.Attack + saveData.Level).ToString();
                textMeshPross[5].text = (character.Defence + saveData.Level).ToString();
                textMeshPross[7].text = (character.Speed + saveData.Level).ToString();
                float Cir = (float)(character.CriticalPer + (saveData.Level* 0.01)) * 100;
                textMeshPross[9].text = $"{Cir}%";
                float CirAt = (float)(character.CriticalAttack + (saveData.Level * 0.01)) * 100;
                textMeshPross[11].text = $"{CirAt}%";

                break;
             case CharacterInfoType.Dolpa:
                Button DolpaButton = GetComponentInChildren<Button>();
                Image[] Dolpaimages = GetComponentsInChildren<Image>();
                for (int i = 0; i < Dolpaimages.Length; i++)
                {
                    Dolpaimages[i].enabled = false;
                }

                for (int i = 0;i < saveData.Recognition; i++)
                {
                    Dolpaimages[i].enabled = true;
                }

                DolpaButton.onClick.RemoveAllListeners();
                DolpaButton.onClick.AddListener(() => UIManager.Instance.ShowPopup("breakthroughPopUP"));
                break;
            case CharacterInfoType.Levelup:
                TextMeshProUGUI Level = GetComponentInChildren<TextMeshProUGUI>();
                Button LevelUpButton = GetComponentInChildren<Button>();
                LevelUpButton.onClick.RemoveAllListeners();
                Level.text = $"Level\n{saveData.Level}";
                LevelUpButton.onClick.AddListener(() => UIManager.Instance.ShowPopup("LevelupPopUp"));
                break;
            case CharacterInfoType.IngiUp:
                TextMeshProUGUI Ingi = GetComponentInChildren<TextMeshProUGUI>();
                Button IngiUpButton = GetComponentInChildren<Button>();
                IngiUpButton.onClick.RemoveAllListeners();
                Ingi.text = $"Ingi\n{saveData.Recognition}";
                IngiUpButton.onClick.AddListener(() => UIManager.Instance.ShowPopup("IngiPopUp"));
                break;
            case CharacterInfoType.ActiveSkill:
                Button[] buttons = GetComponentsInChildren<Button>();
                for(int i = 0; i < buttons.Length; i++)
                {
                    int index = i;
                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(() => SetSkillPopup(index));
                }
                break;
            case CharacterInfoType.PassiveSkill:
                break;
        }



    }

    
    public void SetSkillPopup(int index)
    {
        UIPopup game = UIManager.Instance.ShowPopup("SkillInfoPopup");
        game.GetComponent<SkillInfoPoPUP>().SetPopup(GlobalDataTable.Instance.DataCarrier.GetCharacter(), index);
    }

}
