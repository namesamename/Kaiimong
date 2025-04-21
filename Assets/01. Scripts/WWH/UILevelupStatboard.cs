using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelupStatboard : MonoBehaviour
{
    TextMeshProUGUI[] textMeshPros;
    void Start()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void Statset(int Weight = 0)
    {
        Character character = ImsiGameManager.Instance.GetCharacter();
        CharacterSaveData Save = ImsiGameManager.Instance.GetCharacterSaveData();
        if ( Weight != 0)
        {
            textMeshPros[1].text = (character.Health + Save.Level).ToString();
            textMeshPros[3].text = (character.Attack + Save.Level).ToString();
            textMeshPros[5].text = (character.Defence + Save.Level).ToString();
            textMeshPros[7].text = (character.Speed + Save.Level).ToString();
            textMeshPros[9].text = (character.CriticalPer + (Save.Level*0.01)).ToString();
            textMeshPros[11].text = (character.CriticalAttack + (Save.Level * 0.01)).ToString();
        }
        else
        {
            textMeshPros[1].text = $"{character.Health + Save.Level} + ({Weight})";
            textMeshPros[3].text = $"{character.Attack + Save.Level} + ({Weight})";
            textMeshPros[5].text = $"{character.Defence + Save.Level} + ({Weight})";
            textMeshPros[7].text = $"{character.Speed + Save.Level} + ({Weight})";
            textMeshPros[9].text = $"{character.CriticalPer + (Save.Level* 0.01 )} + ({Weight})";
            textMeshPros[11].text = $"{character.CriticalAttack + (Save.Level * 0.01)} + ({Weight})";
        }
    }


   
}
