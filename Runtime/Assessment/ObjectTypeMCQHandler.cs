using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Simulanis.ContentSDK.K12.Assessment
{
    public class ObjectTypeMCQHandler : MonoBehaviour
    {
        public List<BoundingBox> boundingBoxes = new List<BoundingBox>();
        void Start()
        {
            foreach (BoundingBox boundingBox in boundingBoxes)
            {
                if (boundingBox == null)
                {
                    Debug.LogWarning("BoundingBox reference is null in the list.");
                    continue;
                }

                Renderer childRenderer = boundingBox.GetComponentInChildren<Renderer>();
                if (childRenderer != null)
                {
                    GameObject child = childRenderer.gameObject;

                    // Ensure the ObjectTypeMCQHandler is not already added
                    if (child.GetComponent<ObjectTypeMCQHandler>() == null)
                    {
                        child.AddComponent<ObjectTypeMCQHandler>();
                    }
                    else
                    {
                        Debug.LogWarning($"ObjectTypeMCQHandler is already attached to {child.name}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Renderer not found for BoundingBox: {boundingBox.name}");
                }
            }
        }
    }
}
