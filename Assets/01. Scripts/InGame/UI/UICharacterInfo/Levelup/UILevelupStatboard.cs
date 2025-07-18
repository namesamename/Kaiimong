using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelupStatboard : BaseLevelupInfo, ISetPOPUp
{
    TextMeshProUGUI[] textMeshPros;    
    public void Initialize()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        Statset(0);
    }

    public void Statset(int Weight = 0)
    {
        Character character = GlobalDataTable.Instance.DataCarrier.GetCharacter();
        CharacterSaveData Save = GlobalDataTable.Instance.DataCarrier.GetSave();
        if ( Weight == 0 && Save.Level == 1)
        {
            textMeshPros[1].text = (character.Health + (Save.Level-1)).ToString();
            textMeshPros[3].text = (character.Attack + (Save.Level - 1)).ToString();
            textMeshPros[5].text = (character.Defence + (Save.Level - 1)).ToString();
            textMeshPros[7].text = (character.Speed + (Save.Level - 1)).ToString();
            textMeshPros[9].text = (character.CriticalPer + ((Save.Level-1)*0.001)).ToString("N3");
            textMeshPros[11].text = (character.CriticalAttack + ((Save.Level-1) * 0.01)).ToString("N2");
        }
        else
        {
            textMeshPros[1].text = $"{character.Health + Save.Level} + ({Weight})";
            textMeshPros[3].text = $"{character.Attack + Save.Level} + ({Weight})";
            textMeshPros[5].text = $"{character.Defence + Save.Level} + ({Weight})";
            textMeshPros[7].text = $"{character.Speed + Save.Level} + ({Weight})";
            textMeshPros[9].text = $"{(character.CriticalPer + (Save.Level * 0.001)).ToString("N3")} + ({(Weight * 0.001).ToString("N3")})";
            textMeshPros[11].text = $"{(character.CriticalAttack + (Save.Level * 0.01)).ToString("N2")} + ({(Weight * 0.01).ToString("N2")})";
        }
    }

   
}
