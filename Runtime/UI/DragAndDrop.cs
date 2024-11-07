using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        HandleMouseInput();
#elif UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();
#endif
    }

    void HandleMouseInput()
    {
        // Mouse input for PC and WebGL platforms
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    offset = transform.position - GetMouseWorldPosition();
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void HandleTouchInput()
    {
        // Touch input for Android and iOS platforms
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                        offset = transform.position - GetTouchWorldPosition(touch.position);
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                transform.position = GetTouchWorldPosition(touch.position) + offset;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    Vector3 GetTouchWorldPosition(Vector3 touchPosition)
    {
        touchPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(touchPosition);
    }
}
