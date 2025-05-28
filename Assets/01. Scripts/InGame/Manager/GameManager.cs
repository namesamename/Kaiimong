using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager : Singleton<GameManager>
{

    ItemManager itemManager;
    SceneLoader loader;
    UIManager uiManager;
    ChapterManager chapterManager;
    CurrencyManager currencyManager;
    StageManager stageManager;
    GlobalDataTable GlobalDataTable;
    SaveDataBase SaveData;
    CharacterManager characterManager;
    AddressableManager addressableManager;

    QuestManager questManager;

    AudioManager audioManager;

    GameObject[] ManagersPrefabs;


    AudioClip audioClip;


    GameObject TutorialManager;

    private void Awake()
    {
        //�� �Ŵ����� �ʱ�ȭ
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
        ManagersPrefabs = Resources.LoadAll<GameObject>("Manager");
        GetSFX();
        GetCompo();
        LevelUpSystem.Init();
        Initialize();

        Tutorial();
    }
    void Start()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
    Application.runInBackground = true;
#else
        Application.runInBackground = false;
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.PlaySFX(audioClip);
        }
#else
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
           AudioManager.PlaySFX(audioClip);
    }
#endif
    }


    public void Initialize()
    {
        SaveData.Initialize();
        GlobalDataTable.Initialize();

        characterManager.Initialize();
        itemManager.Initialize();
        currencyManager.InitialIze();
        questManager.Initialize();
        stageManager.Initialize();

        chapterManager.Initailize();


        audioManager.Initialize();
        loader.Initialize();
        uiManager.Initialize();
    }

    public void GetCompo()
    {
        addressableManager = GetComponentInChildren<AddressableManager>();
        if(addressableManager == null)
        {
            addressableManager = Instantiate(Resources.Load<GameObject>("Manager/AddressableManager")).GetComponent<AddressableManager>();
            addressableManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
        }

        questManager = GetComponentInChildren<QuestManager>();
        if (questManager == null)
        {
            questManager = Instantiate(Resources.Load<GameObject>("Manager/QuestManager")).GetComponent<QuestManager>();
            questManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
        }



        characterManager = GetComponentInChildren<CharacterManager>();
        if (characterManager == null)
        {
            characterManager = Instantiate(Resources.Load<GameObject>("Manager/CharacterManager")).GetComponent<CharacterManager>();
            characterManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
        }

        itemManager = GetComponentInChildren<ItemManager>();
        if (itemManager == null)
        {
            itemManager = Instantiate(Resources.Load<GameObject>("Manager/ItemManager")).GetComponent<ItemManager>();
            itemManager.transform.SetParent(transform);  // �θ� ���� (�ʿ� ��)
        }

        loader = GetComponentInChildren<SceneLoader>();
        if (loader == null)
        {
            loader = Instantiate(Resources.Load<GameObject>("Manager/SceneLoader")).GetComponent<SceneLoader>();
            loader.transform.SetParent(transform);
        }

        uiManager = GetComponentInChildren<UIManager>();
        if (uiManager == null)
        {
            uiManager = Instantiate(Resources.Load<GameObject>("Manager/UIManager")).GetComponent<UIManager>();
            uiManager.transform.SetParent(transform);
        }

        chapterManager = FindObjectOfType<ChapterManager>();
        if (chapterManager == null)
        {
            chapterManager = Instantiate(Resources.Load<GameObject>("Manager/ChapterManager")).GetComponent<ChapterManager>();
            chapterManager.transform.SetParent(transform);
        }

        currencyManager = GetComponentInChildren<CurrencyManager>();
        if (currencyManager == null)
        {
            currencyManager = Instantiate(Resources.Load<GameObject>("Manager/CurrencyManager")).GetComponent<CurrencyManager>();
            currencyManager.transform.SetParent(transform);
        }

        //stageManager = GetComponentInChildren<StageManager>();
        stageManager = FindObjectOfType<StageManager>();
        if (stageManager == null)
        {
            stageManager = Instantiate(Resources.Load<GameObject>("Manager/StageManager")).GetComponent<StageManager>();
            stageManager.transform.SetParent(transform);
        }

        GlobalDataTable = GetComponentInChildren<GlobalDataTable>();
        if (GlobalDataTable == null)
        {
            GlobalDataTable = Instantiate(Resources.Load<GameObject>("Manager/GlobalDataTable")).GetComponent<GlobalDataTable>();
            GlobalDataTable.transform.SetParent(transform);
        }

        SaveData = GetComponentInChildren<SaveDataBase>();
        if (SaveData == null)
        {
            SaveData = Instantiate(Resources.Load<GameObject>("Manager/SaveDataBase")).GetComponent<SaveDataBase>();
            SaveData.transform.SetParent(transform);
        }

        audioManager = GetComponentInChildren<AudioManager>();
        if (audioManager == null)
        {
            audioManager = Instantiate(Resources.Load<GameObject>("Manager/AudioManager")).GetComponent<AudioManager>();
            audioManager.transform.SetParent(transform);
        }

        
        
    }

    public void Tutorial()
    {
        if(!currencyManager.GetIsTutorial())
        {
            TutorialManager = Instantiate(Resources.Load<GameObject>("Manager/TutorialManager"));
            TutorialManager.transform.SetParent(transform);
        }
    }
    public void OnApplicationQuit()
    {
        if(questManager != null)
        questManager.TimeCheck();
    }

    public async void GetSFX()
    {
        audioClip = await AddressableManager.Instance.LoadAsset<AudioClip>(AddreassablesType.SoundEffectFx, 3);
    }

}
