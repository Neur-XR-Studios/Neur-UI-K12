using UnityEngine;
using UnityEngine.Events; // Required to use UnityEvent

public class PositionChecker : MonoBehaviour
{
    public Transform targetPosition;  // Reference to the target position
    public float snapDistance = 0.5f; // The distance at which the object will snap to the target
    public float moveSpeed = 5f;      // Speed at which the object moves towards the target

    private bool isSnapped = false;   // A flag to check if the object has already snapped

    // UnityEvent that will be invoked when the object snaps to the target
    public UnityEvent onSnapToTarget;
    public float distanceToTarget;
    void Update()
    {
        if (!isSnapped)
        {
            // Continuously move the object towards the target
          //  MoveTowardsTarget();

            // Check if the object is within the snap distance
            CheckProximityAndSnap();
        }
    }

    // Function to move the object towards the target
    void MoveTowardsTarget()
    {
        // Move the object towards the target position at the given move speed
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
    }

    // Function to check proximity and snap the object to the target position
    void CheckProximityAndSnap()
    {
        // Calculate the distance between the object and the target position
        distanceToTarget = Vector3.Distance(transform.position, targetPosition.position);

        // If the distance is less than or equal to the snap distance, snap to the target
        if (distanceToTarget <= snapDistance)
        {
            // Snap the object to the target position
            transform.position = targetPosition.position;

            // Set the flag to true to prevent further movement
            isSnapped = true;

            // Invoke the event when the object snaps to the target
            if (onSnapToTarget != null)
            {
                onSnapToTarget.Invoke();
            }

            // Log or perform additional actions here if needed
            Debug.Log("Object snapped to target position!");
        }
    }


    public void ChangeTarget(Transform newTarget)
    {
        targetPosition = newTarget;
        isSnapped = false;
    }
}
