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
            $"���ݷ�: {Attack}" : $"���ݷ�: {Attack} +({character.stat.attackStat.Value - Attack})";

        int Defense = Chars.Defence + saveData.Level;


        textMeshPros[4].text = Defense == character.stat.defenseStat.Value ?
            $"����: {Defense}" : $"����: {Defense} +({character.stat.defenseStat.Value - Defense})";

        int Speed = Chars.Speed + saveData.Level;

        //textMeshPros[5].text = Speed == character.stat.agilityStat.Value ?
        // $"�ӵ�: {Speed}" : $"�ӵ�: {Speed} +({character.stat.agilityStat.Value - Speed})";
        textMeshPros[5].text = "�ӵ�: " + Speed.ToString();

        float Criper = Chars.CriticalPer + (float)0.01 * saveData.Level;

        textMeshPros[6].text = Criper == character.stat.criticalPerStat.Value ?
            $"ġ��Ÿ Ȯ��: {Criper}" : $"ġ��Ÿ Ȯ��: {Criper} +({character.stat.criticalPerStat.Value - Criper})";

        float CriAttack = Chars.CriticalAttack + (float)0.01 * saveData.Level;

        textMeshPros[7].text = CriAttack == character.stat.criticalAttackStat.Value ?
            $"ġ��Ÿ ����: {CriAttack}" : $"ġ��Ÿ ����: {CriAttack} +({character.stat.criticalAttackStat.Value - CriAttack})";

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
