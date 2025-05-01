using UnityEngine;
using UnityEngine.UI;

public class TouchScreen : MonoBehaviour
{
    ScrollRect scroll;
    Vector2 StartPos;
    private bool isDragging = false;

    private void Awake()
    {
       scroll = GetComponent<ScrollRect>();
        
    }
    private void Update()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 touchDelta = (Vector2)Input.mousePosition - StartPos;
            if (Mathf.Abs(touchDelta.y) > Mathf.Abs(touchDelta.x))
            {
    
                float scrollAmount = -touchDelta.y / Screen.height;


                float sensitivity = 0.5f;
                scroll.verticalNormalizedPosition += scrollAmount * sensitivity;
                scroll.verticalNormalizedPosition = Mathf.Clamp01(scroll.verticalNormalizedPosition);

                StartPos = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
#else
        // 실제 터치 입력 (모바일용)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    break;

                case TouchPhase.Moved:
                    Vector2 touchDelta = touch.position - touchStart;
                    if (Mathf.Abs(touchDelta.y) > Mathf.Abs(touchDelta.x))
                    {

                        float sensitivity = 0.5f;
                        float scrollAmount = touchDelta.y / Screen.height;
                        scrollRect.verticalNormalizedPosition += scrollAmount * sensitivity;
                        scroll.verticalNormalizedPosition = Mathf.Clamp01(scroll.verticalNormalizedPosition);
                        touchStart = touch.position;
                    }
                    break;
            }
        }
#endif
    }


}

