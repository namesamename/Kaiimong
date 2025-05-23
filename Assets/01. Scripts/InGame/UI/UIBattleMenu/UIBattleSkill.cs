using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleSkill : MonoBehaviour
{
    Image[] images;
    TextMeshProUGUI[] textMeshPros;
    public bool IsOpen = false;
    public bool IsUpdate = false;
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(IsOpen)
        {
            if(IsUpdate)
            {
                for (int i = 0; i < textMeshPros.Length; i++)
                {
                    textMeshPros[i].enabled = true;
                }
                images[1].enabled = true;
            }
            else
            {
                images[1].enabled = false;
                for (int i = 0; i < textMeshPros.Length; i++)
                {
                    textMeshPros[i].enabled = false;
                }
            }
        }
    }
    private void Start()
    {
        images[0].enabled = false;
        images[1].enabled = false;

        textMeshPros[0].text = string.Empty;
        textMeshPros[1].text = string.Empty;

    }

    public void  SetDown()
    {
        images[1].enabled = false;

        textMeshPros[0].text = string.Empty;
        textMeshPros[1].text = string.Empty;
    }

    public  async void SetBattleSkillUI(ActiveSkill active)
    {
        images[1].color = Color.white;
        images[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.SkillIcon, active.ID);
        textMeshPros[0].text = active.Name;
        textMeshPros[1].text = active.Description;

        if (!IsOpen)
        {
            images[1].enabled = false;
            for (int i = 0; i < textMeshPros.Length; i++)
            {
                textMeshPros[i].enabled = false;
            }
        }
    }



}
