using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterContent : MonoBehaviour
{
    private Vector3 dragOrigin;
    private bool isDragging = false;

    private float clickStartTime;
    private float clickTime = 0.2f;
    private float dragMin = 0.1f; 

    private bool isClickValid = true;

    [Header("CameraMovement")]
    [SerializeField] private float scrollMinX = 0f;
    [SerializeField] private float scrollMaxX = 100f;
    [SerializeField] private float scrollSpead = 0.1f;

    public Chapter Chapter;
    [SerializeField] private StageSlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<StageSlot>();
        ChapterManager.Instance.InitializeChapter(Chapter.ID);
    }

    void Start()
    {
        InitializeStages();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickStartTime = Time.time;
            isClickValid = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector3.Distance(curMousePos, dragOrigin);

            // 드래그 거리 초과하면 클릭 취소
            if (distance > dragMin)
            {
                isDragging = true;
                isClickValid = false;

                Vector3 difference = dragOrigin - curMousePos;
                Camera.main.transform.position += difference;

                dragOrigin = curMousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isClickValid && Time.time - clickStartTime < clickTime)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log("클릭된 오브젝트: " + hit.collider.name);
                    // 여기서 오브젝트 정보 표시 함수 호출 가능
                }
            }

            isDragging = false;
        }
    }

    private void InitializeStages()
    {
        for (int i = 0; i < Chapter.StagesID.Length; i++)
        {
            slots[i].Initialize(Chapter.StagesID[i]);
            if (SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, Chapter.StagesID[i]).StageOpen)
            {
                slots[i].gameObject.SetActive(true);
            }
            else { slots[i].gameObject.SetActive(false); }
        }
    }
}
