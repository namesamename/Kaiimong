using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleSkill : MonoBehaviour
{
    Image[] images;
    TextMeshProUGUI[] textMeshPros;
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        images[1].enabled = false;

        textMeshPros[0].text = string.Empty;
        textMeshPros[1].text = string.Empty;

    }

    public  void SetBattleSkillUI(ActiveSkill active)
    {
        //

        textMeshPros[0].text = active.Name;
        textMeshPros[1].text = active.Description;
    }

}
