using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIBattleStatPOPUP : UIPOPUP
{
    Button[] buttons;

    Image[] CharacterImages;


    TextMeshProUGUI[] textMeshPros;
    public GameObject CharacterImage;
    public GameObject CharacterName;
    public GameObject CharacterStat;
    private void Awake()
    {
        
    }



    public void Initialize( CharacterCarrier carrier)
    {
        ButtonSet(carrier);
        CharacterImageSet(carrier);
        CharacterNameSet(carrier );
        CharacterStatSet(carrier);
    }


    public async void CharacterImageSet(CharacterCarrier character)
    {

        CharacterImages = CharacterImage.GetComponentsInChildren<Image>();



        if (character.GetCharacterType() == CharacterType.Friend)
        {
            CharacterImages[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.BattleSD, character.GetID());
        }
        else
        {
            CharacterImages[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.EnemyBattleSD, character.GetID());
        }
        
    }

    public void CharacterNameSet(CharacterCarrier character)
    {
        if (character.GetCharacterType() == CharacterType.Friend)
        {
            CharacterName.GetComponentInChildren<TextMeshProUGUI>().text = GlobalDataTable.Instance.character.GetCharToID(character.GetID()).Name;
        }
        else
        {
            CharacterName.GetComponentInChildren<TextMeshProUGUI>().text = GlobalDataTable.Instance.character.GetEnemyToID(character.GetID()).Name;
        }


    }


    public void CharacterStatSet(CharacterCarrier character)
    {
        textMeshPros = CharacterStat.GetComponentsInChildren <TextMeshProUGUI>();


        if (character.GetCharacterType() == CharacterType.Friend)
        {
            Character Chars = GlobalDataTable.Instance.character.GetCharToID(character.GetID());
            CharacterSaveData saveData = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, character.GetID());
            textMeshPros[0].text = "체력";
            textMeshPros[1].text = $"{character.stat.healthStat.CurHealth}/{character.stat.healthStat.Value}";
            int Attack = Chars.Attack + saveData.Level;
            textMeshPros[2].text = "공격력";
            textMeshPros[3].text = Attack == character.stat.attackStat.Value ?
                $"{Attack}" : $"{Attack} +({character.stat.attackStat.Value - Attack})";
            int Defense = Chars.Defence + saveData.Level;
            textMeshPros[4].text = "방어력";
            textMeshPros[5].text = Defense == character.stat.defenseStat.Value ?
                $"{Defense}" : $"{Defense} +({character.stat.defenseStat.Value - Defense})";
            int Speed = Chars.Speed + saveData.Level;
            textMeshPros[6].text = "속도";
            textMeshPros[7].text = Speed == character.stat.agilityStat.Value ?
            $"{Speed}" : $" {Speed} +({character.stat.agilityStat.Value - Speed})";
            float Criper = Chars.CriticalPer + (float)0.01 * saveData.Level;
            textMeshPros[8].text = "치명타 확률";
            textMeshPros[9].text = Criper == character.stat.criticalPerStat.Value ?
                $"{Criper}" : $"{Criper} +({character.stat.criticalPerStat.Value - Criper})";
            float CriAttack = Chars.CriticalAttack + (float)0.01 * saveData.Level;
            textMeshPros[10].text = "치명타 공격력";
            textMeshPros[11].text = CriAttack == character.stat.criticalAttackStat.Value ?
                $"{CriAttack}" : $"{CriAttack} +({character.stat.criticalAttackStat.Value - CriAttack})";
        }
        else
        {
            Enemy enemy = GlobalDataTable.Instance.character.GetEnemyToID(character.GetID());
            textMeshPros[0].text = "체력";
            textMeshPros[2].text = "공격력";
            textMeshPros[4].text = "방어력";
            textMeshPros[6].text = "속도";
            textMeshPros[8].text = "치명타 확률";
            textMeshPros[10].text = "치명타 공격력";

            textMeshPros[1].text = $"{character.stat.healthStat.CurHealth}/{character.stat.healthStat.Value}";
            textMeshPros[3].text = $"{enemy.Att}";
            textMeshPros[5].text = $"{enemy.Def}";
            textMeshPros[7].text = $"{enemy.Speed}";
            textMeshPros[9].text = $"{enemy.Cri}";
            textMeshPros[11].text = $"{enemy.Cri}";

        }
    }


    public void ButtonSet(CharacterCarrier character)
    {
        buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(Destroy);
        }

        character.SetPopupShonwFalse();

    }

}
