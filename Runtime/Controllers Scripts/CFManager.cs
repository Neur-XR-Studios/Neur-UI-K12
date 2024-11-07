using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CFManager : MonoBehaviour
{
    void EnableChildGameObject(string childName)
    {        
        Transform childTransform = transform.Find(childName); // Find the child GameObject by name
        
        if (childTransform != null) // Check if the child exists
        {            
            childTransform.gameObject.SetActive(true); // Enable the child GameObject
            Debug.Log($"{childName} has been enabled.");
        }
        else
        {
            Debug.LogWarning($"Child GameObject named {childName} not found.");
        }
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
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
        {
            ShowDescription();
        }
    }

    void HandleTouch(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

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

    private void OnEnable()
    {
        EnableChildGameObject("NameCanvas");        
    }    
}
