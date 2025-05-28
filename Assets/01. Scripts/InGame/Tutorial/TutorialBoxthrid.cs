using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBoxthrid : MonoBehaviour
{
    public GameObject ImagePrefab;
    Canvas canvas;
    RectTransform StageUIRect;
    public RectTransform topMask;
    public RectTransform bottomMask;
    public RectTransform leftMask;
    public RectTransform rightMask;

    private void Awake()
    {
        StageUIRect = FindAnyObjectByType<TouchScreen>().GetComponent<RectTransform>();
        canvas = FindAnyObjectByType<Canvas>();
    }
    private void Start()
    {
        Highlight();
    }




    public void Highlight()
    {
        Vector3[] worldCorners = new Vector3[4];
        StageUIRect.GetWorldCorners(worldCorners); // 0:BL, 1:TL, 2:TR, 3:BR

        // 간단한 방법: InverseTransformPoint 사용
        Vector3 bl = canvas.GetComponent<RectTransform>().InverseTransformPoint(worldCorners[0]);
        Vector3 tl = canvas.GetComponent<RectTransform>().InverseTransformPoint(worldCorners[1]);
        Vector3 tr = canvas.GetComponent<RectTransform>().InverseTransformPoint(worldCorners[2]);
        Vector3 br = canvas.GetComponent<RectTransform>().InverseTransformPoint(worldCorners[3]);

        float left = bl.x;
        float right = tr.x;
        float top = tr.y;
        float bottom = bl.y;

        // 디버그 출력

        // Top 마스크 (타겟 UI 위쪽 전체)
        topMask.anchorMin = new Vector2(0, 0);
        topMask.anchorMax = new Vector2(1, 1);
        topMask.offsetMin = new Vector2(0, top);
        topMask.offsetMax = new Vector2(0, 0);

        // Bottom 마스크 (타겟 UI 아래쪽 전체)
        bottomMask.anchorMin = new Vector2(0, 0);
        bottomMask.anchorMax = new Vector2(1, 1);
        bottomMask.offsetMin = new Vector2(0, 0);
        bottomMask.offsetMax = new Vector2(0, bottom);

        // Left 마스크 (타겟 UI 왼쪽)
        leftMask.anchorMin = new Vector2(0, 0);
        leftMask.anchorMax = new Vector2(1, 1);
        leftMask.offsetMin = new Vector2(0, bottom);
        leftMask.offsetMax = new Vector2(left, top);

        // Right 마스크 (타겟 UI 오른쪽)
        rightMask.anchorMin = new Vector2(0, 0);
        rightMask.anchorMax = new Vector2(1, 1);
        rightMask.offsetMin = new Vector2(right, bottom);
        rightMask.offsetMax = new Vector2(0, top);
    }








}
