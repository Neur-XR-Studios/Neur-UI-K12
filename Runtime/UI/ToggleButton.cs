using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ToggleButton : MonoBehaviour
{
    // Reference to the UI Button
    private Button button;

    // UnityEvents for toggle states
    public UnityEvent onToggleValuetrue, onToggleValuefalse;

    // Tracks the toggle state
    private bool isToggle = false;

    private void Start()
    {
        // Get the Button component
        button = GetComponent<Button>();

        // Add the OnToggleChanged method as a listener when the button is clicked
        button.onClick.AddListener(OnToggleChanged);
    }

    // This function is called when the toggle state changes
    private void OnToggleChanged()
    {
        // Flip the toggle state
        isToggle = !isToggle;

        // Invoke the appropriate event based on the toggle state
        if (isToggle)
        {
            //AudioSource audioSource = GetComponent<AudioSource>();
            onToggleValuetrue.Invoke();
        }
        else
        {
            onToggleValuefalse.Invoke();
        }
    }
    private void OnDestroy()
    {
        // Remove listener when the object is destroyed
        button.onClick.RemoveListener(OnToggleChanged);
    }
}
