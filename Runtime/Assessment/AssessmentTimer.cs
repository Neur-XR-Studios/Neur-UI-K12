using System.Collections;
using TMPro;
using UnityEngine;
namespace Simulanis.ContentSDK.K12.Assessment
{
public class AssessmentTimer : MonoBehaviour
{
    public TMP_Text TimerText; // TextMeshPro component to display the timer
    public bool isCountdown = false; // Toggle between count-up (false) and countdown (true)
    public string targetTime = "5:00"; // Target time in MM:SS format for countdown
    private float elapsedTime = 0f; // Tracks elapsed time in seconds (for count-up)
    private float countdownTime = 0f; // Total time in seconds for countdown
    public bool isTimerRunning = false; // Tracks whether the timer is running

    // Start is called before the first frame update
    void Start()
    {
        if (isCountdown)
        {
            SetCountdownTime(targetTime);
        }
        else
        {
            ResetTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            if (isCountdown)
            {
                UpdateCountdownTimer();
            }
            else
            {
                UpdateCountUpTimer();
            }
        }
    }

    // Starts the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Stops the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Resets the timer for both count-up and countdown modes
    public void ResetTimer()
    {
        if (isCountdown)
        {
            countdownTime = ParseTimeStringToSeconds(targetTime);
        }
        else
        {
            elapsedTime = 0f;
        }
        isTimerRunning = false;
        UpdateTimerText();
    }

    // Updates the count-up timer
    private void UpdateCountUpTimer()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerText();
    }

    // Updates the countdown timer
    private void UpdateCountdownTimer()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            countdownTime = 0f;
            isTimerRunning = false;
            TimerEnded();
        }
    }

    // Converts MM:SS string to seconds
    private float ParseTimeStringToSeconds(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        if (timeParts.Length == 2)
        {
            int minutes = int.Parse(timeParts[0]);
            int seconds = int.Parse(timeParts[1]);
            return (minutes * 60) + seconds;
        }
        Debug.LogError("Invalid targetTime format. Use MM:SS.");
        return 0;
    }

    // Sets the countdown time from a string (e.g., "5:00")
    public void SetCountdownTime(string timeString)
    {
        countdownTime = ParseTimeStringToSeconds(timeString);
        UpdateTimerText();
    }

    // Updates the TimerText to display the time in HH:MM:SS:MS format
    private void UpdateTimerText()
    {
        float timeToDisplay = isCountdown ? countdownTime : elapsedTime;
        int hours = Mathf.FloorToInt(timeToDisplay / 3600);
        int minutes = Mathf.FloorToInt((timeToDisplay % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //  display Time
        
        TimerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
       
    }

    // Callback for when the timer ends
    private void TimerEnded()
    {
        Debug.Log("Timer has ended!");
        // Add any additional actions when the timer ends
    }
}

}