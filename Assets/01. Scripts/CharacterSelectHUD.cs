using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectHUD : MonoBehaviour
{

    UICharacterSlotSpawner slotSpawner;
    Transform prefab;
    Button[] buttons;
    TextMeshProUGUI[] textMeshPros;
    List<CharacterSaveData> saveDatas = new List<CharacterSaveData>();


    private void Awake()
    {
        slotSpawner = GetComponentInChildren<UICharacterSlotSpawner>();
        buttons = GetComponentsInChildren<Button>(true);
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>(true);
        prefab = transform.GetChild(2).transform.GetChild(2).transform.GetChild(2);
    }

    private void Start()
    {

        foreach (CharacterSaveData data in SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character))
        {
            if (data.IsEquiped)
            {
                saveDatas.Add(data);
                Debug.Log(data.IsEquiped);
            }
        }

        slotSpawner.GradeFilter(saveDatas);

        Setting();
        Textset();
    }




    public void Setting()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChanagePreScene());
        buttons[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
        buttons[2].onClick.AddListener(SetDropdonw);
        buttons[3].onClick.AddListener(LevelOrder);
        buttons[4].onClick.AddListener(GradeOrder);

    }



    public void GradeOrder()
    {
        if(slotSpawner.ISGradeOrder)
        {
            slotSpawner.GradeFilter(saveDatas);
        }
        else
        {
            slotSpawner.GradeDescendingFilter(saveDatas);
        }
    }


    public void LevelOrder()
    {
        if (slotSpawner.ISLevelOrder)
        {
            slotSpawner.LevelFilter(saveDatas);
        }
        else
        {
            slotSpawner.LevelDescendingFilter(saveDatas);
        }
    }

    public void Textset()
    {
        textMeshPros[3].text = "Level";
        textMeshPros[4].text = "Grade";
    }


    public void SetDropdonw()
    {
        if(prefab.gameObject.activeSelf)
        {
            prefab.gameObject.SetActive(false);

        }
        else
        {
            prefab.gameObject.SetActive(true);
        }
    }



}
