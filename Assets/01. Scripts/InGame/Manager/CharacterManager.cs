using System.Collections.Generic;
public class CharacterManager : Singleton<CharacterManager>
{
    public Dictionary<int, CharacterSaveData> CharacterSaveDic = new Dictionary<int, CharacterSaveData>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

 
    }

    private void Start()
    {
        Initialize();
    }

    public CharacterSaveData GetItemSaveData(int itemID)
    {
        if (CharacterSaveDic.ContainsKey(itemID))
        {
            return CharacterSaveDic[itemID];
        }
        return null;
    }

    public void SaveAllData()
    {
        foreach (var CharacterData in CharacterSaveDic.Values)
        {
            SaveDataBase.Instance.SaveSingleData(CharacterData);
        }
    }

    public void SaveSingleData(int CharacterID)
    {
        SaveDataBase.Instance.SaveSingleData(CharacterSaveDic[CharacterID]);
    }

    public void SaveCharacterSaveData(CharacterSaveData saveData)
    {
        CharacterSaveDic[saveData.ID] = saveData;
        SaveDataBase.Instance.SaveSingleData(saveData);
    }



    //public void Initialize()
    //{   
    //    foreach(ItemData item in GlobalDataTable.Instance.Item.ItemDic.Values)
    //    {
    //        HaveData(item, itemDatas);
    //    }

    //}
    //public void HaveData(ItemData item, List<ItemSavaData> CurSaveList)
    //{
    //    var itemDatas = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);

    //    var FindData = itemDatas.FindIndex(x => x.ID == item.ID);
    //    if(FindData != -1) 
    //    {
    //        CurSaveList.Add(itemDatas[FindData]);
    //    }
    //    else
    //    {
    //        ItemSavaData itemSava = new ItemSavaData()
    //        { 
    //            ID = item.ID,
    //            Value = 0,
    //        };
    //        CurSaveList.Add(itemSava);
    //    }
    //}

    public CharacterSaveData GetSaveData(int ID)
    {
        return CharacterSaveDic[ID];
    }

    public List<CharacterSaveData> GetSaveList()
    {
        List<CharacterSaveData> list = new List<CharacterSaveData>();
        foreach (CharacterSaveData item in CharacterSaveDic.Values)
        {
            if (item.Level > 0)
            {
                list.Add(item);
            }
        }

        return list;

    }
    public void Initialize()
    {
        // 모든 아이템 초기화
        for (int i = 1; i < GlobalDataTable.Instance.character.CharacterDic.Count + 1; i++)
        {
            int CharacterID = GlobalDataTable.Instance.character.GetCharToID(i).ID;
            CharacterSaveData newCharacterID = new CharacterSaveData();
            newCharacterID.ID = CharacterID;

            // 저장된 데이터가 있는지 확인
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
            if (foundData != null && foundData.Find(x => x.ID == CharacterID) != null)
            {
                // 저장된 데이터 로드
                var savedData = foundData.Find(x => x.ID == CharacterID);
                newCharacterID.Recognition = savedData.Recognition;
                newCharacterID.Necessity = savedData.Necessity;
                newCharacterID.ID = savedData.ID;
                newCharacterID.Savetype = savedData.Savetype;
                newCharacterID.IsEquiped = savedData.IsEquiped;
                newCharacterID.Love = savedData.Love;
                newCharacterID.Level = savedData.Level;
    
            }
            else
            {
                // 새 데이터 생성
                newCharacterID.Savetype = SaveType.Character;
                newCharacterID.Recognition = 0;
                newCharacterID.Necessity = 0;
                newCharacterID.IsEquiped =false ;
                newCharacterID.Love = 0;
                newCharacterID.Level = 1;
                SaveDataBase.Instance.SaveSingleData(newCharacterID);
      
            }

            // 사전에 저장
            CharacterSaveDic[CharacterID] = newCharacterID;
        }
    }

    //public void Save()
    //{
    //    foreach(var item in itemDatas) 
    //    {
    //        SaveDataBase.Instance.SaveSingleData(item);
    //    }
    //}
}
