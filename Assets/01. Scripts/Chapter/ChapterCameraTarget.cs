using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChapterCameraTarget : MonoBehaviour
{
    private Vector3 dragStart;
    private bool isDragging = false;
    private float clickStart;
    private bool isStillClick = true;

    [SerializeField] private float clickTime = 0.2f;
    [SerializeField] private float dragDistance = 0.1f;

    [SerializeField] private StageInfoUI stageInfoUI;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStart = GetMouseWorldPosition();
            clickStart = Time.time;
            isStillClick = true;
            isDragging = false;
        }

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
                dragStart = current;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isStillClick && Time.time - clickStart < dragDistance)
            {
                TryClickObject();
            }
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
        Vector3 mouseWorld = GetMouseWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

        if (hit.collider != null)
        {
            if (stageInfoUI.enabled)
            {
                stageInfoUI.DisableUI();
            }
            else
            {
                if (hit.collider.CompareTag("Stage"))
                {
                    StageSlot clickedStage = hit.collider.GetComponent<StageSlot>();
                    stageInfoUI.SetUI(clickedStage);
                    stageInfoUI.gameObject.SetActive(true);
                }
            }
        }
    }
}
