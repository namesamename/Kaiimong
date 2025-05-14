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
            
        }
        else if(Count > 1)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.SecondLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.SecondLocation, () => Goto(item.SecondLocation));
            }
        }
        else if (Count > 2)
        {
            GameObject game = Instantiate(BtnPrefab, transform.GetChild(2));
            if (GotoStage(item.ThridLocation))
            {
                game.GetComponent<UILocationBtn>().SetBtn(item.ThridLocation, () => Goto(item.ThridLocation));
            }
        }



    }


    public bool GotoStage(string Location)
    {
        Debug.Log(Location);
        Debug.Log(Location.Substring(0));
        Debug.Log(Location.Substring(2));
        int ChapterID =int.Parse(Location.Substring(0, 1));
        int StageID = int .Parse( Location.Substring(2));
        
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

        int ChapterID = int.Parse(Location.Substring(0,1));
        int StageID = int.Parse(Location.Substring(2));
        Chapter chapter = GlobalDataTable.Instance.Chapter.ChapterDic[ChapterID];
        Stage stage = GlobalDataTable.Instance.Stage.StageDic[StageID];
        ChapterManager.Instance.RegisterChapter(chapter);
        //StageManager.Instance.CurrentStage = stage;
        SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);

    }


}
