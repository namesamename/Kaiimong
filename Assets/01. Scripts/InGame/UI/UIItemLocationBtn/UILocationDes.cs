using TMPro;
using UnityEngine;


public class UILocationDes : MonoBehaviour
{

    TextMeshProUGUI[] text;
    GameObject BtnPrefab;

    private void Awake()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        BtnPrefab = Resources.Load<GameObject>("ItemSlot/BtnLocation");
    }


    public void InfoSetting(int ID)
    {
        ItemData item = GlobalDataTable.Instance.Item.GetItemDataToID(ID);

        BtnSetting(item);
        text[0].text = item.Name;
        text[1].text = item.Description;

    }

    public void BtnSetting(ItemData item)
    {
        int Count = item.ObtainLocationCount;

        if(Count > 0)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if(GotoStage(item.FirstLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.FirstLocation, () => Goto(item.FirstLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }

        }
        if(Count > 1)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.SecondLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.SecondLocation, () => Goto(item.SecondLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }
        }
        if (Count > 2)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.ThirdLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.ThirdLocation, () => Goto(item.ThirdLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }
        }
        if (Count > 3)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.FourthLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.FourthLocation, () => Goto(item.FourthLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }
        }
        if (Count > 4)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.FifthLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.FifthLocation, () => Goto(item.FifthLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }
        }
        if (Count > 5)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.SixthLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.SixthLocation, () => Goto(item.SixthLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }
        }
        if (Count > 6)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.SeventhLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.SeventhLocation, () => Goto(item.SeventhLocation));
            }
            else
            {
                game.gameObject.SetActive(false);
            }
        }



    }


    public bool GotoStage(string Location)
    {
        string[] parts = Location.Split('-');
        int ChapterID = int.Parse(parts[0]);
        int StageID = int .Parse(parts[1]);
        
        Chapter chapter = GlobalDataTable.Instance.Chapter.ChapterDic[ChapterID];
        Stage stage = GlobalDataTable.Instance.Stage.StageDic[StageID];
        if (chapter != null && stage != null)
        {
            ChapterSaveData data = SaveDataBase.Instance.GetSaveDataToID<ChapterSaveData>(SaveType.Chapter, chapter.ID);
            StageSaveData stageSave = SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, stage.ID);
            if ((data != null && data.ChapterOpen) && (stageSave != null && stageSave.StageOpen))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false; 
        }
    }



    public void Goto(string Location)
    {
        string[] parts = Location.Split('-');
        int ChapterID = int.Parse(parts[0]);
        int StageID = int.Parse(parts[1]);
        Chapter chapter = GlobalDataTable.Instance.Chapter.ChapterDic[ChapterID];
        Stage stage = GlobalDataTable.Instance.Stage.StageDic[StageID];
        ChapterManager.Instance.RegisterChapter(chapter);
        //StageManager.Instance.CurrentStage = stage;
        SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);

    }


}
