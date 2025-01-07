using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace K12.Assessment
{
    [RequireComponent(typeof(BoxCollider))]   
    public class BoundingBox : MonoBehaviour
    {
        [Space(20)]
        [Header("-----------------Setup------------------")]
        [Space(20)]
        [SerializeField]
        private Vector3 boundingBoxSize = Vector3.one; // Fixed bounding box size (e.g., 1x1x1)[Space(20)]
        [Range(0f, 1f)]
        [SerializeField]
        private float boundingobjectSize = 0.3f; 
        [Space(20)]
        [SerializeField]
        private float rotationSpeed = 20f; // Speed of rotation in degrees per second [Space(20)]
        [SerializeField]
        public bool isRotating = false; // Speed of rotation in degrees per second
        [Space(20)]
        [SerializeField]
        private bool ShowGizmos = false;

        private void Start()
        {
            transform.GetComponent<BoxCollider>().size = boundingBoxSize;
        }
        public void FitAndCenterObject(GameObject obj)
        {
            StartCoroutine(RotateContinuously(obj));
            if (!obj.TryGetComponent(out Renderer renderer))
            {
                Debug.LogError("The target object does not have a Renderer component.");
                return;
            }

            // Calculate bounds of the object
            Bounds objectBounds = CalculateBounds(obj);

            // Determine scale factor to fit object within bounding box
            Vector3 objectSize = objectBounds.size;
            float scaleFactor = Mathf.Min(
                boundingobjectSize / objectSize.x,
                boundingobjectSize / objectSize.y,
                boundingobjectSize / objectSize.z
            );

            // Apply scaling
            obj.transform.localScale *= scaleFactor;

            // Recalculate bounds and center the object
            objectBounds = CalculateBounds(obj);
            Vector3 pivotOffset = obj.transform.position - objectBounds.center;
            obj.transform.position = transform.position + pivotOffset;            
        }

        private IEnumerator RotateContinuously(GameObject obj)
        {
            while (isRotating)
            {
                // Rotate around the Y-axis in world space
                obj.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

                yield return null; // Wait for the next frame
            }
        }

        private Bounds CalculateBounds(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
                return new Bounds(obj.transform.position, Vector3.zero);

            Bounds combinedBounds = renderers[0].bounds;
            foreach (var renderer in renderers)
            {
                combinedBounds.Encapsulate(renderer.bounds);
            }
            return combinedBounds;
        }

        void OnDrawGizmos()
        {
            if (ShowGizmos)
            {
                // Visualize the bounding box in the Scene view
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position, boundingBoxSize); // Adjust box size if needed
            }
        }
    }

}

