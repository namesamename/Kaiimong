
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPassivePOPUP : UIPOPUP
{
    Button[] buttons;
    TextMeshProUGUI[] textMeshPros;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
    }


    private void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(Destroy);
        }

        List<CharacterUpgradeTable> characterUpgradeTables =new  List<CharacterUpgradeTable>();

        characterUpgradeTables = GlobalDataTable.Instance.Upgrade.GetRecoList(GlobalDataTable.Instance.DataCarrier.GetSave().ID);

        textMeshPros[0].text = characterUpgradeTables[0].Name;
        textMeshPros[1].text = characterUpgradeTables[0].Des;
        textMeshPros[2].text = characterUpgradeTables[1].Name;
        textMeshPros[3].text = characterUpgradeTables[1].Des;
        textMeshPros[4].text = characterUpgradeTables[2].Name;
        textMeshPros[5].text = characterUpgradeTables[2].Des;
    }




}
