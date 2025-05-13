//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;




//public class GameManager : Singleton<GameManager>
//{

//    ItemManager itemManager;
//    SceneLoader loader;
//    UIManager uiManager;
//    ChapterManager chapterManager;
//    CurrencyManager currencyManager;
//    StageManager stageManager;
//    GlobalDataTable GlobalDataTable;
//    SaveDataBase SaveData;
//    CharacterManager characterManager;
//    AddressableManager addressableManager;
//    QuestManager questManager;
//    AudioManager audioManager;

//    GameObject[] ManagersPrefabs;

//    bool IsInitialize = false;



//    //�Ŵ����� ������ �ʱ�ȭ
//    //�� �Ŵ������� �ٸ� �Ŵ����� �����ؾ��� �� ��� ����
//    private void Awake()
//    {
//        //�� �Ŵ����� �ʱ�ȭ
//        if (_instance == null)
//        {
//            _instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            if (_instance != this)
//            {
//                Destroy(gameObject);
//            }
//        }
//        ManagersPrefabs = Resources.LoadAll<GameObject>("Manager");
     
//        LevelUpSystem.Init();

//        Initialize();
//    }


//    public void Initialize()
//    {
//        if (!IsInitialize)
//        {
//            GetCompo();
//            SaveData.Initialize();
//            GlobalDataTable.Initialize();
//            characterManager.Initialize();
//            itemManager.Initialize();
//            currencyManager.InitialIze();
//            questManager.Initialize();
//            stageManager.Initialize();
//            chapterManager.Initailize();
//            loader.Initialize();
//            uiManager.Initialize();
//            IsInitialize = true;
//        }

//    }

//    public void GetCompo()
//    {
//        addressableManager = GetComponentInChildren<AddressableManager>();
//        if(addressableManager == null)
//        {
//            addressableManager = Instantiate(Resources.Load<GameObject>("Manager/AddressableManager")).GetComponent<AddressableManager>();
//            addressableManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
//        }

//        questManager = GetComponentInChildren<QuestManager>();
//        if (questManager == null)
//        {
//            questManager = Instantiate(Resources.Load<GameObject>("Manager/QuestManager")).GetComponent<QuestManager>();
//            questManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
//        }



//        characterManager = GetComponentInChildren<CharacterManager>();
//        if (characterManager == null)
//        {
//            characterManager = Instantiate(Resources.Load<GameObject>("Manager/CharacterManager")).GetComponent<CharacterManager>();
//            characterManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
//        }

//        itemManager = GetComponentInChildren<ItemManager>();
//        if (itemManager == null)
//        {
//            itemManager = Instantiate(Resources.Load<GameObject>("Manager/ItemManager")).GetComponent<ItemManager>();
//            itemManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
//        }

//        loader = GetComponentInChildren<SceneLoader>();
//        if (loader == null)
//        {
//            loader = Instantiate(Resources.Load<GameObject>("Manager/SceneLoader")).GetComponent<SceneLoader>();
//            loader.transform.SetParent(transform);
//        }

//        uiManager = GetComponentInChildren<UIManager>();
//        if (uiManager == null)
//        {
//            uiManager = Instantiate(Resources.Load<GameObject>("Manager/UIManager")).GetComponent<UIManager>();
//            uiManager.transform.SetParent(transform);
//        }

//        chapterManager = FindObjectOfType<ChapterManager>();
//        if (chapterManager == null)
//        {
//            chapterManager = Instantiate(Resources.Load<GameObject>("Manager/ChapterManager")).GetComponent<ChapterManager>();
//            chapterManager.transform.SetParent(transform);
//        }

//        currencyManager = GetComponentInChildren<CurrencyManager>();
//        if (currencyManager == null)
//        {
//            currencyManager = Instantiate(Resources.Load<GameObject>("Manager/CurrencyManager")).GetComponent<CurrencyManager>();
//            currencyManager.transform.SetParent(transform);
//        }

//        //stageManager = GetComponentInChildren<StageManager>();
//        stageManager = FindObjectOfType<StageManager>();
//        if (stageManager == null)
//        {
//            stageManager = Instantiate(Resources.Load<GameObject>("Manager/StageManager")).GetComponent<StageManager>();
//            stageManager.transform.SetParent(transform);
//        }

//        GlobalDataTable = GetComponentInChildren<GlobalDataTable>();
//        if (GlobalDataTable == null)
//        {
//            GlobalDataTable = Instantiate(Resources.Load<GameObject>("Manager/GlobalDataTable")).GetComponent<GlobalDataTable>();
//            GlobalDataTable.transform.SetParent(transform);
//        }

//        SaveData = GetComponentInChildren<SaveDataBase>();
//        if (SaveData == null)
//        {
//            SaveData = Instantiate(Resources.Load<GameObject>("Manager/SaveDataBase")).GetComponent<SaveDataBase>();
//            SaveData.transform.SetParent(transform);
//        }

//        audioManager = GetComponentInChildren<AudioManager>();
//        if (audioManager == null)
//        {
//            audioManager = Instantiate(Resources.Load<GameObject>("Manager/AudioManager")).GetComponent<AudioManager>();
//            audioManager.transform.SetParent(transform);
//        }
//    }
//    public void OnApplicationQuit()
//    {
//        if(questManager != null)
//        questManager.TimeCheck();
//    }

//}
