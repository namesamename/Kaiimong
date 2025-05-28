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
        buttons = GetComponentsInChildren<Button>();
    }



    public void Initialize( CharacterCarrier carrier)
    {
        ButtonSet();
        CharacterImageSet(carrier.GetID());
        CharacterNameSet(carrier.GetID() );
        CharacterStatSet(carrier);
    }


    public async void CharacterImageSet(int ID)
    {
        CharacterImages = CharacterImage.GetComponentsInChildren<Image>();

        CharacterImages[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.BattleSD, ID);
    }

    public void CharacterNameSet(int ID)
    {
        CharacterName.GetComponentInChildren<TextMeshProUGUI>().text = GlobalDataTable.Instance.character.GetCharToID(ID).Name;
    }


    public void CharacterStatSet(CharacterCarrier character)
    {
        textMeshPros = CharacterStat.GetComponentsInChildren <TextMeshProUGUI>();

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


    public void ButtonSet()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(Destroy);
        }

    }

}
