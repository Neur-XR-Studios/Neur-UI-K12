using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Simulanis.ContentSDK.K12.UI
{
    public class CFManager : MonoBehaviour
    {
        private Camera CFCamera;
        private void Start()
        {
            CFCamera = GameObject.FindWithTag("CFCamera").GetComponent<Camera>();
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Check for mouse click
            {
                HandleMouseClick(Input.mousePosition);
            }
            if (Input.touchCount > 0) // Check for touch input
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    HandleTouch(touch.position);
                }
            }
        }

        void HandleMouseClick(Vector3 mousePosition)
        {
            Ray ray = CFCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
            {
                ShowDescription();
            }
        }

        void HandleTouch(Vector2 touchPosition)
        {
            Ray ray = CFCamera.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
            {
                ShowDescription();
            }
        }
        void ShowDescription()
        {
            TMP_Text textComponents = gameObject.GetComponentInChildren<TMP_Text>();
            DataManager.StaticVariables.CF_OBJECT_NAME = textComponents.text;
            EventManager.Broadcast(EVENTS.UPDATE_CF_UI);
        }
    }

}
