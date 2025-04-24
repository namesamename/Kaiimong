using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngiBoard : MonoBehaviour, ISetPOPUp
{
    TextMeshProUGUI[] Textmesh;
    private void Awake()
    {
        Textmesh = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Initialize()
    {
        int Rec = ImsiGameManager.Instance.GetCharacterSaveData().Recognition;
        int Id = ImsiGameManager.Instance.GetCharacterSaveData().ID;

        //Textmesh[0].text = Rec.ToString();
        //if (Rec == 0)
        //{
        //    Textmesh[1].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Name;
        //    Textmesh[2].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Des;
        //}
        //else if (Rec == 1)
        //{
        //    Textmesh[1].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Name;
        //    Textmesh[2].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Des;
        //}
        //else if(Rec == 2)
        //{
        //    Textmesh[1].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Name;
        //    Textmesh[2].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Des;
        //}
        //else if(Rec == 3)
        //{
        //    Textmesh[1].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Name;
        //    Textmesh[2].text = GlobalDataTable.Instance.skill.GetReco(Id, Rec).Des;
        //}
    }

}
