using UnityEngine;

public class WaterPooring : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform startPoint, endPoint;

    public Material filledObj;
    public Material emptyObj;
    public Material waterEff;
    public string fillName = "_Fill";
    private float filledStartValue; // Starting fill value of filledObj
    private float emptyStartValue;  // Starting fill value of emptyObj

    private float targetFillValue = -0.7f; // Target for filledObj
    private float targetEmptyValue = 1f;   // Target for emptyObj

    public float fillSpeed = 0.5f;  // Speed of fill value change

    private bool isPouring = false;

    private void Start()
    {
        // Get the current fill values from the materials at start
        filledStartValue = filledObj.GetFloat(fillName);
        emptyStartValue = emptyObj.GetFloat(fillName);


        // Set the new fill value to the empty object
        filledObj.SetFloat(fillName, 0);
        emptyObj.SetFloat(fillName, targetFillValue);


    }

    private void Update()
    {
        // Continuously check rotation
        if (CheckRotation() && !isPouring)
        {
            StartPouring();
        }
        else if (!CheckRotation() && isPouring)
        {
            // Stop pouring if the rotation goes below 45 degrees
            StopPouring();
        }

        // If pouring has started, update the line and smoothly change the fill values
        if (isPouring)
        {
            UpdateLine();
            SmoothlyChangeFillValues();
        }
    }

    private void StartPouring()
    {
        // Start line rendering and mark as pouring
        createLineRenderer();
        isPouring = true;

        lineRenderer.enabled = true;
        // Update the start fill values at the moment pouring starts
        filledStartValue = filledObj.GetFloat(fillName);
        emptyStartValue = emptyObj.GetFloat(fillName);
    }

    private void StopPouring()
    {
        // Stop pouring and optionally clear the line renderer or reset the pouring state
        isPouring = false;
        lineRenderer.enabled = false;
        // Optionally, you can stop or clear the line renderer here
        // lineRenderer.enabled = false; // Disable the line
    }

    private void createLineRenderer()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.15f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.cyan;
            lineRenderer.material = waterEff;
            lineRenderer.useWorldSpace = true;
        }
    }

    private void UpdateLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }

    private void SmoothlyChangeFillValues()
    {
        // Smoothly change the fill value of filledObj from current to -0.7f
        float newFilledValue = Mathf.Lerp(filledStartValue, targetFillValue, fillSpeed * Time.deltaTime);
        newFilledValue = Mathf.Clamp(newFilledValue, -0.7f, filledStartValue);  // Clamping within range

        // Set the new fill value to the filled object
        filledObj.SetFloat(fillName, newFilledValue);

        // Smoothly change the fill value of emptyObj from current to 1
        float newEmptyValue = Mathf.Lerp(emptyStartValue, targetEmptyValue, fillSpeed * Time.deltaTime);
        newEmptyValue = Mathf.Clamp(newEmptyValue, emptyStartValue, 1f);  // Clamping within range

        // Set the new fill value to the empty object
        emptyObj.SetFloat(fillName, newEmptyValue);

        // Update the start values so we continue from the last frame's value in the next frame
        filledStartValue = newFilledValue;
        emptyStartValue = newEmptyValue;
    }

    // Check if the Z rotation is greater than 45 degrees
    private bool CheckRotation()
    {
        float zRotation = transform.rotation.eulerAngles.z;

        // Handle the case where rotation wraps around after 360 degrees
        if (zRotation > 180) zRotation -= 360;

        return Mathf.Abs(zRotation) > 45;
    }
}
