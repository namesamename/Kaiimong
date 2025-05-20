using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChapterCameraTarget : MonoBehaviour
{
    private Vector3 dragStart;
    private bool isDragging = false;
    private float clickStart;
    private bool isStillClick = true;

    [SerializeField] private float camMax;
    [SerializeField] private float camMin;
    [SerializeField] private float clickTime = 0.2f;
    [SerializeField] private float dragDistance = 0.1f;

    [SerializeField] private StageInfoUI stageInfoUI;
    [SerializeField] private SpriteRenderer background;
    private bool backgroundFound = false;

    private void Start()
    {
        FindBackGround();
        MoveCameraToLeftEdge();
    }

    void Update()
    {
        if (!backgroundFound)
        {
            FindBackGround();
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragStart = GetMouseWorldPosition();
            clickStart = Time.time;
            isStillClick = true;
            isDragging = false;
        }

        if (!stageInfoUI.gameObject.activeSelf)
        {

            if (Input.GetMouseButton(0))
            {
                Vector3 current = GetMouseWorldPosition();
                Vector3 delta = dragStart - current;
                delta.y = 0;

                if (!isDragging && delta.magnitude > dragDistance)
                {
                    isDragging = true;
                    isStillClick = false;
                }

                if (isDragging)
                {
                    transform.position += delta;
                    float clamp = Mathf.Clamp(transform.position.x, camMin, camMax);
                    transform.position = new Vector3(clamp, transform.position.y, transform.position.z);
                    dragStart = current;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isStillClick && Time.time - clickStart < dragDistance && !EventSystem.current.IsPointerOverGameObject())
            {
                TryClickObject();
            }
        }
    }

    void FindBackGround()
    {
        background = GameObject.Find("Background").GetComponent<SpriteRenderer>();

        if (background != null)
        {
            float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

            Bounds backgroundBound = background.bounds;
            camMax = backgroundBound.max.x - cameraWidth;
            camMin = backgroundBound.min.x + cameraWidth;

            backgroundFound = true;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = 10f;
        return Camera.main.ScreenToWorldPoint(mouseScreen);
    }

    void TryClickObject()
    {

        if (IsPointerOverUIElement())
        {
            return; 
        }

        Vector3 mouseWorld = GetMouseWorldPosition();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorld, Vector2.zero);

        bool isCharacterClicked = false;
        bool isStageClicked = false;
        StageSlot clickedStage = null;

   
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.CompareTag("CharacterSlot"))
            {
                isCharacterClicked = true;
      
            }
            else if (hit.collider.CompareTag("Stage"))
            {
                isStageClicked = true;
                clickedStage = hit.collider.GetComponent<StageSlot>();
            }
        }

        if (isStageClicked && clickedStage != null)
        {
            stageInfoUI.gameObject.SetActive(true);
            stageInfoUI.SetUI(clickedStage);
        }
        else if (!isCharacterClicked)
        {

            stageInfoUI.DisableUI();
        }
    }


    private bool IsPointerOverUIElement()
    {

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;


        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, results);


        return results.Count > 0;
    }



    void MoveCameraToLeftEdge()
    {
        if (backgroundFound)
        {
            transform.position = new Vector3(camMin, transform.position.y, transform.position.z);
        }
    }
}
