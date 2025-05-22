using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBattleStatBoard : MonoBehaviour
{
    TextMeshProUGUI[] textMeshPros;
    public bool IsOpen = false;
    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    private void Start()
    {
        for (int i = 0; i < textMeshPros.Length; i++) 
        {
            textMeshPros[i].text = string.Empty;
        }
    }


    public void SetBattleStatUI(CharacterCarrier character)
    {
     

        Character Chars = GlobalDataTable.Instance.character.GetCharToID(character.GetID());
        CharacterSaveData saveData = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, character.GetID());

        textMeshPros[0].text = Chars.Name;
        textMeshPros[1].text = Chars.Grade.ToString();
        textMeshPros[2].text = $"{character.stat.healthStat.CurHealth}/{character.stat.healthStat.Value}";

        int Attack = Chars.Attack + saveData.Level;

        textMeshPros[3].text = Attack == character.stat.attackStat.Value ?
            $"공격력: {Attack}" : $"공격력: {Attack} +({character.stat.attackStat.Value - Attack})";

        int Defense = Chars.Defence + saveData.Level;


        textMeshPros[4].text = Defense == character.stat.defenseStat.Value ?
            $"방어력: {Defense}" : $"방어력: {Defense} +({character.stat.defenseStat.Value - Defense})";

        int Speed = Chars.Speed + saveData.Level;

        //textMeshPros[5].text = Speed == character.stat.agilityStat.Value ?
        // $"속도: {Speed}" : $"속도: {Speed} +({character.stat.agilityStat.Value - Speed})";
        textMeshPros[5].text = "속도: " + Speed.ToString();

        float Criper = Chars.CriticalPer + (float)0.01 * saveData.Level;

        textMeshPros[6].text = Criper == character.stat.criticalPerStat.Value ?
            $"치명타 확률: {Criper}" : $"치명타 확률: {Criper} +({character.stat.criticalPerStat.Value - Criper})";

        float CriAttack = Chars.CriticalAttack + (float)0.01 * saveData.Level;

        textMeshPros[7].text = CriAttack == character.stat.criticalAttackStat.Value ?
            $"치명타 공격: {CriAttack}" : $"치명타 공격: {CriAttack} +({character.stat.criticalAttackStat.Value - CriAttack})";

        if(!IsOpen)
        {
            for (int i = 0; i < textMeshPros.Length; i++)
            {
                textMeshPros[i].enabled = false;
            }
        }

    }

    public void SetEnalbe()
    {
        for (int i = 0; i < textMeshPros.Length; i++)
        {
            textMeshPros[i].enabled = true;
        }
    }

    public void SetDown()
    {
        for (int i = 0; i < textMeshPros.Length; i++)
        {
            textMeshPros[i].text = string.Empty;
        }
    }
}
