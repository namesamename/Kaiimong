using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterInfoType
{
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

    public void InitialIze(CharacterCarrier character)
    {
        switch (CharacterInfoType)
        {
            case CharacterInfoType.Main:
                Button button = GetComponentInChildren<Button>();
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.MainScene));
                break;
            case CharacterInfoType.Skin:
                //
                break;
            case CharacterInfoType.CharacterImage:
                Image images = GetComponent<Image>();
                //images.sprite = 이미지가 나와야함
                
                break;
            case CharacterInfoType.Name:
                TextMeshProUGUI[] textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
                Slider slider = GetComponentInChildren<Slider>();
                textMeshPros[0].text = GlobalDataTable.Instance.character.GetCharToID(character.CharacterSaveData.ID).Name;
                textMeshPros[1].text = GlobalDataTable.Instance.character.GetCharToID(character.CharacterSaveData.ID).Grade.ToString();
                textMeshPros[2].text = character.CharacterSaveData.Love.ToString("N1");
                slider.value = character.CharacterSaveData.Love / 100f;
                break;
            case CharacterInfoType.Stat:
                TextMeshProUGUI[] textMeshPross = GetComponentsInChildren<TextMeshProUGUI>();
                textMeshPross[1].text = character.stat.healthStat.Value.ToString();
                textMeshPross[3].text = character.stat.attackStat.Value.ToString();
                textMeshPross[5].text = character.stat.defenseStat.Value.ToString();
                textMeshPross[7].text = character.stat.agilityStat.Value.ToString();

                float Cir = character.stat.criticalPerStat.Value * 100;
                textMeshPross[9].text = $"{Cir}%";
                float CirAt = character.stat.criticalAttackStat.Value * 100;
                textMeshPross[11].text = $"{CirAt}%";

                break;
             case CharacterInfoType.Dolpa:
                Button DolpaButton = GetComponentInChildren<Button>();
                DolpaButton.onClick.AddListener(() => UIManager.Instance.ShowPopup("breakthroughPopUP"));
                break;
            case CharacterInfoType.Levelup:
                TextMeshProUGUI Level = GetComponentInChildren<TextMeshProUGUI>();
                Button LevelUpButton = GetComponentInChildren<Button>();
                Level.text = $"Level\n{character.CharacterSaveData.Level}";
                LevelUpButton.onClick.AddListener(() => UIManager.Instance.ShowPopup("LevelupPopUp"));
                break;
            case CharacterInfoType.IngiUp:
                TextMeshProUGUI Ingi = GetComponentInChildren<TextMeshProUGUI>();
                Button IngiUpButton = GetComponentInChildren<Button>();
                Ingi.text = $"Ingi\n{character.CharacterSaveData.Necessity}";
                IngiUpButton.onClick.AddListener(() => UIManager.Instance.ShowPopup("IngiPopUp"));
                break;
            case CharacterInfoType.ActiveSkill:
                Button[] buttons = GetComponentsInChildren<Button>();
                for(int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].onClick.AddListener(() => UIManager.Instance.ShowPopup("SkillInfoPopup"));
                }
                break;
            case CharacterInfoType.PassiveSkill:
                break;
        }



    }

}
