using UnityEngine;
using UnityEngine.Events; // Import UnityEvent

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; // Control how fast the object rotates
    public float resetSpeed = 200f;     // Speed for resetting rotation back to 0°

    private Camera mainCamera;
    private Vector3 previousMousePosition;
    private Vector2 previousTouchPosition;
    private bool isRotating = false;   // To check if the user is actively rotating
    private bool shouldReset = false;   // For triggering the smooth reset

    // Boolean flags to check if rotation limits are reached
    private bool hasReachedRotationLimit = false;

    // Event to invoke when rotation limit is reached
    public UnityEvent onRotationLimitReached;

    // Booleans to allow or restrict rotation directions
    public bool allowRotateTo90 = true;    // Allow rotation from 0 to 90 degrees
    public bool allowRotateToMinus90 = true; // Allow rotation from 0 to -90 degrees

    void Start()
    {
        mainCamera = Camera.main;

        // Initialize UnityEvent if not set in the Inspector
        if (onRotationLimitReached == null)
            onRotationLimitReached = new UnityEvent();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        HandleMouseInput();
#elif UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();
#endif
        HandleReset();
    }

    void HandleMouseInput()
    {
        // Mouse input for PC and WebGL platforms
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
            isRotating = true; // Start rotating
            shouldReset = false; // Cancel reset when rotation starts
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
            RotateObjectWithInput(deltaMousePosition.x);
            previousMousePosition = Input.mousePosition;
        }

        // Stop rotating and trigger reset when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false; // Stop rotating
            shouldReset = true; // Trigger reset
        }
    }

    void HandleTouchInput()
    {
        // Touch input for Android and iOS platforms
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
                isRotating = true; // Start rotating
                shouldReset = false; // Cancel reset when rotation starts
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaTouchPosition = touch.position - previousTouchPosition;
                RotateObjectWithInput(deltaTouchPosition.x);
                previousTouchPosition = touch.position;
            }

            // Stop rotating and trigger reset when touch is lifted
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isRotating = false;
                shouldReset = true; // Trigger reset
            }
        }
    }

    void RotateObjectWithInput(float deltaInput)
    {
        // Get the current Z-axis rotation in degrees
        float currentZRotation = transform.eulerAngles.z;

        // Normalize the current Z rotation to a range of -180 to 180 to make clamping easier
        if (currentZRotation > 180) currentZRotation -= 360;

        // Apply rotation only if allowed
        if ((currentZRotation < 90 && allowRotateTo90) || (currentZRotation > -90 && allowRotateToMinus90))
        {
            float rotationZ = deltaInput * rotationSpeed * Time.deltaTime;
            float newZRotation = currentZRotation - rotationZ;

            // Clamp the new rotation to be between -90 and 90 degrees
            newZRotation = Mathf.Clamp(newZRotation, -90, 90);

            // Apply the clamped rotation
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZRotation);

            // Check for rotation limit conditions
            CheckRotationLimit(newZRotation);
        }
    }

    void CheckRotationLimit(float newZRotation)
    {
        // Check if the rotation has reached 90° or -90°
        if ((newZRotation >= 90 || newZRotation <= -90) && !hasReachedRotationLimit)
        {
            hasReachedRotationLimit = true;
            onRotationLimitReached.Invoke(); // Invoke the event
            shouldReset = true; // Start reset
        }

        // Reset flag if the rotation is back within range
        if (newZRotation < 90 && newZRotation > -90)
        {
            hasReachedRotationLimit = false;
        }
    }

    void HandleReset()
    {
        // Smoothly reset Z rotation back to 0° if not actively rotating
        if (shouldReset && !isRotating)
        {
            float currentZRotation = transform.eulerAngles.z;

            // Normalize current Z rotation to be between -180 and 180
            if (currentZRotation > 180) currentZRotation -= 360;

            // Lerp towards 0° if within the allowed ranges
            float newZRotation = Mathf.Lerp(currentZRotation, 0, resetSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZRotation);
        }
    }
    private void OnDisable()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

    }
}
