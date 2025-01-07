using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TooltipLineRendererController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Header("Tooltip Points")]
    [Tooltip("Start point of the tooltip line")]
    public Transform startPoint;

    [Tooltip("End point of the tooltip line")]
    public Transform endPoint;

    [Header("Line Settings")]
    [Tooltip("Width of the line")]
    [Range(0f, 1f)]
    public float lineWidth = 0.02f; 
    [Tooltip("Width of the line")]
    public Material LineMaterial;

    private void Awake()
    {
        // Get the LineRenderer component attached to the GameObject
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        // Configure the LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = LineMaterial;
    }

    private void Update()
    {
        if (startPoint != null && endPoint != null)
        {
            // Update the LineRenderer positions based on the assigned transforms
            lineRenderer.SetPosition(0, startPoint.position); // Start point
            lineRenderer.SetPosition(1, endPoint.position);   // End point
        }
    }

    /// <summary>
    /// Dynamically sets the start and end points of the line.
    /// </summary>
    /// <param name="start">The new start Transform.</param>
    /// <param name="end">The new end Transform.</param>
    public void SetPoints(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
    }
}
